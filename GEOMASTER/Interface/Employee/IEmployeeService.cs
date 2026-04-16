using GEOMASTER.DTO.Employee;

namespace GEOMASTER.Interface.Employee
{
    public interface IEmployeeService
    {
        Task Create(CreateEmployeeDTO dto);
        Task<List<EmployeeResponseDTO>> GetAll();
        Task<EmployeeResponseDTO?> GetById(int id);
    }
}
