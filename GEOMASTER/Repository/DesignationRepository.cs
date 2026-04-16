using GEOMASTER.Interface.Designation;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;

namespace GEOMASTER.Repository
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly AppDbContext _context;

        public DesignationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tbldesignation>> GetAll()
        {
            return await _context.Tbldesignations
                .Include(x => x.Department) //  join department
                .Where(x => x.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<Tbldesignation?> GetById(int id)
        {
            return await _context.Tbldesignations
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted != true);
        }

        public async Task Add(Tbldesignation data)
        {
            await _context.Tbldesignations.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Tbldesignation data)
        {
            _context.Tbldesignations.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _context.Tbldesignations.FindAsync(id);
            if (item != null)
            {
                item.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task SetActive(int id, bool isActive)
        {
            var item = await _context.Tbldesignations.FindAsync(id);
            if (item != null)
            {
                item.IsActive = isActive;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Tbldesignation>> Search(string keyword)
        {
            return await _context.Tbldesignations
                .Where(x => x.IsDeleted != true &&
                       (x.DesignationName.Contains(keyword)))
                .ToListAsync();
        }
    }
}
