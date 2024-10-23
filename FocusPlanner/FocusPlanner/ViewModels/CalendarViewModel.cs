using Syncfusion.UI.Xaml.Scheduler;
using System.Collections.ObjectModel;

namespace FocusPlanner.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        public ObservableCollection<ScheduleAppointment> Appointments { get; set; }

        private readonly MainViewModel mainViewModel;

        public CalendarViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            Appointments = new ObservableCollection<ScheduleAppointment>();



            LoadAppointmentsAsync();
        }

        public async Task LoadAppointmentsAsync()
        {
            await mainViewModel.LoadTasksAsync();  // Ensure tasks are loaded

            // Clear existing appointments to avoid duplicates
            Appointments.Clear();

            // Convert tasks to appointments
            foreach (var task in mainViewModel.Tasks)
            {
                // Create a ScheduleAppointment for the task
                var appointment = new ScheduleAppointment
                {
                    Subject = task.Title,
                    StartTime = task.StartDate ?? DateTime.Now,  // Use StartDate, fallback to now if null
                    EndTime = task.FinishDate ?? task.DueDate ?? DateTime.Now.AddHours(1),  // Use FinishDate or fallback to DueDate or 1 hour from now
                    Notes = task.Description,
                    IsAllDay = false  // Assuming appointments are not all-day events
                };

                Appointments.Add(appointment);
            }
        }

    }
}
