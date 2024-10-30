using FocusPlanner.Core.Enum;
using FocusPlanner.Core.Interfaces;
using FocusPlanner.Core.Models;
using FocusPlanner.Notification;
using System.Collections.ObjectModel;
using System.Windows;

namespace FocusPlanner.ViewModels
{
    public class AddTaskViewModel : BaseViewModel
    {

        private readonly ITaskRepository taskRepository;
        private readonly NotificationService notificationService;


        public ObservableCollection<Category> Categories { get; set; }

        public ObservableCollection<Core.Models.Task> Tasks { get; set; }

        public List<string> Priorities { get; set; }

        public Core.Models.Task SelectedTask { get; set; }
        private readonly MainViewModel mainViewModel;

        public event Action TaskCompleted;




        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int SelectedCategoryId { get; set; }
        public int SelectedPriorityIndex { get; set; }
        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    var result = MessageBox.Show("Are you really done with the task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _isCompleted = value;
                        OnPropertyChanged(nameof(IsCompleted));

                        if (_isCompleted)
                        {
                            PlayCompletionSoundAsync();
                            TaskCompleted?.Invoke(); // Trigger het event
                        }
                    }
                    else
                    {
                        _isCompleted = false;
                        OnPropertyChanged(nameof(IsCompleted));
                    }
                }
            }
        }


        // Helper async void method to play the completion sound
        private async void PlayCompletionSoundAsync()
        {
            await notificationService.PlayMp3Async(notificationService.AudioFilePath2);
        }
        public DateTime? StartDate { get; set; }
        public string StartTime { get; set; }
        public DateTime? FinishDate { get; set; }
        public string FinishTime { get; set; }
        public string DeadlineTime { get; set; }



        public AddTaskViewModel(ITaskRepository taskRepository, ObservableCollection<Category> categories, ObservableCollection<Core.Models.Task> tasks, MainViewModel mainViewModel, NotificationService notificationService, Core.Models.Task selectedTask = null)
        {
            this.taskRepository = taskRepository;
            this.Categories = categories;
            this.Tasks = tasks;
            this.SelectedTask = selectedTask;
            this.mainViewModel = mainViewModel;
            this.notificationService = notificationService;

            _isCompleted = selectedTask?.IsCompleted ?? false;

            // If editing an existing task
            if (selectedTask != null)
            {
                Title = selectedTask.Title;
                Description = selectedTask.Description;
                DueDate = selectedTask.DueDate;
                SelectedCategoryId = selectedTask.CategoryId;
                SelectedPriorityIndex = (int)selectedTask.Priority;
                IsCompleted = selectedTask.IsCompleted;
                StartDate = selectedTask.StartDate;
                FinishDate = selectedTask.FinishDate;


                // Initialize StartTime and FinishTime based on StartDate and FinishDate
                StartTime = selectedTask.StartDate.HasValue ? selectedTask.StartDate.Value.ToString("HH:mm") : null;
                FinishTime = selectedTask.FinishDate.HasValue ? selectedTask.FinishDate.Value.ToString("HH:mm") : null;
                DeadlineTime = selectedTask.DueDate.HasValue ? selectedTask.DueDate.Value.ToString("HH:mm") : null;
            }
            else
            {
                // If adding a new task, set default values
                if (Categories.Any())
                {
                    SelectedCategoryId = Categories.First().Id; // Default to the first category
                }
            }

            Priorities = Enum.GetNames(typeof(Priority)).ToList();

        }


        public async Task<bool> AddTaskAsync(Core.Models.Task existingTask = null)
        {
            if (string.IsNullOrEmpty(Title) || SelectedCategoryId == 0 || SelectedPriorityIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                // Combine StartDate and StartTime
                DateTime? combinedStartDateTime = null;
                if (StartDate.HasValue && !string.IsNullOrWhiteSpace(StartTime))
                {
                    if (DateTime.TryParse($"{StartDate.Value.ToShortDateString()} {StartTime}", out DateTime start))
                    {
                        combinedStartDateTime = start;
                    }
                }

                // Combine FinishDate and FinishTime
                DateTime? combinedFinishDateTime = null;
                if (FinishDate.HasValue && !string.IsNullOrWhiteSpace(FinishTime))
                {
                    if (DateTime.TryParse($"{FinishDate.Value.ToShortDateString()} {FinishTime}", out DateTime finish))
                    {
                        combinedFinishDateTime = finish;
                    }
                }

                DateTime? combinedDueDateTime = null;
                if (DueDate.HasValue && !string.IsNullOrWhiteSpace(DeadlineTime))
                {
                    if (DateTime.TryParse($"{DueDate.Value.ToShortDateString()} {DeadlineTime}", out DateTime deadline))
                    {
                        combinedDueDateTime = deadline;
                    }
                }

                if (existingTask == null)
                {
                    var newTask = new Core.Models.Task
                    {
                        Title = this.Title,
                        Description = this.Description,
                        DueDate = combinedDueDateTime,
                        StartDate = combinedStartDateTime,
                        FinishDate = combinedFinishDateTime,
                        Priority = (Priority)SelectedPriorityIndex,
                        CategoryId = this.SelectedCategoryId,
                        IsCompleted = this.IsCompleted
                    };

                    await taskRepository.AddTaskAsync(newTask);
                    Tasks.Add(newTask);
                }
                else
                {
                    existingTask.Title = this.Title;
                    existingTask.Description = this.Description;
                    existingTask.DueDate = combinedDueDateTime;
                    existingTask.StartDate = combinedStartDateTime;
                    existingTask.FinishDate = combinedFinishDateTime;
                    existingTask.Priority = (Priority)SelectedPriorityIndex;
                    existingTask.CategoryId = this.SelectedCategoryId;
                    existingTask.IsCompleted = this.IsCompleted;

                    await taskRepository.UpdateTaskAsync(existingTask);
                    await mainViewModel.LoadTasksAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add or update task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }



    }
}
