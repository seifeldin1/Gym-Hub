using Microsoft.AspNetCore.Mvc;
using Backend.Services;

namespace Backend.Controllers {
    [ApiController]
    [Route("api/SignUpChecker   ")]
    public class SignUpCheckerController : ControllerBase {
        private readonly SignUpCheckerServices signUpCheckerServices;

        public SignUpCheckerController(SignUpCheckerServices signUpCheckerServices) {
            this.signUpCheckerServices = signUpCheckerServices;
        }

        [HttpGet("CheckEmail")]
        public IActionResult CheckEmail(string email) {
            if (signUpCheckerServices.IsEmailUsed(email)) {
                return Conflict(new { message = "Email is already in use." });
            }
            return Ok(new { message = "Email is available." });
        }

        [HttpGet("CheckUsername")]
        public IActionResult CheckUsername(string username) {
            if (signUpCheckerServices.IsUsernameUsed(username)) {
                return Conflict(new { message = "Username is already in use." });
            }
            return Ok(new { message = "Username is available." });
        }

        [HttpGet("CheckPhoneNumber")]
        public IActionResult CheckPhoneNumber(string phoneNumber) {
            if (signUpCheckerServices.IsPhoneNumberUsed(phoneNumber)) {
                return Conflict(new { message = "Phone number is already in use." });
            }
            return Ok(new { message = "Phone number is available." });
        }

        [HttpGet("CheckNationalNumber")]
        public IActionResult CheckNationalNumber(long nationalNumber) {
            if (signUpCheckerServices.IsNationalNumberUsed(nationalNumber)) {
                return Conflict(new { message = "National number is already in use." });
            }
            return Ok(new { message = "National number is available." });
        }
    }
}
