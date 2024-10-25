using System.Collections.Generic;
using Information_system_labb3.Models;

namespace Information_system_labb3.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
    }
}
