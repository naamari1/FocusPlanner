using FocusPlanner.Command;
using FocusPlanner.Core.Interfaces;
using FocusPlanner.Core.Models;
using FocusPlanner.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FocusPlanner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ITaskRepository TaskRepository => _taskRepository; // Publieke eigenschap voor TaskRepository


        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));  // Notify the UI when Categories changes
            }
        }

        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                OnPropertyChanged(nameof(SearchTerm));
                FilterTasks();  // Roep filteren aan elke keer dat de zoekterm verandert
            }
        }

        public ObservableCollection<Core.Models.Task> Tasks { get; set; }

        private ObservableCollection<Category> _selectedCategoriesList;
        public ObservableCollection<Category> SelectedCategoriesList
        {
            get => _selectedCategoriesList;
            set
            {
                _selectedCategoriesList = value;
                OnPropertyChanged(nameof(SelectedCategoriesList));
            }
        }

        private bool _showCompletedTasks;
        public bool ShowCompletedTasks
        {
            get => _showCompletedTasks;
            set
            {
                _showCompletedTasks = value;
                OnPropertyChanged(nameof(ShowCompletedTasks));
                FilterTasks();  // Roep opnieuw de filter aan wanneer de waarde verandert
            }
        }

        private DateTime? _dueDateFilter;
        public DateTime? DueDateFilter
        {
            get => _dueDateFilter;
            set
            {
                _dueDateFilter = value;
                OnPropertyChanged(nameof(DueDateFilter));
            }
        }


        public MainViewModel(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;

            EditCommand = new RelayCommand<Core.Models.Task>(ExecuteEditTask);
            DeleteCommand = new RelayCommand<Core.Models.Task>(ExecuteDeleteTask);


            Tasks = new ObservableCollection<Core.Models.Task>();
            Categories = new ObservableCollection<Category>();
            SelectedCategoriesList = new ObservableCollection<Category>();

            // Load categories and tasks asynchronously
            LoadDataAsync();
        }
        private void ExecuteEditTask(Core.Models.Task selectedTask)
        {
            if (selectedTask != null)
            {
                // Create a new instance of AddTaskViewModel for editing
                var editTaskViewModel = new AddTaskViewModel(_taskRepository, Categories, Tasks, this, selectedTask);

                // Open the AddTaskView window for editing
                var addTaskView = new AddTaskView(editTaskViewModel);
                addTaskView.ShowDialog();  // Open as a dialog (modal window)
            }
        }

        private async void ExecuteDeleteTask(Core.Models.Task selectedTask)
        {
            if (selectedTask != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the task '{selectedTask.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _taskRepository.DeleteTaskAsync(selectedTask.Id);
                    Tasks.Remove(selectedTask);
                }
            }
        }
        private async System.Threading.Tasks.Task LoadDataAsync()
        {
            await LoadCategoriesAsync();
            await LoadTasksAsync();
        }

        public async System.Threading.Tasks.Task LoadTasksAsync()
        {
            Tasks.Clear();
            var tasks = await _taskRepository.GetAllTasksAsync();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        public async System.Threading.Tasks.Task LoadCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }

            // Notify that Categories has been updated
            OnPropertyChanged(nameof(Categories));
        }

        public async void FilterTasks()
        {
            // Get the selected category IDs or initialize to an empty list if none are selected
            var categoryIds = SelectedCategoriesList?.Select(c => c.Id).ToList() ?? new List<int>();

            // Get the filtered tasks from the repository based on the provided criteria
            var filteredTasks = await _taskRepository.GetTasksByCategoriesSearchTermCompletionStatusAndDueDateAsync(
                categoryIds,         // Filter by selected categories
                SearchTerm,          // Filter by search term
                ShowCompletedTasks,  // Filter by completed status
                DueDateFilter        // Filter by due date
            );

            // Clear the current task list and populate with the filtered tasks
            Tasks.Clear();
            foreach (var task in filteredTasks)
            {
                Tasks.Add(task);
            }
        }





    }

}
