using System.ComponentModel.DataAnnotations;

namespace Information_system_labb3.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        [RegularExpression(@"^[A-Z]{3}[0-9]{3}$", ErrorMessage = "Car Registration must be in the format ABC123.")]
        public string CarReg { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
        public string ResponsibleEmployee { get; set; }
        public decimal TotalAmountOut { get; set; } = 0;
        public decimal TotalAmountIn { get; set; } = 0;

    }
}
