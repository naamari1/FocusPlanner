using FocusPlanner.Core.Interfaces;
using FocusPlanner.Core.Models;
using System.Collections.ObjectModel;

namespace FocusPlanner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;

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



        public MainViewModel(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;

            Tasks = new ObservableCollection<Core.Models.Task>();
            Categories = new ObservableCollection<Category>();
            SelectedCategoriesList = new ObservableCollection<Category>();

            // Load categories and tasks asynchronously
            LoadDataAsync();
        }

        private async System.Threading.Tasks.Task LoadDataAsync()
        {
            await LoadCategoriesAsync();
            await LoadTasksAsync();
        }

        public async System.Threading.Tasks.Task LoadTasksAsync()
        {
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
            // Wanneer er geen categorieën geselecteerd zijn, toon dan alle taken of gefilterd op zoekterm
            if ((SelectedCategoriesList == null || !SelectedCategoriesList.Any()) && string.IsNullOrEmpty(SearchTerm))
            {
                var allTasks = await _taskRepository.GetAllTasksAsync();  // Haal alle taken op
                Tasks.Clear();
                foreach (var task in allTasks)
                {
                    Tasks.Add(task);
                }
                return;  // Stop verdere verwerking omdat we alle taken tonen
            }

            // Wanneer er categorieën geselecteerd zijn, filter de taken
            var categoryIds = SelectedCategoriesList?.Select(c => c.Id).ToList() ?? new List<int>();
            var filteredTasks = await _taskRepository.GetTasksByCategoriesAndSearchTermAsync(categoryIds, SearchTerm);

            Tasks.Clear();
            foreach (var task in filteredTasks)
            {
                Tasks.Add(task);
            }
        }



    }

}
