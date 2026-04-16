using GEOMASTER.Models;

namespace GEOMASTER.Interface.State
{
    public interface IStateRepository
    {
        List<Tblstate> Search(string? searchTerm);
        List<Tblstate> GetAll();
        Tblstate GetById(int id);
        List<Tblstate> GetByCountryId(int countryId);
        void Add(Tblstate state);
        void Update(Tblstate state);
        void Delete(int id);
        void SetActive(int id, bool isActive);
        void Save();
    }
}
