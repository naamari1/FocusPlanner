namespace FocusPlanner.Core.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public DateTime ReminderTime { get; set; }

        // Foreign key for Task
        public int TaskId { get; set; }

        // Navigation property
        public Task Task { get; set; }
    }
}
