using GEOMASTER.DTO.State;
using GEOMASTER.Models;

namespace GEOMASTER.Interface.State
{
    public interface IStateService
    {
        List<StateResponseDTO> Search(string? searchTerm);
        List<StateResponseDTO> GetAllStates();
        StateResponseDTO? GetStateById(int id);
        List<StateResponseDTO> GetStateByCountryId(int countryId);
        void CreateState(CreateStateDTO dto);
        void UpdateState(UpdateStateDTO dto);
        void DeleteState(int id);
        void SetStateActive(int id, bool isActive);
    }
}
