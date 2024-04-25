using CourseProjectDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProjectNew.Airplanes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirplaneController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public AirplaneController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet("byAirportAndTime/{time}")]
        public async Task<IActionResult> GetAirplanesByAirportAndTime(TimeOnly time)
        {
            var airplanes = await _context.Airplanes
                .Where(a => a.ArrivalTime <= time && a.DepartureTime > time)
                .OrderBy(a => a.ArrivalTime)
                .ThenByDescending(a => a.FlightsCount)
                .ToListAsync();
            return Ok(airplanes);
        }

    }
}
