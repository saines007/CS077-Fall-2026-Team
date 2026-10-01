using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Media.Animation;

namespace ScreenShot
{
    public partial class MainWindow : Window
    {
        private string saveFolder =
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private MediaPlayer screenshotSound = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "Name your screenshot")
            {
                txtName.Text = "";
                txtName.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.Text = "Name your screenshot";
                txtName.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void btnFolder_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog folderDialog =
                   new System.Windows.Forms.FolderBrowserDialog())
            {
                folderDialog.Description =
                    "Choose where screenshots will be saved";

                folderDialog.SelectedPath = saveFolder;

                if (folderDialog.ShowDialog() ==
                    System.Windows.Forms.DialogResult.OK)
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

            // Fade preview in
            DoubleAnimation fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(200)
            };

            previewBorder.BeginAnimation(OpacityProperty, fadeIn);

            // Keep it visible for 3 seconds
            await Task.Delay(3000);

            // Fade preview out
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
            // Play screenshot sound
            screenshotSound.Open(
                new Uri("Assets/cameraclick.mp3", UriKind.Relative));

            screenshotSound.Position = TimeSpan.Zero;
            screenshotSound.Play();

            // Hide Screenshot 9001
            this.Hide();

            await Task.Delay(200);

            int screenWidth = GetSystemMetrics(0);
            int screenHeight = GetSystemMetrics(1);

            using (Bitmap screenshot =
                   new Bitmap(screenWidth, screenHeight))
            {
                using (Graphics graphics = Graphics.FromImage(screenshot))
                {
                    graphics.CopyFromScreen(
                        0,
                        0,
                        0,
                        0,
                        new System.Drawing.Size(
                            screenWidth,
                            screenHeight));
                }

                string fileName;

                // Use automatic name if textbox wasn't used
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    txtName.Text == "Name your screenshot")
                {
                    fileName =
                        $"Screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                }
                else
                {
                    string customName = txtName.Text.Trim();

                    // Replace illegal Windows filename characters
                    foreach (char invalidChar in
                             System.IO.Path.GetInvalidFileNameChars())
                    {
                        customName =
                            customName.Replace(invalidChar, '_');
                    }

                    fileName = customName + ".png";
                }

                // Save to selected folder
                string filePath = System.IO.Path.Combine(
                    saveFolder,
                    fileName);

                screenshot.Save(filePath, ImageFormat.Png);

                // Reset screenshot name box
                txtName.Text = "Name your screenshot";
                txtName.Foreground =
                    System.Windows.Media.Brushes.Gray;

                // Bring Screenshot 9001 back
                this.Show();
                this.Activate();

                // Show screenshot preview
                ShowScreenshotPreview(filePath);
            }
        }
    }
}