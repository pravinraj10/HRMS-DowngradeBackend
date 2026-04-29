using GEOMASTER.DTO.BusinessEntity;

namespace GEOMASTER.Interface.BusinessEntity
{
    public interface IBusinessEntityService
    {
        Task CreateEntityAsync(BusinessEntityDTO dto);
    }
}