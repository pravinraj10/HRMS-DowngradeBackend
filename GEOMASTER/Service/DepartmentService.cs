using GEOMASTER.DTO.Department;
using GEOMASTER.Interface.Department;
using GEOMASTER.Models;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repo;

    public DepartmentService(IDepartmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<DepartmentResponseDTO>> GetAll()
    {
        var data = await _repo.GetAll();

        return data.Select(x => new DepartmentResponseDTO
        {
            Id = x.Id,
            DepartmentName = x.DepartmentName,
            Description = x.Description,
            IsActive = x.IsActive
        }).ToList();
    }

    public async Task<DepartmentResponseDTO?> GetById(int id)
    {
        var x = await _repo.GetById(id);
        if (x == null) return null;

        return new DepartmentResponseDTO
        {
            Id = x.Id,
            DepartmentName = x.DepartmentName,
            Description = x.Description,
            IsActive = x.IsActive
        };
    }

    public async Task Create(DepartmentCreateDTO dto)
    {
        var entity = new Tbldepartment
        {
            DepartmentName = dto.DepartmentName,
            Description = dto.Description
        };

        await _repo.Add(entity);
    }

    public async Task Update(DepartmentUpdateDTO dto)
    {
        var existing = await _repo.GetById(dto.Id);
        if (existing == null) return;

        existing.DepartmentName = dto.DepartmentName;
        existing.Description = dto.Description;
        existing.IsActive = dto.IsActive;

        await _repo.Update(existing);
    }

    public async Task Delete(int id)
    {
        await _repo.Delete(id);
    }

    public async Task<List<DepartmentResponseDTO>> Search(string? searchTerm)
    {
        var data = await _repo.Search(searchTerm);

        return data.Select(x => new DepartmentResponseDTO
        {
            Id = x.Id,
            DepartmentName = x.DepartmentName,
            Description = x.Description,
            IsActive = x.IsActive
        }).ToList();
    }

    public async Task SetActive(int id, bool isActive)
    {
        await _repo.SetActive(id, isActive);
    }
}
