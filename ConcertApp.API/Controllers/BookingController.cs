using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;
using ConcertApp.Data.Repository;
namespace ConcertApp.API.Controllers;
public enum ErrorCode
{
    InvalidBooking,
    UserBookingExistsExists,
    CouldNotCreateBooking,
    BookingNotFound,
    CouldNotDeleteBooking

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
                return BadRequest(ErrorCode.InvalidBooking.ToString());
            }
            bool itemExists = await _unitOfWork.Bookings.Find(item.ID);
            if (itemExists)
            {
                return StatusCode(StatusCodes.Status409Conflict,
                ErrorCode.BookingExists.ToString());
            }
            _unitOfWork.Bookings.Insert(item);
            int affectedItems = await _unitOfWork.Complete();
        }
        catch (Exception)
        {
            return BadRequest(ErrorCode.CouldNotCreateBooking.ToString());
        }
        return Ok(_mapper.Map<BookingDto>(item));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        Booking? item;
        try
        {
            item = await _unitOfWork.Bookings.Find(id);
            if (item == null)
            {
                return NotFound(ErrorCode.BookingNotFound.ToString());
            }
            //_todoRepository.Delete(id);
            
            _unitOfWork.Bookings.Delete(id);
            int affectedItems = await _unitOfWork.Complete();
        }
        catch (Exception)
        {
            return BadRequest(ErrorCode.CouldNotDeleteBooking.ToString());
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
                return BadRequest(ErrorCode.BookingRequired.ToString());
            }
            var existingItem = await _unitOfWork.Bookings.Find(item.ID);
            if (existingItem == null)
            {
                return NotFound(ErrorCode.RecordNotFound.ToString());
            }
            item.Performances = existingItem.Performances;
            //_todoRepository.Update(item);
            //_unitOfWork.TodoItems.Update(item);
            _unitOfWork.Bookings.Delete(existingItem);
            _unitOfWork.Bookings.Insert(item);
            int affectedItems = await _unitOfWork.Complete();
        }
        catch (Exception)
        {
            return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
        }
        
//return NoContent();
return Ok(_mapper.Map<BookingDto>(item));
    }
}
