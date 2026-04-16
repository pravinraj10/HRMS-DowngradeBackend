using GEOMASTER.Models;
using System.Threading.Tasks;

namespace GEOMASTER.Interface.BusinessEntity
{
    public interface IBusinessEntityService
    {
        Task CreateEntityAsync(Tblbusinessentity entity);
    }
}
