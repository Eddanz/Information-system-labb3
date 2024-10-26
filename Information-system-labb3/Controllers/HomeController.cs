using Information_system_labb3.Data;
using Information_system_labb3.Models;
using Information_system_labb3.ViewModels;
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

        public async Task<IActionResult> Index(string searchString, string driverName, string employeeName, DateTime? startDate, DateTime? endDate)
        {
            var driversQuery = _context.Drivers.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                driversQuery = driversQuery.Where(d => d.DriverName.ToLower().Contains(searchString.ToLower()));
            }

            var drivers = await _context.Drivers.ToListAsync();
            var employees = await _context.Employees.ToListAsync();
            var notifications = await _context.Notifications.ToListAsync();

            // Determine the time range based on the user's role
            TimeSpan timeRange;
            if (User.IsInRole("Admin"))
            {
                timeRange = TimeSpan.FromHours(24);
            }
            else
            {
                timeRange = TimeSpan.FromHours(12);
            }

            var recentNotes = await GetRecentNotes(timeRange);

            var historyQuery = _context.Notes.AsQueryable();

            if (!string.IsNullOrEmpty(driverName))
            {
                historyQuery = historyQuery.Where(n => n.Driver.DriverName.ToLower().Contains(driverName.ToLower()));
            }

            if (!string.IsNullOrEmpty(employeeName))
            {
                historyQuery = historyQuery.Where(n => n.Driver.ResponsibleEmployee.ToLower().Contains(employeeName.ToLower()));
            }

            if (startDate.HasValue)
            {
                historyQuery = historyQuery.Where(n => n.NoteDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                historyQuery = historyQuery.Where(n => n.NoteDate <= endDate.Value);
            }

            var history = await historyQuery
                .OrderByDescending(n => n.NoteDate)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                Drivers = string.IsNullOrEmpty(searchString)
                    ? _context.Drivers.ToList()
                    : _context.Drivers.Where(d => d.DriverName.ToLower().Contains(searchString.ToLower())).ToList(),
                Employees = employees,
                Notifications = notifications,
                History = history,
                Notes = recentNotes
            };

            ViewData["searchString"] = searchString;
            ViewData["driverName"] = driverName;
            ViewData["employeeName"] = employeeName;
            ViewData["startDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["endDate"] = endDate?.ToString("yyyy-MM-dd");

            return View(viewModel);
        }

        // GET: Home/Notifications
        public async Task<List<Note>> GetRecentNotes(TimeSpan timeRange)
        {
            var cutoffDate = DateTime.Now.Subtract(timeRange);
            return await _context.Notes
                .Where(n => n.NoteDate >= cutoffDate)
                .Include(n => n.Driver)
                .ToListAsync();
        }

        // GET: Home/History
        [HttpGet]
        public IActionResult FilterHistory(string driverName, string employeeName, DateTime? startDate, DateTime? endDate)
        {
            var history = GetHistory();

            if (!string.IsNullOrEmpty(driverName))
            {
                history = history.Where(h => h.Driver.DriverName.ToLower().Contains(driverName.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(employeeName))
            {
                history = history.Where(h => h.Driver.ResponsibleEmployee.ToLower().Contains(employeeName.ToLower())).ToList();
            }

            if (startDate.HasValue)
            {
                history = history.Where(h => h.NoteDate >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                history = history.Where(h => h.NoteDate <= endDate.Value).ToList();
            }

            return PartialView("_History", history);
        }

        private List<Note> GetHistory()
        {
            // Fetch the history data from the database
            return _context.Notes.Include(n => n.Driver).ToList();
        }
    }
}
