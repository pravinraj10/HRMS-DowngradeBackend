using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using GEOMASTER.DTO.Department;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _service;

    public DepartmentController(IDepartmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? searchTerm)
    {
        return Ok(await _service.Search(searchTerm));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentCreateDTO dto)
    {
        await _service.Create(dto);
        return Ok("Created Successfully");
    }

    [HttpPut]
    public async Task<IActionResult> Update(DepartmentUpdateDTO dto)
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
        return Ok("Status Updated");
    }
}
