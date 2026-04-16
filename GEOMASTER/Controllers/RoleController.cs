using GEOMASTER.DTO.Roles;
using GEOMASTER.Interface.Role;
using Microsoft.AspNetCore.Mvc;

namespace GEOMASTER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
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
        public async Task<IActionResult> Create(CreateRoleDTO dto)
        {
            await _service.Create(dto);
            return Ok("Created Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleDTO dto)
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

        [HttpPut("set-active/{id}")]
        public async Task<IActionResult> SetActive(int id, [FromQuery] bool isActive)
        {
            await _service.SetActive(id, isActive);
            return Ok(isActive ? "Activated" : "Deactivated");
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            return Ok(await _service.Search(keyword));
        }
    }
}
