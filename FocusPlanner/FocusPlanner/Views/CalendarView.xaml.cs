using FocusPlanner.ViewModels;
using System.Windows;

namespace FocusPlanner.Views
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : Window
    {
        private CalendarViewModel calendarViewModel;

        public CalendarView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            calendarViewModel = new CalendarViewModel(mainViewModel);
            this.DataContext = calendarViewModel;
        }
    }
}
