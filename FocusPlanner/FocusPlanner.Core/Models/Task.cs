using FocusPlanner.Core.Enum;

namespace FocusPlanner.Core.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        // Foreign key for Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Navigation property for reminders
        public ICollection<Reminder> Reminders { get; set; }
        public Priority Priority { get; set; }

    }
}
