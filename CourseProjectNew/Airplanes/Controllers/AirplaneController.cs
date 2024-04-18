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
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAirplanes()
        {
            var airplanes = await _context.Airplanes.ToListAsync();
            return Ok(airplanes);
        }
        [HttpGet("bytimeandamount")]
        public async Task<IActionResult> ActionResult(TimeOnly time, int amount)
        {
            var airplanes = await _context.Flights.Where(x => x.Airplane.FlightsCount >= amount && x.CurrentFlightTime.ArrivalTime == time).Select(x => x.Airplane).ToListAsync();
            return Ok(airplanes);
        }
    }
}
