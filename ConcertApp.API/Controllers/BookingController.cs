using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;
using ConcertApp.Data.Repository;
using Microsoft.EntityFrameworkCore;
namespace ConcertApp.API.Controllers;
public enum ErrorCode2
{
    InvalidBooking,
    BookingExists,
    CouldNotCreateBooking,
    BookingNotFound,
    CouldNotDeleteBooking,
    BookingRequired,
    CouldNotUpdateItem,
    RecordNotFound
}

[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public BookingController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("isBooked")]
    public async Task<IActionResult> IsPerformanceBooked(int performanceId, int userId)
    {
        var bookings = await _unitOfWork.Bookings.All();
        bool isBooked = bookings.Any(b => b.PerformanceID == performanceId && b.UserId == userId);

        return Ok(new { IsBooked = isBooked });
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBooking([FromBody] BookingDto bookingDto)
    {
        if (bookingDto == null || bookingDto.UserID <= 0 || bookingDto.PerformanceID <= 0)
        {
            return BadRequest("Invalid booking data.");
        }

        var existingBooking = await _unitOfWork.Bookings.All()
            .ContinueWith(t => t.Result.FirstOrDefault(b => b.UserId == bookingDto.UserID && b.PerformanceID == bookingDto.PerformanceID));

        if (existingBooking != null)
        {
            // If booking exists, return a message without trying to create another
            return Ok(new { IsBooked = true, Message = "Booking already exists." });
        }

        var booking = _mapper.Map<Booking>(bookingDto); // Use AutoMapper

        _unitOfWork.Bookings.Insert(booking);
        await _unitOfWork.Complete();

        return Ok("Booking created successfully.");
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteBooking(int performanceId, int userId)
    {
        var booking = await _unitOfWork.Bookings.Find(b => b.PerformanceID == performanceId && b.UserId == userId);

        if (booking == null || !booking.Any()) // ✅ Ensure booking exists
        {
            return NotFound("Booking not found.");
        }

        _unitOfWork.Bookings.Delete(booking.First()); // ✅ Delete only the first matching booking

        bool success = await _unitOfWork.Complete() > 0; // ✅ Ensure deletion was committed

        if (!success)
        {
            return StatusCode(500, "Failed to delete booking.");
        }

        return Ok("Booking deleted successfully.");
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetBookingsByUserId(int userId)
    {
        try
        {
            // Retrieve all bookings from the repository
            var bookings = await _unitOfWork.Bookings.All();
            var userBookings = bookings.Where(b => b.UserId == userId).ToList(); // Filter by UserID

            if (!userBookings.Any())
            {
                return NotFound(ErrorCode2.BookingNotFound.ToString()); // If no bookings found
            }

            // Map the result to BookingDto and return
            return Ok(_mapper.Map<IEnumerable<BookingDto>>(userBookings));
        }
        catch (Exception ex)
        {
            return BadRequest($"Error fetching bookings: {ex.Message}");
        }
    }


    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(_mapper.Map<IEnumerable<BookingDto>>(await _unitOfWork.Bookings.All()));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookingDto dto)
    {
        Booking item;
        try
        {
            item = _mapper.Map<Booking>(dto);
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ErrorCode2.InvalidBooking.ToString());
            }
            Booking itemExists = await _unitOfWork.Bookings.Find(item.ID);
            if (itemExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict,
                ErrorCode2.BookingExists.ToString());
            }
            _unitOfWork.Bookings.Insert(item);
            int affectedItems = await _unitOfWork.Complete();
        }
        catch (Exception)
        {
            return BadRequest(ErrorCode2.CouldNotCreateBooking.ToString());
        }
        return Ok(_mapper.Map<BookingDto>(item));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Booking? item;
        try
        {
            item = await _unitOfWork.Bookings.Find(id);
            if (item == null)
            {
                return NotFound(ErrorCode2.BookingNotFound.ToString());
            }
            //_todoRepository.Delete(id);
            
            _unitOfWork.Bookings.Delete(id);
            int affectedItems = await _unitOfWork.Complete();
        }
        catch (Exception)
        {
            return BadRequest(ErrorCode2.CouldNotDeleteBooking.ToString());
        }
        //return NoContent();
        return Ok(_mapper.Map<BookingDto>(item));
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] BookingDto dto)
    {
        Booking item;
        try
        {
            item = _mapper.Map<Booking>(dto);
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ErrorCode2.BookingRequired.ToString());
            }
            var existingItem = await _unitOfWork.Bookings.Find(item.ID);
            if (existingItem == null)
            {
                return NotFound(ErrorCode2.RecordNotFound.ToString());
            }
            item.Performance = existingItem.Performance;
            //_todoRepository.Update(item);
            //_unitOfWork.TodoItems.Update(item);
            _unitOfWork.Bookings.Delete(existingItem);
            _unitOfWork.Bookings.Insert(item);
            int affectedItems = await _unitOfWork.Complete();
        }
        catch (Exception)
        {
            return BadRequest(ErrorCode2.CouldNotUpdateItem.ToString());
        }
        
//return NoContent();
return Ok(_mapper.Map<BookingDto>(item));
    }
}
