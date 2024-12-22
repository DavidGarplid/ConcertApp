using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;
using ConcertApp.Data.Repository;
namespace ConcertApp.API.Controllers;

[Route("api/[controller]")]
public class ConcertController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ConcertController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(_mapper.Map<IEnumerable<ConcertDto>>(await _unitOfWork.Concerts.All()));
    }

}


