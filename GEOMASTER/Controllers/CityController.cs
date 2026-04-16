using GEOMASTER.DTO.City;
using GEOMASTER.Interface.City;
using GEOMASTER.Models;
using Microsoft.AspNetCore.Mvc;

namespace GEOMASTER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;

        public CityController(ICityService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllCities());
        }

        [HttpGet("search")]
        public IActionResult Search(string? searchTerm)
        {
            return Ok(_service.Search(searchTerm));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _service.GetCityById(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet("by-country-state")]
        public IActionResult GetByCountryState(int countryId, int stateId)
        {
            return Ok(_service.GetCityByCountryState(countryId, stateId));
        }

        [HttpPost]
        public IActionResult Create(CityDTO dto)
        {
            _service.CreateCity(dto);
            return Ok("City Created");
        }

        [HttpPut]
        public IActionResult Update(UpdateCityDTO dto)
        {
            var existing = _service.GetCityById(dto.Id);
            if (existing == null)
                return NotFound();

            _service.UpdateCity(dto);
            return Ok("City Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.DeleteCity(id);
            return Ok("City Deleted");
        }

        [HttpPut("set-active/{id}")]
        public IActionResult SetActive(int id, [FromQuery] bool isActive)
        {
            _service.SetCityActive(id, isActive);
            return Ok(isActive ? "City activated" : "City deactivated");
        }
    }
}
