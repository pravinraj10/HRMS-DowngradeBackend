using GEOMASTER.DTO.Designation;
using GEOMASTER.Interface.Designation;
using GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _repo;

        public DesignationService(IDesignationRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<DesignationResponseDTO>> GetAll()
        {
            var data = await _repo.GetAll();
            return data.Select(x => MapToDTO(x)).ToList();
        }

        public async Task<DesignationResponseDTO?> GetById(int id)
        {
            var data = await _repo.GetById(id);
            if (data == null) return null;

            return MapToDTO(data);
        }

        public async Task Create(CreateDesignationDTO dto)
        {
            var entity = new Tbldesignation
            {
                DesignationName = dto.DesignationName,
                DepartmentId = dto.DepartmentId,
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            await _repo.Add(entity);
        }

        public async Task Update(UpdateDesignationDTO dto)
        {
            var existing = await _repo.GetById(dto.Id);
            if (existing == null) return;

            existing.DesignationName = dto.DesignationName;
            existing.DepartmentId = dto.DepartmentId;
            existing.Description = dto.Description;
            existing.UpdatedBy = dto.UpdatedBy;
            existing.UpdatedAt = DateTime.Now;

            await _repo.Update(existing);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task SetActive(int id, bool isActive)
        {
            await _repo.SetActive(id, isActive);
        }

        public async Task<List<DesignationResponseDTO>> Search(string keyword)
        {
            var data = await _repo.Search(keyword);
            return data.Select(x => MapToDTO(x)).ToList();
        }

        //  Mapper Method
        private DesignationResponseDTO MapToDTO(Tbldesignation x)
        {
            return new DesignationResponseDTO
            {
                Id = x.Id,
                DesignationName = x.DesignationName,
                DepartmentId = x.DepartmentId,
                Description = x.Description,
                IsActive = x.IsActive == true,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            };
        }
    }
}