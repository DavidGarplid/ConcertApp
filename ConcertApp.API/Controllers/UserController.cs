using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;
using ConcertApp.Data.Repository;
namespace ConcertApp.API.Controllers;
public enum ErrorCode
{
    InvalidUser,
    UserExists,
    CouldNotCreateUser,
    
}

[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UserController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(_mapper.Map<IEnumerable<UserDto>>(await _unitOfWork.Users.All()));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto dto)
    {
        if (_unitOfWork == null)
        {
            return StatusCode(500, "Dependency injection failed: _unitOfWork is null");
        }

        if (_unitOfWork.Users == null)
        {
            return StatusCode(500, "Dependency injection failed: _unitOfWork.Users is null");
        }
        User item;
        try
        {
            item = _mapper.Map<User>(dto);
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ErrorCode.InvalidUser.ToString());
            }
            User itemExists = await _unitOfWork.Users.Find(item.ID);
            if (itemExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict,
                ErrorCode.UserExists.ToString());
            }
            _unitOfWork.Users.Insert(item);
            int affectedItems = await _unitOfWork.Complete();
        }
        catch (Exception ex)
        {
            return BadRequest($"CouldNotCreateUser: {ex.Message}");
        }
        return Ok(_mapper.Map<UserDto>(item));
    }
}
