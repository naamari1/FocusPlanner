using FocusPlanner.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FocusPlanner.Views
{
    /// <summary>
    /// Interaction logic for AddTaskView.xaml
    /// </summary>
    public partial class AddTaskView : Window
    {
        private readonly AddTaskViewModel _addTaskViewModel;

        public AddTaskView(AddTaskViewModel addTaskViewModel)
        {
            InitializeComponent();
            _addTaskViewModel = addTaskViewModel;
            DataContext = _addTaskViewModel;



            StartDatePicker.SelectedDateChanged += DatePicker_SelectedDateChanged;
            FinishDatePicker.SelectedDateChanged += DatePicker_SelectedDateChanged;
            DueDatePicker.SelectedDateChanged += DatePicker_SelectedDateChanged;

            Loaded += AddTaskView_Loaded;

        }

        private void AddTaskView_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTimeTextBoxState();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTimeTextBoxState();
        }


        private void UpdateTimeTextBoxState()
        {
            StartTimeTextBox.IsEnabled = StartDatePicker.SelectedDate.HasValue;
            FinishTimeTextBox.IsEnabled = FinishDatePicker.SelectedDate.HasValue;
            DueTimeTextBox.IsEnabled = DueDatePicker.SelectedDate.HasValue;
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = await _addTaskViewModel.AddTaskAsync(_addTaskViewModel.SelectedTask);
            if (isSuccess)
            {
                this.Close();
            }
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
