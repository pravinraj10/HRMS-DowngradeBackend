using GEOMASTER.DTO.Signup;
using GEOMASTER.Interface.Signup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GEOMASTER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignupController : ControllerBase
    {
        private readonly ISignupService _service;

        public SignupController(ISignupService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignupDTO dto)
        {
            var result = await _service.RegisterAsync(dto);
            return Ok(result);
        }

    }
}