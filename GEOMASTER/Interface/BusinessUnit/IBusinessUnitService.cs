using GEOMASTER.DTO.BusinessUnit;

public interface IBusinessUnitService
{
    Task<List<BusinessUnitResponseDTO>> GetAll();
    Task<BusinessUnitResponseDTO?> GetById(int id);

    Task Create(CreateBusinessUnitDTO dto);
    Task Update(UpdateBusinessUnitDTO dto);

    Task Delete(int id);
}
