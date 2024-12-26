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
        public IActionResult CreateEvent([FromBody] Events events){
            var result = eventService.AddEvent(events);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }


        [HttpPut]
        public IActionResult UpdateEvent([FromBody] Events events){
            var result = eventService.UpdateEvent(events);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }


        [HttpDelete]
        public IActionResult DeleteEvent(GetByIDModel getByIDModel)
        {
            var result = eventService.DeleteEvent(getByIDModel.id);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }

        [HttpGet]
        public IActionResult GetEvents(){
            var result = eventService.GetEvents();
            return Ok(result);
        }



    }
}