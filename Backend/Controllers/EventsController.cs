using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Events")]
    public class EventsController : ControllerBase{
        private readonly EventService eventService;
        public EventsController(EventService eventService){
            this.eventService = eventService;
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] EventModel events){
            var result = eventService.AddEvent(events.events , events.EventID);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }


        [HttpPut]
        public IActionResult UpdateEvent([FromBody] Events events){
            var result = eventService.UpdateEvent(events);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEvent( int id){
            var result = eventService.DeleteEvent(id);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }

        [HttpGet]
        public IActionResult GetEvents(){
            var result = eventService.GetEvents();
            return Ok(result);
        }



    }

    public class EventModel{
        public int EventID { get; set; }
        public Events events { get; set; }
    }
}