using GEOMASTER.DTO.State;
using GEOMASTER.Interface.State;
using GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _repo;

        public StateService(IStateRepository repo)
        {
            _repo = repo;
        }

        public List<StateResponseDTO> Search(string? searchTerm)
        {
            return _repo.Search(searchTerm).Select(MapToDTO).ToList();
        }

        public List<StateResponseDTO> GetAllStates()
        {
            return _repo.GetAll().Select(MapToDTO).ToList();
        }

        public StateResponseDTO? GetStateById(int id)
        {
            var data = _repo.GetById(id);
            return data == null ? null : MapToDTO(data);
        }

        public List<StateResponseDTO> GetStateByCountryId(int countryId)
        {
            return _repo.GetByCountryId(countryId)
                        .Select(MapToDTO)
                        .ToList();
        }

        public void CreateState(CreateStateDTO dto)
        {
            var state = new Tblstate
            {
                CountryId = dto.CountryId,
                StateCode = dto.StateCode,
                StateName = dto.StateName,
                IsActive = true,
                IsDelete = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy
            };

            _repo.Add(state);
            _repo.Save();
        }

        public void UpdateState(UpdateStateDTO dto)
        {
            var existing = _repo.GetById(dto.Id);
            if (existing == null) return;

            existing.CountryId = dto.CountryId;
            existing.StateCode = dto.StateCode;
            existing.StateName = dto.StateName;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = dto.UpdatedBy;

            _repo.Update(existing);
            _repo.Save();
        }

        public void DeleteState(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }

        public void SetStateActive(int id, bool isActive)
        {
            _repo.SetActive(id, isActive);
            _repo.Save();
        }

        //  Mapper (Important)
        private StateResponseDTO MapToDTO(Tblstate s)
        {
            return new StateResponseDTO
            {
                Id = s.Id,
                CountryId = s.CountryId,
                StateCode = s.StateCode,
                StateName = s.StateName,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy
            };
        }
    }
}
