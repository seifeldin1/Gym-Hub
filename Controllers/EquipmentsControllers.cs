using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/EquipmentsModel")]
    public class EquipmentsController : ControllerBase
    {
        private readonly Equipments equipmentsService;
        public EquipmentsController(Equipments equipmentsService)
        {
            this.equipmentsService = equipmentsService;
        }
        [HttpPost("add")]
        public IActionResult AddEquipment([FromBody] EquipmentsModel entry)
        {
            // Call the service method to add the workout
            var result = equipmentsService.AddEquipments(entry);
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return Unauthorized(new
            {
                success = false,
                message = result.message
            });
            // Return the JSON result
        }



        [HttpGet]
        public IActionResult GetEquipments()
        {
            var equipmentList = equipmentsService.GetEquipments();
            return Ok(equipmentList);
        }
    }
}