using GEOMASTER.Interface.Role;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;

namespace GEOMASTER.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tblrole>> GetAll()
        {
            return await _context.Tblroles
                .Include(x => x.Employees)
                .Where(x => x.IsDeleted != true)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Tblrole?> GetById(int id)
        {
            return await _context.Tblroles
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.IsDeleted != true);
        }

        public async Task Add(Tblrole data)
        {
            await _context.Tblroles.AddAsync(data);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Tblrole data)
        {
            _context.Tblroles.Update(data);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _context.Tblroles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                item.IsDeleted = true;

                item.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }

        public async Task SetActive(int id, bool isActive)
        {
            var item = await _context.Tblroles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                item.IsActive = isActive;

                item.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Tblrole>> Search(string keyword)
        {
            return await _context.Tblroles
                .Include(x => x.Employees)
                .Where(x =>
                    x.IsDeleted != true &&
                    x.RoleName.Contains(keyword))
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}