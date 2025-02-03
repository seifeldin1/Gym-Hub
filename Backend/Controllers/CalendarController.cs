using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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
        //[Authorize(Roles = "Owner , BranchManager , Client , Coach")]
        public async Task<IActionResult> GetCalendarBetween([FromBody] TimeFrameModel timeFrame){
            var result = await calendarService.GetCalendarEventsBetweenAsync(timeFrame.Start, timeFrame.End);
            return Ok(result);
        }

        [HttpGet("All")]
        //[Authorize(Roles = "Owner,Coach,Client,BranchManager")]
        public async Task<IActionResult> GetAllCalendarEvents()
        {
            var result = await calendarService.GetAllCalendarEventsAsync();
            return Ok(result);
        }
    }

    public class TimeFrameModel{
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }   
}