namespace Information_system_labb3.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
    }
}
