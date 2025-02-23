using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;
using ConcertApp.Data.Repository;
namespace ConcertApp.API.Controllers;

[Route("api/[controller]")]
public class PerformanceController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PerformanceController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(_mapper.Map<IEnumerable<PerformanceDto>>(await _unitOfWork.Performances.All()));
    }

    [HttpGet("byconcert/{concertId}")]
    public async Task<IActionResult> GetPerformances(int concertId)
    {
        var performances = await _unitOfWork.Performances.Find(p => p.ConcertId == concertId);
        if (performances == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<PerformanceDto>>(performances));
    }

}