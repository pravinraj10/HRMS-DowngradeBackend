using GEOMASTER.DTO.Designation;
using GEOMASTER.Interface.Designation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GEOMASTER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _service;

        public DesignationController(IDesignationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _service.GetById(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDesignationDTO dto)
        {
            await _service.Create(dto);
            return Ok("Created Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDesignationDTO dto)
        {
            await _service.Update(dto);
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Deleted Successfully");
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var result = await _service.Search(keyword);
            return Ok(result);
        }

        [HttpPut("set-active/{id}")]
        public async Task<IActionResult> SetActive(int id, [FromQuery] bool isActive)
        {
            await _service.SetActive(id, isActive);
            return Ok(isActive ? "Activated" : "Deactivated");
        }
    }
}
