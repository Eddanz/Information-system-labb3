using Microsoft.AspNetCore.Identity;

namespace Information_system_labb3.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public string IdentityUserId { get; set; }
    }
}
