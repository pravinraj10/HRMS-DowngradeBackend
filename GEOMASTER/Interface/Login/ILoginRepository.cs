using GEOMASTER.Models;

namespace GEOMASTER.Interface.Login
{
    public interface ILoginRepository
    {
        Task<TblLogin?> GetByUsername(string username);
        Task Add(TblLogin login);
        Task<TblLogin?> GetByEmployeeId(int employeeId);
        Task Update(TblLogin login);
        Task<TblLogin?> GetByEmail(string email);
        Task<TblLogin?> GetByResetToken(string token);
    }
}
