using GEOMASTER.DTO.Holiday;

namespace GEOMASTER.Interface.Holiday
{
    public interface IHolidayService
    {
        Task<List<HolidayResponseDTO>> GetAll();
        Task Create(CreateHolidayDTO dto);
        Task Update(UpdateHolidayDTO dto);
        Task Delete(int id);
        Task SetActive(int id, bool isActive);
    }
}
