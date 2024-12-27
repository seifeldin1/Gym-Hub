using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers{
    [ApiController]
    [Route("api/Owner")]
    public class OwnerController : ControllerBase{
        private readonly Owner ownerServices;
        public OwnerController(Owner ownerServices){
            this.ownerServices = ownerServices;
        }
        
        [HttpPost("add")]
        public IActionResult AddOwner([FromBody] OwnerModel entry)
        {
            
            var result = ownerServices.AddOwner(entry);
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
        [HttpDelete]
        public IActionResult DeleteOwner([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Owner ID provided." });
            }
            var result = ownerServices.DeleteOwner(entry.id);
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
        [HttpGet]
        public IActionResult GetOwners()
        {
            var ownerlist=ownerServices.GetOwners();
            return Ok(ownerlist);
        }
        [HttpPut]
        public IActionResult UpdateOwner([FromBody] OwnerUpdaterModel entry)
        {
            // Call the service to update the Branch
            var result =ownerServices.UpdateOwner(entry);
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

