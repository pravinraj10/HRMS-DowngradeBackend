using GEOMASTER.Models;

namespace GEOMASTER.Interface.Department
{
    public interface IDepartmentRepository
    {
        Task<List<Tbldepartment>> GetAll();
        Task<Tbldepartment?> GetById(int id);
        Task Add(Tbldepartment data);
        Task Update(Tbldepartment data);
        Task Delete(int id);
        Task<List<Tbldepartment>> Search(string? searchTerm);
        Task SetActive(int id, bool isActive);
    }
}
