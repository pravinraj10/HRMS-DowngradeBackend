using GEOMASTER.DTO.Login;
using GEOMASTER.Interface.Login;
using Microsoft.AspNetCore.Mvc;

namespace GEOMASTER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        // =========================
        // LOGIN
        // =========================
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO dto)
        {
            try
            {
                var result = await _service.Login(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // =========================
        // FORGOT PASSWORD
        // =========================
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
            ForgotPasswordDTO dto)
        {
            try
            {
                await _service.ForgotPassword(dto);

                return Ok(new
                {
                    message = "Reset link sent successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // =========================
        // RESET PASSWORD
        // =========================
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
            ResetPasswordDTO dto)
        {
            try
            {
                await _service.ResetPassword(dto);

                return Ok(new
                {
                    message = "Password reset successful"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}