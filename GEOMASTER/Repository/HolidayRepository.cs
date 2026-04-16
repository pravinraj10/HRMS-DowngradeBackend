using GEOMASTER.Interface.Holiday;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class HolidayRepository : IHolidayRepository
{
    private readonly AppDbContext _context;

    public HolidayRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tblholiday>> GetAll()
    {
        return await _context.Tblholidays
            .Where(x => x.IsDelete != true)
            .ToListAsync();
    }

    public async Task<Tblholiday?> GetById(int id)
    {
        return await _context.Tblholidays
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDelete != true);
    }

    public async Task Add(Tblholiday data)
    {
        await _context.Tblholidays.AddAsync(data);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Tblholiday data)
    {
        _context.Tblholidays.Update(data);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var item = await _context.Tblholidays.FindAsync(id);
        if (item != null)
        {
            item.IsDelete = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task SetActive(int id, bool isActive)
    {
        var item = await _context.Tblholidays.FindAsync(id);
        if (item != null)
        {
            item.IsActive = isActive;
            await _context.SaveChangesAsync();
        }
    }
}