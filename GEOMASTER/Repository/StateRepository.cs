using GEOMASTER.Interface.State;
using GEOMASTER.Models;

namespace GEOMASTER.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly AppDbContext _context;

        public StateRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Tblstate> Search(string? searchTerm)
        {
            var query = _context.Tblstates.Where(s => s.IsDelete == false);
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(s => s.StateName!.Contains(searchTerm) || s.StateCode!.Contains(searchTerm));
            }
            return query.ToList();
        }

        public List<Tblstate> GetAll()
        {
            return _context.Tblstates
                .Where(s => s.IsDelete == false)
                .ToList();
        }

        public Tblstate? GetById(int id)
        {
            return _context.Tblstates
                .FirstOrDefault(s => s.Id == id && s.IsDelete == false);
        }

        public List<Tblstate> GetByCountryId(int countryId)
        {
            return _context.Tblstates
                .Where(s => s.CountryId == countryId && s.IsDelete == false)
                .ToList();
        }

        public void Add(Tblstate state)
        {
            _context.Tblstates.Add(state);
        }

        public void Update(Tblstate state)
        {
            _context.Tblstates.Update(state);
        }

        public void Delete(int id)
        {
            var state = _context.Tblstates.Find(id);
            if (state != null)
            {
                if (_context.Tblemployees.Any(e => e.StateId == id && !e.IsDeleted))
                {
                    throw new System.InvalidOperationException("Cannot delete state because employees are assigned to it.");
                }

                state.IsDelete = true;

                //  Delete related cities
                var cities = _context.Tblcities.Where(c => c.StateId == id).ToList();
                foreach (var city in cities)
                {
                    city.IsDelete = true;
                }
            }
        }

        public void SetActive(int id, bool isActive)
        {
            var data = _context.Tblstates.Find(id);
            if (data != null)
            {
                data.IsActive = isActive;
                _context.Tblstates.Update(data);
            }
        }

        public void Save()

        {
            _context.SaveChanges();
        }
    }
}
