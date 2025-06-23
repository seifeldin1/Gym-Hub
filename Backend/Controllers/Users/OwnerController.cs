using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Owner")]
    public class OwnerController : ControllerBase
    {
        private readonly OwnerService ownerServices;

        public OwnerController(OwnerService ownerServices)
        {
            this.ownerServices = ownerServices;
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> AddOwner([FromBody] OwnerModel entry)
        {
            var result = await ownerServices.AddOwnerAsync(entry);
            return result.success
                ? Ok(new { success = true, message = result.message })
                : BadRequest(new { success = false, message = result.message });
        }

        [HttpDelete]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteOwner([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
                return BadRequest(new { message = "Invalid Owner ID provided." });

            int userId = int.Parse(User.FindFirst("UserID").Value);
            var result = await ownerServices.DeleteOwnerAsync(entry.id, userId);

            return result.success
                ? Ok(new { success = true, message = result.message })
                : BadRequest(new { success = false, message = result.message });
        }

        [HttpPut]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateOwner([FromBody] OwnerUpdaterModel entry)
        {
            var result = await ownerServices.UpdateOwnerAsync(entry);
            return result.success
                ? Ok(new { success = true, message = result.message })
                : BadRequest(new { success = false, message = result.message });
        }

        [HttpGet("all")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetAllOwners()
        {
            int userId = int.Parse(User.FindFirst("UserID").Value);
            var result = await ownerServices.GetOwnersAsync(userId);

            return result.success
                ? Ok(new { success = true, message = result.message, owners = result.owners })
                : Forbid(result.message);
        }

        [HttpGet("me")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetMyDetails()
        {
            int userId = int.Parse(User.FindFirst("UserID").Value);
            var owner = await ownerServices.GetMyOwnerDetailsAsync(userId);

            return owner == null
                ? NotFound(new { success = false, message = "Owner not found" })
                : Ok(new { success = true, owner });
        }

        [HttpPut("update-shares")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateAllOwnerShares([FromBody] List<OwnerShareUpdateModel> updatedOwners)
        {
            var result = await ownerServices.UpdateAllOwnerSharesAsync(updatedOwners);
            return result.success
                ? Ok(new { success = true, message = result.message })
                : BadRequest(new { success = false, message = result.message });
        }
    }
}
