using GEOMASTER.Models;

namespace GEOMASTER.Interface.Login
{
    public interface ILoginRepository
    {
        Task<TblLogin?> GetByUsername(string username);
        Task Add(TblLogin login);
    }
}
