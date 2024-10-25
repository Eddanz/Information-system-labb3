using Information_system_labb3.Data;
using Information_system_labb3.Models;
using Microsoft.EntityFrameworkCore;

namespace Information_system_labb3.Service
{
    public class DriverRepository : IDriverRepo
    {
        private readonly AppDbContext _context;
        public DriverRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddDriverAsync(Driver driver)
        {
            await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDriverAsync(Driver driver)
        {
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            return await _context.Drivers.FindAsync(id);
        }

        public async Task UpdateDriverAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
        }
    }
}
