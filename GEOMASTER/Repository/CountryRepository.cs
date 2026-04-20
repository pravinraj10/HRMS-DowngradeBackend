
using GEOMASTER.Interface.Country;
using GEOMASTER.Models;

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _context;

    public CountryRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Tblcountry> Search(string? searchTerm)
    {
        var query = _context.Tblcountries.Where(c => c.IsDelete == false);
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(c => c.CountryName!.Contains(searchTerm) || c.CountryCode!.Contains(searchTerm));
        }
        return query.ToList();
    }

    public List<Tblcountry> GetAllCountries()
    {
        return _context.Tblcountries
            .Where(c => c.IsDelete == false)
            .ToList();
    }

    public Tblcountry GetById(int id)
    {
        return _context.Tblcountries
            .FirstOrDefault(c => c.Id == id && c.IsDelete == false)!;
    }

    public void Add(Tblcountry country)
    {
        _context.Tblcountries.Add(country);
    }

    public void Update(Tblcountry country)
    {
        _context.Tblcountries.Update(country);
    }

    public void Delete(int id)
    {
        var data = _context.Tblcountries.Find(id);
        if (data != null)
        {
            data.IsDelete = true;  //soft
        }
    }

    public void Disable(int id)
    {
        var data = _context.Tblcountries.Find(id);
        if (data != null)
        {
            data.IsActive = !(data.IsActive ?? false);
        }
    }

    public bool SetActive(int id, bool isActive)
    {
        var data = _context.Tblcountries.Find(id);
        if (data == null) return false;

        data.IsActive = isActive;
        return true;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}