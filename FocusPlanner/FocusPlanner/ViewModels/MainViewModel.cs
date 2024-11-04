using FocusPlanner.Command;
using FocusPlanner.Core.Interfaces;
using FocusPlanner.Core.Models;
using FocusPlanner.Notification;
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
        private readonly NotificationService notificationService;


        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private readonly Dictionary<int, (bool Notified30, bool Notified10, bool Notified1)> taskNotifications = new();


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


        public MainViewModel(ITaskRepository taskRepository, ICategoryRepository categoryRepository, NotificationService notificationService)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            this.notificationService = notificationService;

            EditCommand = new RelayCommand<Core.Models.Task>(ExecuteEditTask);
            DeleteCommand = new RelayCommand<Core.Models.Task>(ExecuteDeleteTask);


            Tasks = new ObservableCollection<Core.Models.Task>();
            Categories = new ObservableCollection<Category>();
            SelectedCategoriesList = new ObservableCollection<Category>();

            // Load categories and tasks asynchronously
            LoadDataAsync();

            MonitorTaskDeadlines();
        }

        private async void MonitorTaskDeadlines()
        {
            while (true)
            {
                var tasksToNotify30 = Tasks
                    .Where(t => t.DueDate.HasValue &&
                                t.DueDate.Value > DateTime.Now &&
                                (t.DueDate.Value - DateTime.Now).TotalMinutes <= 30 &&
                                (t.DueDate.Value - DateTime.Now).TotalMinutes > 29 &&
                                (!taskNotifications.ContainsKey(t.Id) || !taskNotifications[t.Id].Notified30))
                    .ToList();

                foreach (var task in tasksToNotify30)
                {
                    await notificationService.ShowToastNotificationAsync(task.Title);
                    taskNotifications[task.Id] = (
                        Notified30: true,
                        Notified10: taskNotifications.ContainsKey(task.Id) ? taskNotifications[task.Id].Notified10 : false,
                        Notified1: taskNotifications.ContainsKey(task.Id) ? taskNotifications[task.Id].Notified1 : false
                    );
                }

                var tasksToNotify10 = Tasks
                    .Where(t => t.DueDate.HasValue &&
                                t.DueDate.Value > DateTime.Now &&
                                (t.DueDate.Value - DateTime.Now).TotalMinutes <= 10 &&
                                (t.DueDate.Value - DateTime.Now).TotalMinutes > 9 &&
                                (!taskNotifications.ContainsKey(t.Id) || !taskNotifications[t.Id].Notified10))
                    .ToList();

                foreach (var task in tasksToNotify10)
                {
                    await notificationService.ShowToastNotification10minAsync(task.Title);
                    taskNotifications[task.Id] = (
                        Notified30: taskNotifications.ContainsKey(task.Id) ? taskNotifications[task.Id].Notified30 : false,
                        Notified10: true,
                        Notified1: taskNotifications.ContainsKey(task.Id) ? taskNotifications[task.Id].Notified1 : false
                    );
                }

                var tasksToNotify1 = Tasks
                    .Where(t => t.DueDate.HasValue &&
                                t.DueDate.Value > DateTime.Now &&
                                (t.DueDate.Value - DateTime.Now).TotalMinutes <= 2 &&
                                (t.DueDate.Value - DateTime.Now).TotalMinutes > 1 &&
                                (!taskNotifications.ContainsKey(t.Id) || !taskNotifications[t.Id].Notified1))
                    .ToList();

                foreach (var task in tasksToNotify1)
                {
                    await notificationService.ShowToastNotification1minAsync(task.Title);
                    taskNotifications[task.Id] = (
                        Notified30: taskNotifications.ContainsKey(task.Id) ? taskNotifications[task.Id].Notified30 : false,
                        Notified10: taskNotifications.ContainsKey(task.Id) ? taskNotifications[task.Id].Notified10 : false,
                        Notified1: true
                    );
                }

                // Wait for a minute before checking again
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(1));
            }
        }






        private void ExecuteEditTask(Core.Models.Task selectedTask)
        {
            if (selectedTask != null)
            {
                // Create a new instance of AddTaskViewModel for editing
                var editTaskViewModel = new AddTaskViewModel(_taskRepository, Categories, Tasks, this, notificationService, selectedTask);

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
