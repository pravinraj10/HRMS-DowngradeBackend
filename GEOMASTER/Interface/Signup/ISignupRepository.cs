using GEOMASTER.Models;

namespace GEOMASTER.Interface.Signup
{
    public interface ISignupRepository
    {
        Task<Tblsignup> AddAsync(Tblsignup user);
        Task<Tblsignup> GetByEmailAsync(string email);
    }
}
