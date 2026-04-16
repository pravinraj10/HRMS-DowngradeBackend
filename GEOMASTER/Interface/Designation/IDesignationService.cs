using GEOMASTER.DTO.Designation;
using GEOMASTER.Models;

namespace GEOMASTER.Interface.Designation
{
    public interface IDesignationService
    {
        Task<List<DesignationResponseDTO>> GetAll();
        Task<DesignationResponseDTO?> GetById(int id);
        Task Create(CreateDesignationDTO dto);
        Task Update(UpdateDesignationDTO dto);
        Task Delete(int id);
        Task SetActive(int id, bool isActive);
        Task<List<DesignationResponseDTO>> Search(string keyword);
    }
}
