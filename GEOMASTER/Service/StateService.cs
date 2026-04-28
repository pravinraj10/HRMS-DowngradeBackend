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

        // GET METHODS

        public List<StateResponseDTO> Search(string? searchTerm)
        {
            var data = _repo.Search(searchTerm ?? string.Empty);
            return data.Select(MapToDTO).ToList();
        }

        public List<StateResponseDTO> GetAllStates()
        {
            return _repo.GetAll().Select(MapToDTO).ToList();
        }

        public StateResponseDTO GetStateById(int id)
        {
            var data = _repo.GetById(id);

            if (data == null)
                throw new KeyNotFoundException($"State with ID {id} was not found.");

            return MapToDTO(data);
        }

        public List<StateResponseDTO> GetStateByCountryId(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("Invalid CountryId.");

            return _repo.GetByCountryId(countryId)
                        .Select(MapToDTO)
                        .ToList();
        }

        // =========================
        // CREATE
        // =========================

        public void CreateState(CreateStateDTO dto)
        {
            ValidateCreate(dto);

            var state = new Tblstate
            {
                CountryId = dto.CountryId,
                StateCode = dto.StateCode.Trim(),
                StateName = dto.StateName.Trim(),
                IsActive = true,
                IsDelete = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy
            };

            _repo.Add(state);
            _repo.Save();
        }

        // =========================
        // UPDATE
        // =========================

        public void UpdateState(UpdateStateDTO dto)
        {
            ValidateUpdate(dto);

            var existing = _repo.GetById(dto.Id);

            if (existing == null)
                throw new KeyNotFoundException($"State with ID {dto.Id} was not found.");

            existing.CountryId = dto.CountryId;
            existing.StateCode = dto.StateCode.Trim();
            existing.StateName = dto.StateName.Trim();
            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = dto.UpdatedBy;

            _repo.Update(existing);
            _repo.Save();
        }

        // =========================
        // DELETE (SAFE CHECK)
        // =========================

        public void DeleteState(int id)
        {
            var existing = _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"State with ID {id} was not found.");

            _repo.Delete(id);
            _repo.Save();
        }

        // =========================
        // ACTIVE STATUS
        // =========================

        public void SetStateActive(int id, bool isActive)
        {
            var existing = _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"State with ID {id} was not found.");

            _repo.SetActive(id, isActive);
            _repo.Save();
        }

        // =========================
        // MAPPER
        // =========================

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

        // =========================
        // VALIDATION
        // =========================

        private void ValidateCreate(CreateStateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.CountryId <= 0)
                throw new ArgumentException("CountryId is required.");

            if (string.IsNullOrWhiteSpace(dto.StateName))
                throw new ArgumentException("StateName is required.");

            if (string.IsNullOrWhiteSpace(dto.StateCode))
                throw new ArgumentException("StateCode is required.");
        }

        private void ValidateUpdate(UpdateStateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Id <= 0)
                throw new ArgumentException("Invalid State Id.");

            if (dto.CountryId <= 0)
                throw new ArgumentException("CountryId is required.");

            if (string.IsNullOrWhiteSpace(dto.StateName))
                throw new ArgumentException("StateName is required.");

            if (string.IsNullOrWhiteSpace(dto.StateCode))
                throw new ArgumentException("StateCode is required.");
        }
    }
}