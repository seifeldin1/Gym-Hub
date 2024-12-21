using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Equipments")]
    public class EquipmentsController : ControllerBase
    {
        private readonly Equipments equipmentsService;
        public EquipmentsController(Equipments equipmentsService)
        {
            this.equipmentsService = equipmentsService;
        }
        [HttpPost]
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

            return BadRequest(new
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

        [HttpDelete]
        public IActionResult DeleteEquipment([FromBody] int id)
        {

            var result = equipmentsService.DeleteEquipment(id);
            // Return success response after deletion
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return BadRequest(new
            {
                success = false,
                message = result.message
            });
        }

        [HttpPut]
        public IActionResult UpdateWorkout([FromBody] EquipmentsModel entry)
        {
            // Call the service to update the Branch
            var result = equipmentsService.UpdateEquipment(entry);
            // Return success response after update
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return BadRequest(new
            {
                success = false,
                message = result.message
            });
        }
    }
}