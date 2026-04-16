using GEOMASTER.Models;

namespace GEOMASTER.Interface.Holiday
{
    public interface IHolidayRepository
    {
        Task<List<Tblholiday>> GetAll();
        Task<Tblholiday?> GetById(int id);
        Task Add(Tblholiday data);
        Task Update(Tblholiday data);
        Task Delete(int id);
        Task SetActive(int id, bool isActive);
    }
}
