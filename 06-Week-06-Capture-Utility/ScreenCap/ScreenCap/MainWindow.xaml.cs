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
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace ScreenCap
{
    public partial class MainWindow : Window
    {
        private MediaPlayer captureSound = new MediaPlayer();

    public MainWindow()
    {
        InitializeComponent();

        captureSound.Open(new Uri(
            System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "sounds",
                "cameraclick.mp3")));
    }

    private void btnCapture_Click(object sender, RoutedEventArgs e)
    {
        var bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

        using (Bitmap screenshot = new Bitmap(
            bounds.Width,
            bounds.Height))
        {
            using (Graphics graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(
                    bounds.X,
                    bounds.Y,
                    0,
                    0,
                    bounds.Size);
            }

            string filename =
                $"Screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";

            string filePath =
                System.IO.Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.MyPictures),
                    filename);

            screenshot.Save(filePath, ImageFormat.Png);

            // Play camera/capture sound
            captureSound.Position = TimeSpan.Zero;
            captureSound.Play();

            System.Windows.MessageBox.Show(
                $"Screenshot saved successfully!\n\n" +
                $"File path:\n{filePath}",
                "Screen Capture");
            }
        }
    }
}
