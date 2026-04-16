using GEOMASTER.DTO.Country;
using GEOMASTER.Interface.Country;
using GEOMASTER.Models;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _service;

    public CountryController(ICountryService service)
    {
        _service = service;
    }

    //  Get All
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAllCountries());
    }

    [HttpGet("search")]
    public IActionResult Search(string? searchTerm)
    {
        return Ok(_service.Search(searchTerm));
    }

    // Get By Id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var data = _service.GetCountryById(id);
        if (data == null)
            return NotFound();

        return Ok(data);
    }

    //  Insert
    [HttpPost]
    public IActionResult CreateCountry(CountryDTO dto)
    {
        _service.CreateCountry(dto);
        return Ok("Created Successfully");
    }
    //update
    [HttpPut]
    public IActionResult UpdateCountry(UpdateCountryDTO dto)
    {
        _service.UpdateCountry(dto);
        return Ok("Updated Successfully");
    }

    // Soft Delete
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.DeleteCountry(id);
        return Ok("Country Deleted");
    }

    //  Activate / Deactivate explicitly
    [HttpPut("set-active/{id}")]
    public IActionResult SetActive(int id, [FromQuery] bool isActive)
    {
        var result = _service.SetCountryActive(id, isActive);
        if (!result) return NotFound("Country not found");
        return Ok(isActive ? "Country activated" : "Country deactivated");
    }
  
}