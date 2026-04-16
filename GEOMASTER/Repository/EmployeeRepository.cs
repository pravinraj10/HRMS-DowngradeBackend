using GEOMASTER.Interface.Employee;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;

namespace GEOMASTER.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tblemployee>> GetAll()
        {
            return await _context.Set<Tblemployee>().ToListAsync();
        }

        public async Task<Tblemployee?> GetById(int id)
        {
            return await _context.Set<Tblemployee>().FindAsync(id);
        }

        public async Task Add(Tblemployee emp)
        {
            await _context.AddAsync(emp);
            await _context.SaveChangesAsync();
        }
    }
}
