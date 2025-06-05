using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Interests")]
    public class InterestsController : ControllerBase
    {
        private readonly InterestServices _interestServices;

        public InterestsController(InterestServices interestServices)
        {
            _interestServices = interestServices;
        }

        /// <summary>
        /// Adds an interest for the specified client and session.
        /// </summary>
        /// <param name="model">Contains ClientID and SessionID.</param>
        /// <returns>A JSON result indicating success or failure.</returns>
        [HttpPost]
        public async Task<IActionResult> AddToInterests([FromBody] InterestRequestModel model)
        {
            var result = await _interestServices.AddToInterestsAsync(model.ClientID, model.SessionID);
            if (result.success)
                return Ok(new { success = true, message = result.message });
            return BadRequest(new { success = false, message = result.message });
        }

        /// <summary>
        /// Removes an interest for the specified client and session.
        /// </summary>
        /// <param name="model">Contains ClientID and SessionID.</param>
        /// <returns>A JSON result indicating success or failure.</returns>
        [HttpDelete]
        public async Task<IActionResult> RemoveFromInterests([FromBody] InterestRequestModel model)
        {
            var result = await _interestServices.RemoveFromInterestsAsync(model.ClientID, model.SessionID);
            if (result.success)
                return Ok(new { success = true, message = result.message });
            return BadRequest(new { success = false, message = result.message });
        }

        /// <summary>
        /// Retrieves the list of sessions the client is interested in.
        /// </summary>
        /// <param name="clientId">The client's ID.</param>
        /// <returns>A list of interest details.</returns>
        [HttpGet]
        public async Task<IActionResult> ViewMyInterests([FromBody]int clientId)
        {
            var interests = await _interestServices.ViewMyInterestsAsync(clientId);
            return Ok(interests);
        }
    }
    public class InterestRequestModel
    {
        public int ClientID { get; set; }
        public int SessionID { get; set; }
    }
}
