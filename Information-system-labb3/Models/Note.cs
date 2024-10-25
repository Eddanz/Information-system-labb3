namespace Information_system_labb3.Models
{
    public class Note
    {
        public int NoteId { get; set; }  // Unique identifier for the note
        public int DriverId { get; set; }  // Foreign key to Driver
        public DateTime NoteDate { get; set; }
        public string NoteDescription { get; set; }
        public decimal AmountOut { get; set; }
        public decimal AmountIn { get; set; }

        // Navigation property to reference the associated Driver
        public Driver Driver { get; set; }
    }
}
