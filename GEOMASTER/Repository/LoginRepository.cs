using GEOMASTER.Interface.Login;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;

namespace GEOMASTER.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;

        public LoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TblLogin?> GetByUsername(string username)
        {
            return await _context.TblLogins
                .FirstOrDefaultAsync(x => x.Username == username && x.IsActive);
        }

        public async Task Add(TblLogin login)
        {
            await _context.TblLogins.AddAsync(login);
            await _context.SaveChangesAsync();
        }
        public async Task<TblLogin?> GetByEmployeeId(int employeeId)
        {
            return await _context.TblLogins
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }
        public async Task Update(TblLogin login)
        {
            _context.TblLogins.Update(login);

            await _context.SaveChangesAsync();
        }
        public async Task<TblLogin?> GetByEmail(string email)
        {
            return await _context.TblLogins
                .FirstOrDefaultAsync(x => x.Username == email);
        }

        public async Task<TblLogin?> GetByResetToken(string token)
        {
            return await _context.TblLogins
                .FirstOrDefaultAsync(x =>
                    x.PasswordResetToken == token);
        }
    }
}
