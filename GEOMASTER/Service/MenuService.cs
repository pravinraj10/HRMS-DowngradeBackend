using GEOMASTER.DTO.Menu;
using GEOMASTER.Interface.Menu;

namespace GEOMASTER.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repo;

        public MenuService(IMenuRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<MenuDTO>> GetMenus()
        {
            var data = await _repo.GetMenus();

            if (data == null)
                throw new Exception("Menu data not found.");

            return data
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new MenuDTO
                {
                    Id = x.Id,
                    Label = x.Label,
                    Icon = x.Icon,
                    Url = x.Url,

                    Children = x.Children == null
                        ? new List<MenuDTO>()
                        : x.Children
                            .OrderBy(c => c.DisplayOrder)
                            .Select(c => new MenuDTO
                            {
                                Id = c.Id,
                                Label = c.Label,
                                Icon = c.Icon,
                                Url = c.Url,
                                Children = new List<MenuDTO>() // prevent null recursion issues
                            })
                            .ToList()
                })
                .ToList();
        }
    }
}