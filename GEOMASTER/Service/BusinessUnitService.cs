using GEOMASTER.DTO.BusinessUnit;
using GEOMASTER.Interface.BusinessUnit;
using GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class BusinessUnitService : IBusinessUnitService
    {
        private readonly IBusinessUnitRepository _repo;

        public BusinessUnitService(IBusinessUnitRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<BusinessUnitResponseDTO>> GetAll()
        {
            var data = await _repo.GetAll();
            return data.Select(MapToDTO).ToList();
        }

        public async Task<BusinessUnitResponseDTO?> GetById(int id)
        {
            var data = await _repo.GetById(id);
            return data == null ? null : MapToDTO(data);
        }

        public async Task Create(CreateBusinessUnitDTO dto)
        {
            var entity = new Tblbusinessunit
            {
                UnitCode = dto.UnitCode,
                UnitName = dto.UnitName,

                IsDelete = false,

                CreatedAt = DateTime.Now,
                CreatedBy = dto.CreatedBy ?? "Admin"
            };

            await _repo.Add(entity);
        }

        public async Task Update(UpdateBusinessUnitDTO dto)
        {
            var existing = await _repo.GetById(dto.Id);
            if (existing == null) return;

            existing.UnitCode = dto.UnitCode;
            existing.UnitName = dto.UnitName;

            existing.UpdatedAt = DateTime.Now;
            existing.UpdatedBy = dto.UpdatedBy ?? "Admin";

            await _repo.Update(existing);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        //  Mapper
        private BusinessUnitResponseDTO MapToDTO(Tblbusinessunit x)
        {
            return new BusinessUnitResponseDTO
            {
                Id = x.Id,
                UnitCode = x.UnitCode,
                UnitName = x.UnitName,

                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy
            };
        }
    }
}