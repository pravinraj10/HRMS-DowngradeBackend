using GEOMASTER.DTO.Department;

public interface IDepartmentService
{
    Task<List<DepartmentResponseDTO>> GetAll();
    Task<DepartmentResponseDTO?> GetById(int id);
    Task Create(DepartmentCreateDTO dto);
    Task Update(DepartmentUpdateDTO dto);
    Task Delete(int id);
    Task<List<DepartmentResponseDTO>> Search(string? searchTerm);
    Task SetActive(int id, bool isActive);
}