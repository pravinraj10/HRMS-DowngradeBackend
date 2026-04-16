using GEOMASTER.Interface.BusinessEntity;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GEOMASTER.Repository
{
    public class BusinessEntityRepository : IBusinessEntityRepository
    {
        private readonly AppDbContext _context;

        public BusinessEntityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tblbusinessentity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Tblbusinessentities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
