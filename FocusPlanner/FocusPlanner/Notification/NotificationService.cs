using Microsoft.Toolkit.Uwp.Notifications;
using NAudio.Wave;
using System.Diagnostics;
using System.IO;

namespace FocusPlanner.Notification
{
    public class NotificationService
    {

        private readonly string _audioFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "gog.mp3");
        public readonly string AudioFilePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "done.mp3");


        public async System.Threading.Tasks.Task ShowToastNotificationAsync(string title)
        {
            new ToastContentBuilder()
                .AddText($"Herinnering: {title}")
                .AddText("Je hebt nog 30 minuten voor de deadline.")
                .AddAudio(new ToastAudio() { Silent = true })
                .SetToastDuration(ToastDuration.Long)
                .Show();

            await PlayMp3Async(_audioFilePath);
        }

        public async System.Threading.Tasks.Task PlayMp3Async(string filePath, int playDurationInMilliseconds = 9000)
        {
            Debug.WriteLine($"Afspelen van geluid van: {filePath}");

            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                // Optionally, set the start position (e.g., start from 5 seconds).
                audioFile.CurrentTime = TimeSpan.FromSeconds(0);

                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Await the playback for a specific duration (e.g., 3000ms = 3 seconds).
                await System.Threading.Tasks.Task.Delay(playDurationInMilliseconds);

                // Stop playback after the specified time.
                outputDevice.Stop();
            }
        }



    }
}
