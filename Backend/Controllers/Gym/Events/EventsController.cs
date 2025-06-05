using Backend.Models;
using Backend.DbModels;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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
        //[Authorize(Roles = "Coach , BranchManager, Owner")]
        public async Task<IActionResult> CreateEvent([FromBody] Event events){
            var result = await eventService.AddEventAsync(events);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }


        [HttpPut]
        //[Authorize(Roles = "Coach , BranchManager, Owner")]
        public async Task<IActionResult> UpdateEvent([FromBody] Events events){
            var result = await eventService.UpdateEventAsync(events);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }


        [HttpDelete]
        //[Authorize(Roles = "Coach , BranchManager, Owner")]
        public async Task<IActionResult> DeleteEvent(GetByIDModel getByIDModel)
        {
            var result = await eventService.DeleteEventAsync(getByIDModel.id);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }

        [HttpGet]
        //[Authorize(Roles = "Coach , BranchManager ,Client , Owner")]
        public async Task<IActionResult> GetEvents(){
            var result = await eventService.GetEventsAsync();
            return Ok(result);
        }



    }
}