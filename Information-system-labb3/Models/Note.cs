using System.ComponentModel.DataAnnotations;

namespace Information_system_labb3.Models
{
    public class Note
    {
        public int NoteId { get; set; }  // Unique identifier for the note
        public int DriverId { get; set; }  // Foreign key to Driver
        [Required(ErrorMessage = "Please select a date.")]
        public DateTime NoteDate { get; set; }
        [Required(ErrorMessage = "Please enter a description.")]
        public string NoteDescription { get; set; }
        [Required(ErrorMessage = "Please enter an amount.")]
        public decimal AmountOut { get; set; }
        [Required(ErrorMessage = "Please enter an amount.")]
        public decimal AmountIn { get; set; }

        // Navigation property to reference the associated Driver
        public Driver Driver { get; set; }
    }
}
