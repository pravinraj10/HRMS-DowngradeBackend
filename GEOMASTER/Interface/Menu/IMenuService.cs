using GEOMASTER.DTO.Menu;

namespace GEOMASTER.Interface.Menu
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> GetMenus();
    }
}
