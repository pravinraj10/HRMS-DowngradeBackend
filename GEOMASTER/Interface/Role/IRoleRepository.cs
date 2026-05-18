
namespace GEOMASTER.Interface.Role
{
    public interface IRoleRepository
    {
        Task<List<Tblrole>> GetAll();
        Task<Tblrole?> GetById(int id);
        Task Add(Tblrole data);
        Task Update(Tblrole data);
        Task Delete(int id);
        Task SetActive(int id, bool isActive);
        Task<List<Tblrole>> Search(string keyword);
    }
}
