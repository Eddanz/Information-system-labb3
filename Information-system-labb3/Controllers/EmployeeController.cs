using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Information_system_labb3.Data;
using Information_system_labb3.Models;
using Microsoft.AspNetCore.Identity;
using Information_system_labb3.Service;

namespace Information_system_labb3.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeeController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Employees.Include(e => e.IdentityUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,Email")] Employee employee, string password)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = employee.Email, Email = employee.Email };
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    employee.IdentityUserId = user.Id;
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employee.IdentityUserId);
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,Email,IdentityUserId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the existing employee
                    var existingEmployee = await _context.Employees
                        .Include(e => e.IdentityUser)
                        .FirstOrDefaultAsync(e => e.EmployeeId == id);

                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }

                    // Update the employee properties
                    existingEmployee.EmployeeName = employee.EmployeeName;
                    existingEmployee.Email = employee.Email;

                    // Update the associated IdentityUser
                    if (existingEmployee.IdentityUser != null)
                    {
                        existingEmployee.IdentityUser.UserName = employee.Email;
                        existingEmployee.IdentityUser.NormalizedUserName = employee.Email.ToUpper();
                        existingEmployee.IdentityUser.Email = employee.Email;
                        existingEmployee.IdentityUser.NormalizedEmail = employee.Email.ToUpper();

                        var result = await _userManager.UpdateAsync(existingEmployee.IdentityUser);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(employee);
                        }
                    }

                    // Update the employee in the database
                    _context.Update(existingEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employee.IdentityUserId);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, [FromServices] UserManager<IdentityUser> userManager)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var employee = await _context.Employees
                    .Include(e => e.IdentityUser)
                    .FirstOrDefaultAsync(e => e.EmployeeId == id);

                if (employee == null)
                {
                    return NotFound();
                }

                // Remove the IdentityUser if it exists
                if (employee.IdentityUser != null)
                {
                    var identityUserToDelete = await userManager.FindByIdAsync(employee.IdentityUserId);

                    if (identityUserToDelete != null)
                    {
                        var result = await userManager.DeleteAsync(identityUserToDelete);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(employee);
                        }
                    }
                }

                // Remove the Employee
                _context.Employees.Remove(employee);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                await transaction.RollbackAsync();
                // Log the exception (logging mechanism not shown here)
                ModelState.AddModelError(string.Empty, "Error deleting employee and associated IdentityUser: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
