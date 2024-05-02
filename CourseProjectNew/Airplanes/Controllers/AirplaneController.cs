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
        [HttpGet("byInspectionAndRepair")]
        public async Task<IActionResult> GetAirplanesByInspectionAndRepair(DateTime? startDate = null, DateTime? endDate = null, TimeOnly? repairTime = null, int repairsCount = 0, int flightsCount = 0, int airplaneAge = 0)
        {
            var date = DateOnly.FromDateTime(DateTime.Now.AddYears(-airplaneAge));
            var airplanesQuery = _context.Airplanes.AsQueryable();

            if (startDate.HasValue && endDate.HasValue)
            {
                airplanesQuery = airplanesQuery.Where(a => a.Inspections.Any(ti => ti.BeginTime >= startDate.Value && ti.EndTime <= endDate.Value));;
            }
            if (repairTime.HasValue)
            {
                airplanesQuery = airplanesQuery.Where(a => a.Inspections.Any(x => x.BeginTime == startDate));
            }
            if (repairsCount > 0)
            {
                airplanesQuery = airplanesQuery.Where(a => a.Inspections.Count == repairsCount);
            }

            if (flightsCount > 0)
            {
                airplanesQuery = airplanesQuery.Where(a => a.FlightsCount <= flightsCount);
            }

            if (airplaneAge > 0)
            {
                airplanesQuery = airplanesQuery.Where(a => a.ReleaseDate <= date);
            }

            var airplanes = await airplanesQuery.ToListAsync();
            var count = airplanes.Count;

            return Ok(new { Airplanes = airplanes, Count = count });
        }

    }
}
