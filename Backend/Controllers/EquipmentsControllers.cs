using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "BranchManager, Owner")]
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
        [Authorize(Roles = "Owner , BranchManager , Coach ")]
        public IActionResult GetEquipments()
        {
            var equipmentList = equipmentsService.GetEquipments();
            return Ok(equipmentList);
        }

       [HttpDelete]
       [Authorize(Roles = "Owner , BranchManager")]
        public IActionResult DeleteEquipment([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Equipment ID provided." });
            }

            var result = equipmentsService.DeleteEquipment(entry.id);
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
        [Authorize(Roles = "Owner , BranchManager")]
        public IActionResult UpdateEquipment([FromBody] EquipmentsModel entry)
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
        [HttpPut("AssignEquipmentToBranch")]
        [Authorize(Roles = "Owner , BranchManager")]
        public IActionResult AssignEquipmentToBranch([FromBody] AssigningModel entry)
        {
            // Call the service to Assign client To coach
            var result = equipmentsService.AssignEquipmentToBranch(entry.Equipment_ID,entry.Branch_ID);            // Return success response after update
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
    public class AssigningModel
    {
        public int Equipment_ID { get; set; }
        public int Branch_ID { get; set; }
    }
}