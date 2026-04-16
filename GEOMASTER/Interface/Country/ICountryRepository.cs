using GEOMASTER.Models;

namespace GEOMASTER.Interface.Country
{
    public interface ICountryRepository
    {
        List<Tblcountry> Search(string? searchTerm);
        List<Tblcountry> GetAllCountries();
        Tblcountry GetById(int id);
        void Add(Tblcountry country);
        void Update(Tblcountry country);
        void Delete(int id);
        void Disable(int id);
        bool SetActive(int id, bool isActive);
        void Save();
    }
}
