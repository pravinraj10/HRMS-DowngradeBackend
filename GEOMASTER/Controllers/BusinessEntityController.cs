using GEOMASTER.DTO.BusinessEntity;
using GEOMASTER.Interface.BusinessEntity;
using Microsoft.AspNetCore.Mvc;

namespace GEOMASTER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessEntityController : ControllerBase
    {
        private readonly IBusinessEntityService _service;

        public BusinessEntityController(
            IBusinessEntityService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] BusinessEntityDTO dto)
        {
            if (dto == null)
                return BadRequest("Request cannot be empty.");

            await _service.CreateEntityAsync(dto);

            return Created(
                string.Empty,
                new
                {
                    message = "Business Entity created successfully"
                });
        }
    }
}