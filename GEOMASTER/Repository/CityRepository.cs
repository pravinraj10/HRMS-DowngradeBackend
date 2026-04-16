using GEOMASTER.Interface.City;
using GEOMASTER.Models;
using System.Collections.Generic;
using System.Linq;

namespace GEOMASTER.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Tblcity> Search(string? searchTerm)
        {
            var query = _context.Tblcities.Where(c => c.IsDelete == false);
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.CityName!.Contains(searchTerm));
            }
            return query.ToList();
        }

        public List<Tblcity> GetAll()
        {
            return _context.Tblcities
                .Where(c => c.IsDelete == false)
                .ToList();
        }

        public Tblcity? GetById(int id)
        {
            return _context.Tblcities
                .FirstOrDefault(c => c.Id == id && c.IsDelete == false);
        }

        public List<Tblcity> GetByCountryState(int countryId, int stateId)
        {
            return _context.Tblcities
                .Where(c => c.CountryId == countryId &&
                            c.StateId == stateId &&
                            c.IsDelete == false)
                .ToList();
        }

        public void Add(Tblcity city)
        {
            _context.Tblcities.Add(city);
        }

        public void Update(Tblcity city)
        {
            _context.Tblcities.Update(city);
        }

        public void Delete(int id)
        {
            var data = _context.Tblcities.Find(id);
            if (data != null)
            {
                data.IsDelete = true;
            }
        }

        public void SetActive(int id, bool isActive)
        {
            var data = _context.Tblcities.Find(id);
            if (data != null)
            {
                data.IsActive = isActive;
                _context.Tblcities.Update(data);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}