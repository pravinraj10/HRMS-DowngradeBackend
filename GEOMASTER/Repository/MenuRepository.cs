using GEOMASTER.Interface.Menu;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;

namespace GEOMASTER.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;

        public MenuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TblMenu>> GetMenus()
        {
            return await _context.TblMenus
                .Where(x => x.IsActive)
                .Include(x => x.Children)
                .Where(x => x.ParentId == null)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }
    }
}
