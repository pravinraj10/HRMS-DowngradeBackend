using GEOMASTER.Models;

namespace GEOMASTER.Interface.Designation
{
    public interface IDesignationRepository
    {
        Task<List<Tbldesignation>> GetAll();
        Task<Tbldesignation?> GetById(int id);
        Task Add(Tbldesignation data);
        Task Update(Tbldesignation data);
        Task Delete(int id);
        Task SetActive(int id, bool isActive);
        Task<List<Tbldesignation>> Search(string keyword);
    }
}
