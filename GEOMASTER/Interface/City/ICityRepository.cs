using GEOMASTER.Models;

namespace GEOMASTER.Interface.City
{
    public interface ICityRepository
    {
        List<Tblcity> Search(string? searchTerm);
        List<Tblcity> GetAll();
        Tblcity GetById(int id);
        List<Tblcity> GetByCountryState(int countryId, int stateId);
        void Add(Tblcity city);
        void Update(Tblcity city);
        void Delete(int id);
        void SetActive(int id, bool isActive);
        void Save();
    }
}
