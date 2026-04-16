using GEOMASTER.DTO.Holiday;
using GEOMASTER.Interface.Holiday;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GEOMASTER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _service;

        public HolidayController(IHolidayService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHolidayDTO dto)
        {
            await _service.Create(dto);
            return Ok("Holiday Created");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateHolidayDTO dto)
        {
            await _service.Update(dto);
            return Ok("Holiday Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Deleted");
        }

        [HttpPut("set-active/{id}")]
        public async Task<IActionResult> SetActive(int id, [FromQuery] bool isActive)
        {
            await _service.SetActive(id, isActive);
            return Ok(isActive ? "Holiday Activated" : "Holiday Deactivated");
        }
    }
}
