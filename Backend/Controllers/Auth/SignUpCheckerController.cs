using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using System.Threading.Tasks;

namespace Backend.Controllers {
    [ApiController]
    [Route("api/SignUpChecker")]
    public class SignUpCheckerController : ControllerBase {
        private readonly SignUpCheckerServices signUpCheckerServices;

        public SignUpCheckerController(SignUpCheckerServices signUpCheckerServices) {
            this.signUpCheckerServices = signUpCheckerServices;
        }

        [HttpGet("Check-Email")]
        public async Task<IActionResult> CheckEmail(string email) {
            if (await signUpCheckerServices.IsEmailUsedAsync(email)) {
                return Conflict(new { message = "Email is already in use." });
            }
            return Ok(new { message = "Email is available." });
        }

        [HttpGet("Check-Username")]
        public async Task<IActionResult> CheckUsername(string username) {
            if (await signUpCheckerServices.IsUsernameUsedAsync(username)) {
                return Conflict(new { message = "Username is already in use." });
            }
            return Ok(new { message = "Username is available." });
        }

        [HttpGet("Check-Phone-Number")]
        public async Task<IActionResult> CheckPhoneNumber(string phoneNumber) {
            if (await signUpCheckerServices.IsPhoneNumberUsedAsync(phoneNumber)) {
                return Conflict(new { message = "Phone number is already in use." });
            }
            return Ok(new { message = "Phone number is available." });
        }

        [HttpGet("Check-National-Number")]
        public async Task<IActionResult> CheckNationalNumber(long nationalNumber) {
            if (await signUpCheckerServices.IsNationalNumberUsedAsync(nationalNumber)) {
                return Conflict(new { message = "National number is already in use." });
            }
            return Ok(new { message = "National number is available." });
        }
    }
}
