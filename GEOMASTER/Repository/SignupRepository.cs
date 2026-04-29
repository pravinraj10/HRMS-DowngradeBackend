using GEOMASTER.Interface.Signup;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;

public class SignupRepository : ISignupRepository
{
    private readonly AppDbContext _context;

    public SignupRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Tblsignup> AddAsync(Tblsignup user)
    {
        await _context.Tblsignup.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<Tblsignup> GetByEmailAsync(string email)
    {
        return await _context.Tblsignup
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}