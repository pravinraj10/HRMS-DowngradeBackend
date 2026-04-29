using GEOMASTER.DTO.BusinessEntity;
using GEOMASTER.Interface.BusinessEntity;
using GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class BusinessEntityService
        : IBusinessEntityService
    {
        private readonly IBusinessEntityRepository _repo;
        private readonly IWebHostEnvironment _env;

        public BusinessEntityService(
            IBusinessEntityRepository repo,
            IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task CreateEntityAsync(
            BusinessEntityDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(
                    nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException(
                    "Business name is required.");

            string filePath = string.Empty;

            if (dto.Logo != null)
            {
                filePath = await SaveFile(dto.Logo);
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
                Website = dto.Website
            };

            await _repo.AddAsync(entity);
        }

        private async Task<string> SaveFile(
            IFormFile file)
        {
            if (file.Length == 0)
                throw new ArgumentException(
                    "Invalid file.");

            var folderPath = Path.Combine(
                _env.WebRootPath,
                "uploads");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName =
                Guid.NewGuid() +
                Path.GetExtension(file.FileName);

            var fullPath = Path.Combine(
                folderPath,
                fileName);

            using var stream =
                new FileStream(
                    fullPath,
                    FileMode.Create);

            await file.CopyToAsync(stream);

            return "/uploads/" + fileName;
        }
    }
}