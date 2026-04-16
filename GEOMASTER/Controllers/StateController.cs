using GEOMASTER.DTO.State;
using GEOMASTER.Interface.State;
using GEOMASTER.Models;
using Microsoft.AspNetCore.Mvc;

namespace GEOMASTER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService _service;

        public StateController(IStateService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllStates());
        }

        [HttpGet("search")]
        public IActionResult Search(string? searchTerm)
        {
            return Ok(_service.Search(searchTerm));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _service.GetStateById(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet("by-country/{countryId}")]
        public IActionResult GetByCountry(int countryId)
        {
            return Ok(_service.GetStateByCountryId(countryId));
        }

        [HttpPost]
        public IActionResult Create(CreateStateDTO dto)
        {
            _service.CreateState(dto);
            return Ok("State Created");
        }

        [HttpPut]
        public IActionResult Update(UpdateStateDTO dto)
        {
            var existing = _service.GetStateById(dto.Id);
            if (existing == null)
                return NotFound();

            _service.UpdateState(dto);
            return Ok("State Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.DeleteState(id);
            return Ok("State Deleted");
        }

        [HttpPut("set-active/{id}")]
        public IActionResult SetActive(int id, [FromQuery] bool isActive)
        {
            _service.SetStateActive(id, isActive);
            return Ok(isActive ? "State activated" : "State deactivated");
        }
    }
}
