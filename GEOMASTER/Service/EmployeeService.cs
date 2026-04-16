using GEOMASTER.DTO.Employee;
using GEOMASTER.Interface.Employee;
using GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IWebHostEnvironment _env;

        public EmployeeService(IEmployeeRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<List<EmployeeResponseDTO>> GetAll()
        {
            var data = await _repo.GetAll();

            return data.Select(x => new EmployeeResponseDTO
            {
                Id = x.Id,
                FullName = x.FullName,
                PersonalEmail = x.PersonalEmail,
                PersonalPhone = x.PersonalPhone,
                EmployeeCode = x.EmployeeCode,
                ProfilePhoto = x.ProfilePhoto
            }).ToList();
        }

        public async Task<EmployeeResponseDTO?> GetById(int id)
        {
            var x = await _repo.GetById(id);
            if (x == null) return null;

            return new EmployeeResponseDTO
            {
                Id = x.Id,
                FullName = x.FullName,
                PersonalEmail = x.PersonalEmail,
                PersonalPhone = x.PersonalPhone,
                EmployeeCode = x.EmployeeCode,
                ProfilePhoto = x.ProfilePhoto
            };
        }

        public async Task Create(CreateEmployeeDTO dto)
        {
            string uploadsPath = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            string SaveFile(IFormFile file)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);

                return "/uploads/" + fileName;
            }

            var entity = new Tblemployee
            {
                FullName = dto.FullName,

                //  ADD THESE (Missing fields)
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                PersonalEmail = dto.PersonalEmail,
                PersonalPhone = dto.PersonalPhone,
                EmergencyContact = dto.EmergencyContact,
                Address = dto.Address,

                DepartmentId = dto.DepartmentId,
                DesignationId = dto.DesignationId,
                JoiningDate = dto.JoiningDate,
                EmployeeCode = dto.EmployeeCode,
                ReportingManagerId = dto.ReportingManagerId,
                Shift = dto.Shift,

                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.Now,

                // FILES
                ProfilePhoto = dto.ProfilePhoto != null ? SaveFile(dto.ProfilePhoto) : null,
                IdProof = dto.IdProof != null ? SaveFile(dto.IdProof) : null,
                EmploymentContract = dto.EmploymentContract != null ? SaveFile(dto.EmploymentContract) : null
            };

            await _repo.Add(entity);
        }
    }

}
