using GEOMASTER.DTO.Employee;
using GEOMASTER.Interface.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GEOMASTER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetById(id);
            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateEmployeeDTO dto)
        {
            await _service.Create(dto);
            return Ok("Created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateEmployeeDTO dto)
        {
            var result = await _service.Update(id, dto);
            if (!result) return NotFound();

            return Ok("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result) return NotFound();

            return Ok("Deleted Successfully");
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? search)
        {
            var result = await _service.Search(search);
            return Ok(result);
        }
        [HttpPut("set-active/{id}")]
        public async Task<IActionResult> SetActive(int id, [FromQuery] bool isActive)
        {
            var result = await _service.SetActive(id, isActive);
            if (!result) return NotFound();

            return Ok("Status updated");
        }
    }
}
