using Information_system_labb3.Models;

namespace Information_system_labb3.Service
{
    public interface IEmployeeRepo
    {
        // Method to get all employees
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        // Method to get employee by id
        Task<Employee> GetEmployeeByIdAsync(int id);

        // Method to add employee
        Task AddEmployeeAsync(Employee employee);

        // Method to update employee
        Task UpdateEmployeeAsync(Employee employee);

        // Method to delete employee
        Task DeleteEmployeeAsync(int employeeId);
    }
}
