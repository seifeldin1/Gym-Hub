using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Holiday")]
    public class HolidayController : ControllerBase{
        private readonly HolidayService HolidayService;
        public HolidayController(HolidayService HolidayService){
            this.HolidayService = HolidayService;
        }

        [HttpPost]
        public IActionResult AddHoliday([FromBody] Holiday holiday){
            var result = HolidayService.AddHoliday(holiday);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }


        [HttpPut]
        public IActionResult UpdateHoliday([FromBody] Holiday holiday){
            var result = HolidayService.UpdateHoliday(holiday);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }


        [HttpDelete]
        public IActionResult DeleteHoliday(GetByIDModel getByIDModel)
        {
            var result = HolidayService.DeleteHoliday(getByIDModel.id);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }

        [HttpGet]
        public IActionResult GetHolidays(){
            var result = HolidayService.GetHolidays();
            return Ok(result);
        }



    }
}