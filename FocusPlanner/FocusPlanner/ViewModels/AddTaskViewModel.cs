using FocusPlanner.Core.Enum;
using FocusPlanner.Core.Interfaces;
using FocusPlanner.Core.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace FocusPlanner.ViewModels
{
    public class AddTaskViewModel : BaseViewModel
    {

        private readonly ITaskRepository taskRepository;

        public ObservableCollection<Category> Categories { get; set; }

        public ObservableCollection<Core.Models.Task> Tasks { get; set; }

        public List<string> Priorities { get; set; }

        public Core.Models.Task SelectedTask { get; set; }
        private readonly MainViewModel mainViewModel;



        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int SelectedCategoryId { get; set; }
        public int SelectedPriorityIndex { get; set; }
        public bool IsCompleted { get; set; }


        public AddTaskViewModel(ITaskRepository taskRepository, ObservableCollection<Category> categories, ObservableCollection<Core.Models.Task> tasks, MainViewModel mainViewModel, Core.Models.Task selectedTask = null)
        {
            this.taskRepository = taskRepository;
            this.Categories = categories;
            this.Tasks = tasks;
            this.SelectedTask = selectedTask;
            this.mainViewModel = mainViewModel;

            // If editing an existing task
            if (selectedTask != null)
            {
                Title = selectedTask.Title;
                Description = selectedTask.Description;
                DueDate = selectedTask.DueDate;
                SelectedCategoryId = selectedTask.CategoryId;
                SelectedPriorityIndex = (int)selectedTask.Priority;
                IsCompleted = selectedTask.IsCompleted;
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
            // Perform validation inside ViewModel
            if (string.IsNullOrEmpty(Title) || SelectedCategoryId == 0 || SelectedPriorityIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;  // Indicate that the operation was not successful
            }

            try
            {
                if (existingTask == null)
                {
                    // Create a new task if no existing task is provided (Add mode)
                    var newTask = new Core.Models.Task
                    {
                        Title = this.Title,
                        Description = this.Description,
                        DueDate = this.DueDate,
                        Priority = (Priority)SelectedPriorityIndex,
                        CategoryId = this.SelectedCategoryId,
                        IsCompleted = this.IsCompleted
                    };

                    // Add the new task to the repository
                    await taskRepository.AddTaskAsync(newTask);
                    Tasks.Add(newTask); // Add the task to the in-memory collection (UI update)
                }
                else
                {
                    // Update the existing task (Edit mode)
                    existingTask.Title = this.Title;
                    existingTask.Description = this.Description;
                    existingTask.DueDate = this.DueDate;
                    existingTask.Priority = (Priority)SelectedPriorityIndex;
                    existingTask.CategoryId = this.SelectedCategoryId;
                    existingTask.IsCompleted = this.IsCompleted;

                    // Update the task in the repository
                    await taskRepository.UpdateTaskAsync(existingTask);
                    await mainViewModel.LoadTasksAsync();
                }

                return true;  // Indicate success
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add or update task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


    }
}
