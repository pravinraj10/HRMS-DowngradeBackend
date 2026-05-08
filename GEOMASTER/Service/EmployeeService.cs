using GEOMASTER.DTO.Employee;
using GEOMASTER.Interface.Employee;
using GEOMASTER.Interface.Login;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;

namespace GEOMASTER.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IWebHostEnvironment _env;
        private readonly ILoginRepository _loginRepo;

        public EmployeeService(IEmployeeRepository repo, ILoginRepository loginRepo, IWebHostEnvironment env)
        {
            _repo = repo;
            _loginRepo = loginRepo;
            _env = env;
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<List<EmployeeResponseDTO>> GetAll()
        {
            var data = await _repo.GetAll();

            return data.Select(MapToDTO).ToList();
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<EmployeeResponseDTO> GetById(int id)
        {
            var x = await _repo.GetById(id);

            if (x == null)
                throw new KeyNotFoundException($"Employee with ID {id} not found.");

            return MapToDTO(x);
        }

        // =========================
        // CREATE
        // =========================
        public async Task Create(CreateEmployeeDTO dto)
        {
            Validate(dto);

            if (dto.Password != dto.ConfirmPassword)
                throw new Exception("Passwords do not match");

            // Employee code duplicate validation
            var employees = await _repo.GetAll();

            if (employees.Any(x => x.EmployeeCode == dto.EmployeeCode))
            {
                throw new Exception("Employee code already exists.");
            }

            // Step 1: Save employee
            var entity = new Tblemployee
            {
                FullName = dto.FullName.Trim(),
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
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
                ProfilePhoto = SaveFile(dto.ProfilePhoto),
                IdProof = SaveFile(dto.IdProof)
            };

            await _repo.Add(entity);

            // Step 2: Create login (IMPORTANT)
            var login = new TblLogin
            {
                EmployeeId = entity.Id,
                Username = dto.PersonalEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsActive = true
            };

            await _loginRepo.Add(login);
            
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<bool> Update(int id, CreateEmployeeDTO dto)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"Employee with ID {id} not found.");

            Validate(dto);

            existing.FullName = dto.FullName.Trim();
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

            existing.UpdatedAt = DateTime.UtcNow;

            if (dto.ProfilePhoto != null)
                existing.ProfilePhoto = SaveFile(dto.ProfilePhoto);

            if (dto.IdProof != null)
                existing.IdProof = SaveFile(dto.IdProof);

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                var login = await _loginRepo.GetByEmployeeId(existing.Id);

                if (login != null)
                {
                    login.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                    await _loginRepo.Update(login);
                }
            }

            await _repo.Update(existing);
            return true;
        }

        // =========================
        // DELETE
        // =========================
        public async Task<bool> Delete(int id)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"Employee with ID {id} not found.");

            await _repo.Delete(id);
            return true;
        }

        // =========================
        // SEARCH
        // =========================
        public async Task<List<EmployeeResponseDTO>> Search(string? search)
        {
            var data = await _repo.Search(search ?? string.Empty);
            return data.Select(MapToDTO).ToList();
        }

        // =========================
        // ACTIVE STATUS
        // =========================
        public async Task<bool> SetActive(int id, bool isActive)
        {
            // Update employee table
            var result = await _repo.SetActive(id, isActive);

            // Fetch login row
            var login = await _loginRepo.GetByEmployeeId(id);

            // Update login table
            if (login != null)
            {
                login.IsActive = isActive;

                await _loginRepo.Update(login);
            }

            return result;
        }

        // =========================
        // ROLE DROPDOWN
        // =========================
        public async Task<List<RoleDropdownDTO>> GetRolesDropdown()
        {
            var roles = await _repo.GetRolesDropdown();

            return roles.Select(x => new RoleDropdownDTO
            {
                Id = x.Id,
                RoleName = x.RoleName
            }).ToList();
        }

        // =========================
        // FILE UPLOAD (COMMON)
        // =========================
        private string? SaveFile(IFormFile? file)
        {
            if (file == null) return null;

            string uploadsPath = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            return "/uploads/" + fileName;
        }

        // =========================
        // MAPPER (REDUCES DUPLICATION)
        // =========================
        private EmployeeResponseDTO MapToDTO(Tblemployee x)
        {
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

                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department?.DepartmentName,

                DesignationId = x.DesignationId,
                DesignationName = x.Designation?.DesignationName,

                ReportingManagerId = x.ReportingManagerId,
                ReportingManagerName = x.ReportingManager?.RoleName,

                EmployeeCode = x.EmployeeCode,
                JoiningDate = x.JoiningDate,
                Shift = x.Shift,

                ProfilePhoto = x.ProfilePhoto,
                IdProof = x.IdProof,

                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy
            };
        }

        // =========================
        // VALIDATION
        // =========================
        private void Validate(CreateEmployeeDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("FullName is required.");

            if (dto.DepartmentId <= 0)
                throw new ArgumentException("DepartmentId is required.");

            if (dto.DesignationId <= 0)
                throw new ArgumentException("DesignationId is required.");
        }
    }
}