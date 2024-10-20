using FocusPlanner.Core.Models;
using FocusPlanner.ViewModels;
using System.Windows;

namespace FocusPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _mainViewModel = mainViewModel;

            DataContext = _mainViewModel;

        }

        private void CategoryListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach (Category addedItem in e.AddedItems)
            {
                if (!_mainViewModel.SelectedCategoriesList.Contains(addedItem))
                {
                    _mainViewModel.SelectedCategoriesList.Add(addedItem);
                }
            }

            // Handle removed items
            foreach (Category removedItem in e.RemovedItems)
            {
                if (_mainViewModel.SelectedCategoriesList.Contains(removedItem))
                {
                    _mainViewModel.SelectedCategoriesList.Remove(removedItem);
                }
            }

            // Update tasks based on the selected categories
            _mainViewModel.FilterTasks();
        }

        private void checkComplete_Checked(object sender, RoutedEventArgs e)
        {
            _mainViewModel.FilterTasks();  // Trigger filtering when the checkbox is checked

        }

        private void checkComplete_Unchecked(object sender, RoutedEventArgs e)
        {
            _mainViewModel.FilterTasks();  // Trigger filtering when the checkbox is checked

        }
    }
}