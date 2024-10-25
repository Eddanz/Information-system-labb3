namespace Information_system_labb3.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string CarReg { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
        public string ResponsibleEmployee { get; set; }
        public decimal TotalAmountOut { get; set; }
        public decimal TotalAmountIn { get; set; }

    }
}
