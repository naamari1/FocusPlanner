using NAudio.Wave;
using Notifications.Wpf;



namespace FocusPlanner.Services
{
    public class NotificationService
    {
        private readonly string _audioFilePath = "C:\\Users\\nouri\\source\\repos\\FocusPlanner\\FocusPlanner\\Assets\\gog.mp3";

        public void ShowToastNotification()
        {
            var notificationManager = new NotificationManager();

            // Stel je notificatie in
            notificationManager.Show(new NotificationContent
            {
                Title = "Herinnering",
                Message = "Je hebt nog 30 minuten voor de deadline.",
                Type = NotificationType.Information
            });

            // Speel het MP3-geluid af
            PlayMp3("_audioFilePath");
        }

        private void PlayMp3(string filePath)
        {
            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}