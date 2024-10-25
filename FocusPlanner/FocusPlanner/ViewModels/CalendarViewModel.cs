﻿using FocusPlanner.Core.Enum;
using Syncfusion.UI.Xaml.Scheduler;
using System.Collections.ObjectModel;
using System.Windows.Media;

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
                    StartTime = task.StartDate ?? DateTime.Now,
                    EndTime = task.FinishDate ?? task.DueDate ?? DateTime.Now.AddHours(1),
                    Notes = task.Description,
                    IsAllDay = false
                };


                switch (task.Priority)
                {
                    case Priority.Low:
                        appointment.AppointmentBackground = new SolidColorBrush(Colors.LightGreen);
                        break;
                    case Priority.Medium:
                        appointment.AppointmentBackground = new SolidColorBrush(Colors.Yellow);
                        break;
                    case Priority.High:
                        appointment.AppointmentBackground = new SolidColorBrush(Colors.Red);
                        break;
                }

                appointment.Foreground = new SolidColorBrush(Colors.Black);

                Appointments.Add(appointment);
            }
        }


    }
}
