using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;
using ConcertApp.Data.Repository;
using System.Diagnostics;
namespace ConcertApp.API.Controllers;
public enum ErrorCode
{
    InvalidUser,
    UserExists,
    CouldNotCreateUser,
    InvalidCredentials

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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto loginDto)
    {
        if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
        {
            return BadRequest("Email and password are required");
        }

        // Find the user by email
        var users = await _unitOfWork.Users.All();
        var user = users.FirstOrDefault(u => u.email == loginDto.Email);

        if (user == null || user.password != loginDto.Password)
        {
            return Unauthorized(ErrorCode.InvalidCredentials.ToString()); // Invalid email or password
        }
        Debug.WriteLine($"User before mapping: Name={user.name}, Email={user.email}");
        // Map user to UserDto
        var userDto = _mapper.Map<UserDto>(user);

        // Log the data before returning
        Debug.WriteLine($"Returning UserDto: Name={userDto.Name}, Email={userDto.Email}");


        // If login is successful, return the user info
        return Ok(userDto);
        //return Ok(_mapper.Map<UserDto>(user));
    }
}
