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
                Gender = x.Gender,
                DateOfBirth = x.DateOfBirth,

                PersonalEmail = x.PersonalEmail,
                PersonalPhone = x.PersonalPhone,
                EmergencyContact = x.EmergencyContact,
                Address = x.Address,

                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department?.DepartmentName,

                DesignationId = x.DesignationId,
                DesignationName = x.Designation?.DesignationName,

                JoiningDate = x.JoiningDate,
                EmployeeCode = x.EmployeeCode,
                ReportingManagerId = x.ReportingManagerId,
                Shift = x.Shift,

                ProfilePhoto = x.ProfilePhoto,
                IdProof = x.IdProof,

                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy
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
                Gender = x.Gender,
                DateOfBirth = x.DateOfBirth,

                PersonalEmail = x.PersonalEmail,
                PersonalPhone = x.PersonalPhone,
                EmergencyContact = x.EmergencyContact,
                Address = x.Address,

                EmployeeCode = x.EmployeeCode,
                JoiningDate = x.JoiningDate,
                Shift = x.Shift,

                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department?.DepartmentName,

                DesignationId = x.DesignationId,
                DesignationName = x.Designation?.DesignationName,

                ReportingManagerId = x.ReportingManagerId,

                ProfilePhoto = x.ProfilePhoto,
                IdProof = x.IdProof,

                CreatedBy = x.CreatedBy
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
                IsActive = true,
                IsDeleted = false,

                // FILES
                ProfilePhoto = dto.ProfilePhoto != null ? SaveFile(dto.ProfilePhoto) : null,
                IdProof = dto.IdProof != null ? SaveFile(dto.IdProof) : null,
       
            };

            await _repo.Add(entity);
        }
        public async Task<bool> Update(int id, CreateEmployeeDTO dto)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return false;

            existing.FullName = dto.FullName;
            existing.Gender = dto.Gender;
            existing.DateOfBirth = dto.DateOfBirth;
            existing.PersonalEmail = dto.PersonalEmail;
            existing.PersonalPhone = dto.PersonalPhone;
            existing.EmergencyContact = dto.EmergencyContact;
            existing.Address = dto.Address;

            existing.DepartmentId = dto.DepartmentId;
            existing.DesignationId = dto.DesignationId;
            existing.JoiningDate = dto.JoiningDate;
            existing.EmployeeCode = dto.EmployeeCode;
            existing.ReportingManagerId = dto.ReportingManagerId;
            existing.Shift = dto.Shift;

            existing.UpdatedAt = DateTime.Now;

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

            //  Update files only if new ones provided
            if (dto.ProfilePhoto != null)
                existing.ProfilePhoto = SaveFile(dto.ProfilePhoto);

            if (dto.IdProof != null)
                existing.IdProof = SaveFile(dto.IdProof);

            await _repo.Update(existing);
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return false;

            await _repo.Delete(id);
            return true;
        }
        public async Task<List<EmployeeResponseDTO>> Search(string? search)
        {
            var data = await _repo.Search(search);

            return data.Select(x => new EmployeeResponseDTO
            {
                Id = x.Id,
                FullName = x.FullName,
                Gender = x.Gender,
                DateOfBirth = x.DateOfBirth,

                PersonalEmail = x.PersonalEmail,
                PersonalPhone = x.PersonalPhone,
                EmergencyContact = x.EmergencyContact,
                Address = x.Address,

                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department?.DepartmentName,

                DesignationId = x.DesignationId,
                DesignationName = x.Designation?.DesignationName,

                JoiningDate = x.JoiningDate,
                EmployeeCode = x.EmployeeCode,
                ReportingManagerId = x.ReportingManagerId,
                Shift = x.Shift,

                ProfilePhoto = x.ProfilePhoto,
                IdProof = x.IdProof,

                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy
            }).ToList();
        }
        public async Task<bool> SetActive(int id, bool isActive)
        {
            return await _repo.SetActive(id, isActive);
        }
    }

}
