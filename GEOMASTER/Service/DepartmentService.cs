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

    // =========================
    // GET ALL
    // =========================
    public async Task<List<DepartmentResponseDTO>> GetAll()
    {
        var data = await _repo.GetAll();

        if (data == null)
            throw new Exception("No department data found.");

        return data.Select(MapToDTO).ToList();
    }

    // =========================
    // GET BY ID
    // =========================
    public async Task<DepartmentResponseDTO> GetById(int id)
    {
        var x = await _repo.GetById(id);

        if (x == null)
            throw new KeyNotFoundException($"Department with ID {id} was not found.");

        return MapToDTO(x);
    }

    // =========================
    // CREATE
    // =========================
    public async Task Create(DepartmentCreateDTO dto)
    {
        ValidateCreate(dto);

        var entity = new Tbldepartment
        {
            DepartmentName = dto.DepartmentName.Trim(),
            Description = dto.Description,
            IsActive = true,
            IsDelete = false,
        };

        await _repo.Add(entity);
    }

    // =========================
    // UPDATE
    // =========================
    public async Task Update(DepartmentUpdateDTO dto)
    {
        ValidateUpdate(dto);

        var existing = await _repo.GetById(dto.Id);

        if (existing == null)
            throw new KeyNotFoundException($"Department with ID {dto.Id} was not found.");

        existing.DepartmentName = dto.DepartmentName.Trim();
        existing.Description = dto.Description;
        existing.IsActive = dto.IsActive;

        existing.UpdatedAt = DateTime.UtcNow;
        existing.UpdatedBy = dto.UpdatedBy;

        await _repo.Update(existing);
    }

    // =========================
    // DELETE
    // =========================
    public async Task Delete(int id)
    {
        var existing = await _repo.GetById(id);

        if (existing == null)
            throw new KeyNotFoundException($"Department with ID {id} was not found.");

        await _repo.Delete(id);
    }

    // =========================
    // SEARCH
    // =========================
    public async Task<List<DepartmentResponseDTO>> Search(string? searchTerm)
    {
        var data = await _repo.Search(searchTerm ?? string.Empty);

        return data.Select(MapToDTO).ToList();
    }

    // =========================
    // ACTIVE STATUS
    // =========================
    public async Task SetActive(int id, bool isActive)
    {
        var existing = await _repo.GetById(id);

        if (existing == null)
            throw new KeyNotFoundException($"Department with ID {id} was not found.");

        await _repo.SetActive(id, isActive);
    }

    // =========================
    // MAPPER
    // =========================
    private DepartmentResponseDTO MapToDTO(Tbldepartment x)
    {
        return new DepartmentResponseDTO
        {
            Id = x.Id,
            DepartmentName = x.DepartmentName,
            Description = x.Description,
            IsActive = x.IsActive
        };
    }

    // =========================
    // VALIDATION
    // =========================
    private void ValidateCreate(DepartmentCreateDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.DepartmentName))
            throw new ArgumentException("DepartmentName is required.");
    }

    private void ValidateUpdate(DepartmentUpdateDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (dto.Id <= 0)
            throw new ArgumentException("Invalid Department Id.");

        if (string.IsNullOrWhiteSpace(dto.DepartmentName))
            throw new ArgumentException("DepartmentName is required.");
    }
}