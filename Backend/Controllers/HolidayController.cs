using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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
        //[Authorize(Roles = "Coach , BranchManager, Owner")]
        public async Task<IActionResult> AddHoliday([FromBody] DbModels.Holiday holiday){
            var result = await HolidayService.AddHolidayAsync(holiday);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }


        [HttpPut]
        //[Authorize(Roles = "Coach , BranchManager, Owner")]
        public async Task<IActionResult> UpdateHoliday([FromBody] DbModels.Holiday holiday){
            var result = await HolidayService.UpdateHolidayAsync(holiday);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }


        [HttpDelete]
        //[Authorize(Roles = "Coach , BranchManager, Owner")]
        public async Task<IActionResult> DeleteHoliday(GetByIDModel getByIDModel)
        {
            var result = await HolidayService.DeleteHolidayAsync(getByIDModel.id);
            if(result.success) return Ok(new {success = result.success , message = result.message});
            return BadRequest(new {success = result.success , message = result.message});
        }

        [HttpGet]
        [Authorize(Roles = "Coach , BranchManager , Client , Owner")]
        public async Task<IActionResult> GetHolidays(){
            var result = await HolidayService.GetHolidaysAsync();
            return Ok(result);
        }



    }
}