using FocusPlanner.ViewModels;
using System.Windows;

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
