using Information_system_labb3.Models;

namespace Information_system_labb3.Service
{
    public interface IDriverRepo
    {
        // Method to get all drivers
        Task<IEnumerable<Driver>> GetAllDriversAsync();

        // Method to get driver by id
        Task<Driver> GetDriverByIdAsync(int id);

        // Method to add driver
        Task AddDriverAsync(Driver driver);

        // Method to update driver
        Task UpdateDriverAsync(Driver driver);

        // Method to delete driver
        Task DeleteDriverAsync(Driver driver);
    }
}
