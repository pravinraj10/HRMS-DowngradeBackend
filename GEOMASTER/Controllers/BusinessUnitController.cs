using GEOMASTER.DTO.BusinessUnit;
using GEOMASTER.Interface.BusinessUnit;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BusinessUnitController : ControllerBase
{
    private readonly IBusinessUnitService _service;

    public BusinessUnitController(IBusinessUnitService service)
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
    public async Task<IActionResult> Create(CreateBusinessUnitDTO dto)
    {
        await _service.Create(dto);
        return Ok("Created successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBusinessUnitDTO dto)
    {
        if (id != dto.Id)
            return BadRequest("Id mismatch");

        await _service.Update(dto);
        return Ok("Updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok("Deleted successfully");
    }
}