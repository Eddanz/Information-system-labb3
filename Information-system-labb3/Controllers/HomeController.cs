using Information_system_labb3.Data;
using Information_system_labb3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Information_system_labb3.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var notifications = await _context.Notifications
                .Where(n => n.EventDate >= DateTime.Now.AddDays(-1))
                .ToListAsync();

            return View(notifications);
        }

        // GET: Home/Notifications
        public async Task<IActionResult> Notifications()
        {
            var notifications = await _context.Notes
                .Where(n => n.NoteDate >= DateTime.Now.AddDays(-1))
                .Include(n => n.Driver)
                .ToListAsync();

            return View(notifications);
        }

        // GET: Home/History
        public async Task<IActionResult> History(string driverName, string employeeName, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Notes.Include(n => n.Driver).AsQueryable();

            if (!string.IsNullOrEmpty(driverName))
            {
                query = query.Where(n => n.Driver.DriverName.Contains(driverName));
            }

            if (!string.IsNullOrEmpty(employeeName))
            {
                query = query.Where(n => n.Driver.ResponsibleEmployee.Contains(employeeName));
            }

            if (startDate.HasValue)
            {
                query = query.Where(n => n.NoteDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(n => n.NoteDate <= endDate.Value);
            }

            var history = await query.ToListAsync();
            return View(history);
        }
    }
}
