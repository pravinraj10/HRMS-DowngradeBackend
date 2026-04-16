using GEOMASTER.Models;

namespace GEOMASTER.Interface.Employee
{

    public interface IEmployeeRepository
    {
        Task<List<Tblemployee>> GetAll();
        Task<Tblemployee?> GetById(int id);
        Task Add(Tblemployee emp);
    }
}
