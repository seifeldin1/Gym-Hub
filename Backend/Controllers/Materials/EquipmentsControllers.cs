using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Equipments")]
    public class EquipmentsController : ControllerBase
    {
        private readonly EquipmentService equipmentsService;
        public EquipmentsController(EquipmentService equipmentsService)
        {
            this.equipmentsService = equipmentsService;
        }
        [HttpPost]
        [Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> AddEquipment([FromBody] EquipmentsModel entry)
        {
            // Call the service method to add the workout
            var result = await equipmentsService.AddEquipmentAsync(entry);
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
        public async Task<IActionResult> GetEquipments()
        {
            var equipmentList = await equipmentsService.GetEquipmentsAsync();
            return Ok(equipmentList);
        }

       [HttpDelete]
       [Authorize(Roles = "Owner , BranchManager")]
        public async Task<IActionResult> DeleteEquipment([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Equipment ID provided." });
            }

            var result = await equipmentsService.DeleteEquipmentAsync(entry.id);
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
        public async Task<IActionResult> UpdateEquipment([FromBody] EquipmentsUpdaterModel entry)
        {
            // Call the service to update the Branch
            var result = await equipmentsService.UpdateEquipmentAsync(entry);
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
        [HttpPut("Assign-Equipment")]
        [Authorize(Roles = "Owner , BranchManager")]
        public async Task<IActionResult> AssignEquipmentToBranch([FromBody] AssigningModel entry)
        {
            // Call the service to Assign client To coach
            var result = await equipmentsService.AssignEquipmentToBranchAsync(entry.Equipment_ID,entry.Branch_ID);            // Return success response after update
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