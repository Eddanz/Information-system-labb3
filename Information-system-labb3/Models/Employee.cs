using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Information_system_labb3.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Please enter an email.")]
        public string Email { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public string IdentityUserId { get; set; }
    }
}
