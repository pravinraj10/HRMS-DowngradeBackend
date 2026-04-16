using GEOMASTER.DTO.BusinessEntity;
using GEOMASTER.Interface.BusinessEntity;
using GEOMASTER.Models;
using Microsoft.AspNetCore.Mvc;

namespace GEOMASTER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessEntityController : ControllerBase
    {
        private readonly IBusinessEntityService _service;

        public BusinessEntityController(IBusinessEntityService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BusinessEntityDTO dto)
        {
            string filePath = "";

            if (dto.Logo != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Logo.FileName);
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.Logo.CopyToAsync(stream);
                }

                filePath = "/uploads/" + fileName;
            }

            var entity = new Tblbusinessentity
            {
                Name = dto.Name,
                Email = dto.Email,
                Logo = filePath,
                Mobile = dto.Mobile,
                Telephone = dto.Telephone,
                Address1 = dto.Address1,
                Address2 = dto.Address2,
                Address3 = dto.Address3,
                Pincode = dto.Pincode,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                Gst = dto.Gst,
                Website = dto.Website,
            };

            await _service.CreateEntityAsync(entity);

            return Ok(new { message = "Business Entity created" });
        }
    }
}
