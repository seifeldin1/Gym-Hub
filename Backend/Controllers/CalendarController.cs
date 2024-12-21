using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Calendar")]
    public class CalendarController : ControllerBase{
        private readonly CalendarServices calendarService;
        public CalendarController(CalendarServices calendarService){
            this.calendarService = calendarService;
        }

        [HttpGet]
        public IActionResult GetCalendarBetween([FromBody] TimeFrameModel timeFrame){
            var result = calendarService.GetCalendarEventsBetween(timeFrame.Start, timeFrame.End);
            return Ok(result);
        }
    }

    public class TimeFrameModel{
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }   
}