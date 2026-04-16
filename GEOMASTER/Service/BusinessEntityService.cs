using GEOMASTER.Interface.BusinessEntity;
using GEOMASTER.Models;
using System.Threading.Tasks;

namespace GEOMASTER.Service
{
    public class BusinessEntityService : IBusinessEntityService
    {
        private readonly IBusinessEntityRepository _repo;

        public BusinessEntityService(IBusinessEntityRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateEntityAsync(Tblbusinessentity entity)
        {
            await _repo.AddAsync(entity);
        }
    }
}
