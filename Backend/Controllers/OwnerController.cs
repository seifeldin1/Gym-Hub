using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
using System.Threading.Tasks;

namespace Backend.Controllers{
    [ApiController]
    [Route("api/Owner")]
    public class OwnerController : ControllerBase{
        private readonly OwnerService ownerServices;
        public OwnerController(OwnerService ownerServices){
            this.ownerServices = ownerServices;
        }
        
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> AddOwner([FromBody] OwnerModel entry)
        {
            
            var result =await ownerServices.AddOwnerAsync(entry);
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
        //[Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteOwner([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Owner ID provided." });
            }
            var result =await ownerServices.DeleteOwnerAsync(entry.id);
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
        //[Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetOwners()
        {
            var ownerlist=await ownerServices.GetOwnersAsync();
            return Ok(ownerlist);
        }

        [HttpPut]
        //[Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateOwner([FromBody] OwnerUpdaterModel entry)
        {
            // Call the service to update the Branch
            var result =await ownerServices.UpdateOwnerAsync(entry);
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

