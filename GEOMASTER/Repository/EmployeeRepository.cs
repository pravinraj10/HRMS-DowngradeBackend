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
            return await _context.Tblemployees
            .Where(x => !x.IsDeleted)
            .Include(x => x.Country)
            .Include(x => x.State)
            .Include(x => x.City)
            .Include(x => x.Department)
            .Include(x => x.Designation)
            .Include(x => x.ReportingManager)
            .Include(x => x.Role)
            .ToListAsync();
        }
        public async Task<Tblemployee?> GetById(int id)
        {
            return await _context.Tblemployees
           .Include(x => x.Country)
           .Include(x => x.State)
           .Include(x => x.City)
           .Include(x => x.Department)
           .Include(x => x.Designation)
           .Include(x => x.ReportingManager)
           .Include(x => x.Role)
           .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task Add(Tblemployee emp)
        {
            await _context.AddAsync(emp);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Tblemployee emp)
        {
            _context.Update(emp);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<Tblemployee>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Tblemployee>> Search(string? search)
        {
            var query = _context.Tblemployees
                .Where(x => !x.IsDeleted)
                .Include(x => x.Department)
                .Include(x => x.Designation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();

                query = query.Where(x =>
                (x.FullName != null && x.FullName.ToLower().Contains(search)) ||
                (x.EmployeeCode != null && x.EmployeeCode.ToLower().Contains(search)) ||
                (x.PersonalEmail != null && x.PersonalEmail.ToLower().Contains(search)) ||
                (x.PersonalPhone != null && x.PersonalPhone.Contains(search))
                );
            }

            return await query.ToListAsync();
        }
        public async Task<bool> SetActive(int id, bool isActive)
        {
            var emp = new Tblemployee
            {
                Id = id,
                IsActive = isActive
            };

            _context.Tblemployees.Attach(emp);

            _context.Entry(emp).Property(x => x.IsActive).IsModified = true;

            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }
        public async Task<List<Tblrole>> GetRolesDropdown()
        {
            return await _context.Tblroles
                .Where(x => x.IsActive && !x.IsDeleted)
                .ToListAsync();
        }
     

    }
}
