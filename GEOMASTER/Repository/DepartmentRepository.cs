using GEOMASTER.Interface.Department;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;

namespace GEOMASTER.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tbldepartment>> GetAll()
        {
            return await _context.Tbldepartments
                .Where(x => x.IsDelete == false)
                .ToListAsync();
        }

        public async Task<Tbldepartment?> GetById(int id)
        {
            return await _context.Tbldepartments
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDelete == false);
        }

        public async Task Add(Tbldepartment data)
        {
            data.CreatedAt = DateTime.Now;
            data.CreatedBy = "admin";

            await _context.Tbldepartments.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Tbldepartment data)
        {
            data.UpdatedAt = DateTime.Now;
            data.UpdatedBy = "admin";

            _context.Tbldepartments.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _context.Tbldepartments.FindAsync(id);
            if (item != null)
            {
                item.IsDelete = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Tbldepartment>> Search(string? searchTerm)
        {
            var query = _context.Tbldepartments.Where(x => x.IsDelete == false);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => x.DepartmentName.Contains(searchTerm) || (x.Description != null && x.Description.Contains(searchTerm)));
            }

            return await query.ToListAsync();
        }

        public async Task SetActive(int id, bool isActive)
        {
            var item = await _context.Tbldepartments.FindAsync(id);
            if (item != null)
            {
                item.IsActive = isActive;
                await _context.SaveChangesAsync();
            }
        }
    }
}
