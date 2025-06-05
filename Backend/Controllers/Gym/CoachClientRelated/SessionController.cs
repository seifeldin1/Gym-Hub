using Backend.DbModels;   // For the Session EF entity
using Backend.Services;     // For the SessionService
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Session")]
    public class SessionController : ControllerBase
    {
        private readonly SessionService _sessionService;

        public SessionController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// Adds a new session.
        /// </summary>
        /// <param name="session">The session data.</param>
        /// <returns>A JSON result indicating success or failure.</returns>
        [HttpPost]
        public async Task<IActionResult> AddSession([FromBody] Session session)
        {
            var result = await _sessionService.AddSessionAsync(session);
            if (result.success)
                return Ok(new { success = true, message = result.message });
            return BadRequest(new { success = false, message = result.message });
        }

        /// <summary>
        /// Retrieves a session by its ID.
        /// </summary>
        /// <param name="id">The session ID.</param>
        /// <returns>The session data.</returns>
        [HttpGet]
        public async Task<IActionResult> GetSessionById([FromBody] GetByIDModel entry)
        {
            var session = await _sessionService.GetSessionByIdAsync(entry.id);
            if (session == null)
                return NotFound(new { success = false, message = "Session not found" });
            return Ok(session);
        }

        /// <summary>
        /// Retrieves all sessions.
        /// </summary>
        /// <returns>A list of sessions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSessions()
        {
            var sessions = await _sessionService.GetAllSessionsAsync();
            return Ok(sessions);
        }

        /// <summary>
        /// Updates an existing session.
        /// </summary>
        /// <param name="session">The session data to update. Session_ID is required.</param>
        /// <returns>A JSON result indicating success or failure.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateSession([FromBody] Session session)
        {
            var result = await _sessionService.UpdateSessionAsync(session);
            if (result.success)
                return Ok(new { success = true, message = result.message });
            return BadRequest(new { success = false, message = result.message });
        }

        /// <summary>
        /// Deletes a session by its ID.
        /// </summary>
        /// <param name="id">The session ID.</param>
        /// <returns>A JSON result indicating success or failure.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteSession([FromBody] GetByIDModel entry)
        {
            var result = await _sessionService.DeleteSessionAsync(entry.id);
            if (result.success)
                return Ok(new { success = true, message = result.message });
            return BadRequest(new { success = false, message = result.message });
        }
    }
}
