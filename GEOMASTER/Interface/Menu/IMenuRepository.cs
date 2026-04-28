using GEOMASTER.Models;

namespace GEOMASTER.Interface.Menu
{
    public interface IMenuRepository
    {
        Task<List<TblMenu>> GetMenus();
    }
}
