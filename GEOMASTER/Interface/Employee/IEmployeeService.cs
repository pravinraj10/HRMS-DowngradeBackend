using GEOMASTER.DTO.Employee;

namespace GEOMASTER.Interface.Employee
{
    public interface IEmployeeService
    {
        Task Create(CreateEmployeeDTO dto);
        Task<List<EmployeeResponseDTO>> GetAll();
        Task<EmployeeResponseDTO?> GetById(int id);
        Task<bool> Update(int id, CreateEmployeeDTO dto);
        Task<bool> Delete(int id);
        Task<List<EmployeeResponseDTO>> Search(string? search);
    }
}
