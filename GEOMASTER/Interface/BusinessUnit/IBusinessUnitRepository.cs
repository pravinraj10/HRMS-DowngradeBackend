using GEOMASTER.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GEOMASTER.Interface.BusinessUnit
{
    public interface IBusinessUnitRepository
    {
        Task<List<Tblbusinessunit>> GetAll();
        Task<Tblbusinessunit?> GetById(int id);
        Task Add(Tblbusinessunit data);
        Task Update(Tblbusinessunit data);
        Task Delete(int id);
    }
}
