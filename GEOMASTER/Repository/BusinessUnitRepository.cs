using GEOMASTER.Interface.BusinessUnit;
using GEOMASTER.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GEOMASTER.Repository
{
    public class BusinessUnitRepository : IBusinessUnitRepository
    {
        private readonly AppDbContext _context;

        public BusinessUnitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tblbusinessunit>> GetAll()
        {
            return await _context.Tblbusinessunits
                .Where(x => x.IsDelete == false || x.IsDelete == null)
                .ToListAsync();
        }

        public async Task<Tblbusinessunit?> GetById(int id)
        {
            return await _context.Tblbusinessunits
                .FirstOrDefaultAsync(x => x.Id == id && (x.IsDelete == false || x.IsDelete == null));
        }

        public async Task Add(Tblbusinessunit data)
        {
            data.Id = 0;
            await _context.Tblbusinessunits.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Tblbusinessunit data)
        {
            _context.Tblbusinessunits.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _context.Tblbusinessunits.FindAsync(id);
            if (item != null)
            {
                item.IsDelete = true; // Soft delete
                await _context.SaveChangesAsync();
            }
        }
    }
}
