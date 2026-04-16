using GEOMASTER.Models;
using System.Threading.Tasks;

namespace GEOMASTER.Interface.BusinessEntity
{
    public interface IBusinessEntityRepository
    {
        Task AddAsync(Tblbusinessentity entity);
    }
}
