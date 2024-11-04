using FocusPlanner.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

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

            _addTaskViewModel.TaskCompleted += async () => await PlayConfettiAnimationAsync(); // Abonneer het event op de animatiemethode


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

        private async Task PlayConfettiAnimationAsync()
        {
            ConfettiCanvas.Visibility = Visibility.Visible;

            // Wacht tot de layout volledig is bijgewerkt en het Canvas de juiste afmetingen heeft
            await Dispatcher.InvokeAsync(() => { }, System.Windows.Threading.DispatcherPriority.Loaded);

            Random random = new Random();

            // Voeg meerdere confetti-elementen toe
            for (int i = 0; i < 400; i++)
            {
                var confetti = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))),
                    Opacity = 0.8
                };

                ConfettiCanvas.Children.Add(confetti);

                // Startpositie instellen met meer variatie in de verticale spreiding
                double startX = random.Next(0, (int)ConfettiCanvas.ActualWidth);
                double startY = random.Next(-600, -20); // Willekeurige hoogte boven het canvas voor spreiding

                double endX = startX + random.Next(-200, 200); // Grotere x-beweging voor bredere spreiding
                double endY = ConfettiCanvas.ActualHeight + 20;

                var translateTransform = new TranslateTransform(startX, startY);
                confetti.RenderTransform = translateTransform;

                // Langzamere x- en y-animatie voor trager vallende confetti
                var xAnimation = new DoubleAnimation
                {
                    From = startX,
                    To = endX,
                    Duration = TimeSpan.FromSeconds(6 + random.NextDouble() * 3), // Langere duur voor bredere spreiding
                    EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
                };

                var yAnimation = new DoubleAnimation
                {
                    From = startY,
                    To = endY,
                    Duration = TimeSpan.FromSeconds(8 + random.NextDouble() * 4), // Langere duur voor trager vallen
                    EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
                };

                translateTransform.BeginAnimation(TranslateTransform.XProperty, xAnimation);
                translateTransform.BeginAnimation(TranslateTransform.YProperty, yAnimation);
            }

            await Task.Delay(9000);
            ConfettiCanvas.Children.Clear();
            ConfettiCanvas.Visibility = Visibility.Collapsed;
        }



    }
}
