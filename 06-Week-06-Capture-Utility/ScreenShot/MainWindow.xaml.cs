using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Media.Animation;

namespace ScreenShot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private MediaPlayer screenshotSound = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnFolder_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog folderDialog =
                   new System.Windows.Forms.FolderBrowserDialog())
            {
                folderDialog.Description = "Choose where screenshots will be saved";
                folderDialog.SelectedPath = saveFolder;

                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    saveFolder = folderDialog.SelectedPath;

                    MessageBox.Show(
                        $"Screenshots will now save to:\n\n{saveFolder}",
                        "Save Location");
                }
            }
        }

        private async void ShowScreenshotPreview(string filePath)
        {
            BitmapImage previewImage = new BitmapImage();

            previewImage.BeginInit();
            previewImage.CacheOption = BitmapCacheOption.OnLoad;
            previewImage.UriSource = new Uri(filePath, UriKind.Absolute);
            previewImage.EndInit();

            imgPreview.Source = previewImage;

            // Fade in quickly
            DoubleAnimation fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(200)
            };

            previewBorder.BeginAnimation(OpacityProperty, fadeIn);

            // Stay visible
            await Task.Delay(3000);

            // Fade out
            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(500)
            };

            previewBorder.BeginAnimation(OpacityProperty, fadeOut);
        }

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        private async void btnScreenshot_Click(object sender, RoutedEventArgs e)
        {
            screenshotSound.Open(
                new Uri("Assets/cameraclick.mp3", UriKind.Relative));
            screenshotSound.Position = TimeSpan.Zero;
            screenshotSound.Play();

            this.Hide();

            await Task.Delay(200);

            int screenWidth = GetSystemMetrics(0);
            int screenHeight = GetSystemMetrics(1);

            using (Bitmap screenshot = new Bitmap(screenWidth, screenHeight))
            {
                using (Graphics graphics = Graphics.FromImage(screenshot))
                {
                    graphics.CopyFromScreen(
                        0,
                        0,
                        0,
                        0,
                        new System.Drawing.Size(screenWidth, screenHeight));
                }

                string fileName = $"Screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";

                string filePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    fileName);

                screenshot.Save(filePath, ImageFormat.Png);

                this.Show();
                this.Activate();

                ShowScreenshotPreview(filePath);

            }
        }
    }
}
