using CourseProjectDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProjectNew.Flights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public FlightController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet("byCriteria")]
        public async Task<IActionResult> GetFlightsByCriteria(string? origin = null, string? destination = null, TimeSpan? duration = null, decimal? price = null)
        {
            var flightsQuery = _context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(origin) && !string.IsNullOrEmpty(destination))
            {
                flightsQuery = flightsQuery.Where(f => f.Origin == origin && f.Destination == destination);
            }

            if (duration.HasValue)
            {
                flightsQuery = flightsQuery.Where(f => f.FlightPaths.Any(ft => ft.Duration == duration.Value));
            }

            if (price.HasValue)
            {
                flightsQuery = flightsQuery.Where(f => f.Tickets.Any(t => t.Price <= price.Value));
            }

            var flights = await flightsQuery.ToListAsync();
            var count = flights.Count;

            return Ok(new { Flights = flights, Count = count });
        }
        [HttpGet("cancelledFlights")]
        public async Task<IActionResult> GetCancelledFlights(string? origin = null, string? destination = null, int? unusedSeatsCount = null, double? unusedSeatsPercentage = null)
        {
            var flightsQuery = _context.Flights.Where(f => f.FlightStatus == CourseDb.Models.FlightStatus.Canceled);

            if (!string.IsNullOrEmpty(origin) && !string.IsNullOrEmpty(destination))
            {
                flightsQuery = flightsQuery.Where(f => f.Origin == origin && f.Destination == destination);
            }

            if (unusedSeatsCount.HasValue)
            {
                flightsQuery = flightsQuery.Where(f => f.Airplane.SeatsCount - f.Tickets.Count(t => t.Status == CourseProjectDb.Models.TicketStatus.Booked) >= unusedSeatsCount.Value);
            }

            if (unusedSeatsPercentage.HasValue)
            {
                flightsQuery = flightsQuery.Where(f => (double)(f.Airplane.SeatsCount - f.Tickets.Count(t => t.Status == CourseProjectDb.Models.TicketStatus.Booked)) / f.Airplane.SeatsCount * 100 >= unusedSeatsPercentage.Value);
            }

            var flights = await flightsQuery.ToListAsync();
            var count = flights.Count;

            return Ok(new { Flights = flights, Count = count });
        }
        [HttpGet("delayedFlights")]
        public async Task<IActionResult> GetDelayedFlights(string? reason = null, string? origin = null, string? destination = null, DateTime? delayStartTime = null, DateTime? delayEndTime = null)
        {
            var flightsQuery = _context.Flights.Where(f => f.FlightStatus == CourseDb.Models.FlightStatus.Delayed);

            if (!string.IsNullOrEmpty(reason))
            {
                flightsQuery = flightsQuery.Where(f => f.DelayReason == reason);
            }

            if (!string.IsNullOrEmpty(origin) && !string.IsNullOrEmpty(destination))
            {
                flightsQuery = flightsQuery.Where(f => f.Origin == origin && f.Destination == destination);
            }

            if (delayStartTime.HasValue && delayEndTime.HasValue)
            {
                flightsQuery = flightsQuery.Where(f => f.DelayStartTime.HasValue && f.DelayEndTime.HasValue && f.DelayStartTime.Value >= delayStartTime.Value && f.DelayEndTime.Value <= delayEndTime.Value);
            }

            var flights = await flightsQuery.ToListAsync();
            var count = flights.Count;
            var ticketsSoldDuringDelay = flights.Sum(f => f.Tickets.Count(t => t.SoldTime >= f.DelayStartTime && t.SoldTime <= f.DelayEndTime));

            return Ok(new { Flights = flights, Count = count, TicketsSoldDuringDelay = ticketsSoldDuringDelay });
        }




    }
}
