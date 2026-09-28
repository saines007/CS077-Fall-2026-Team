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
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Forms = System.Windows.Forms;

namespace ScreenCapture9000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            var bounds = Forms.Screen.PrimaryScreen.Bounds;

            using (Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics graphics = Graphics.FromImage(screenshot))
                {
                    graphics.CopyFromScreen(
                        bounds.Left,
                        bounds.Top,
                        0,
                        0,
                        bounds.Size);
                }

                using (MemoryStream memory = new MemoryStream())
                {
                    screenshot.Save(
                        memory,
                        System.Drawing.Imaging.ImageFormat.Png);

                    memory.Position = 0;

                    BitmapImage image = new BitmapImage();

                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = memory;
                    image.EndInit();
                    picPreview.Source = image;

                    // Get the Windows Desktop folder
                    string desktopPath = Environment.GetFolderPath(
                        Environment.SpecialFolder.Desktop);

                    // Create a unique filename using the current date and time
                    string fileName = "Screenshot_" +
                                      DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") +
                                      ".png";

                    // Combine the Desktop location and filename
                    string filePath = System.IO.Path.Combine(desktopPath, fileName);

                    picPreview.Source = image;

                    lblStatus.Text = "Screen captured successfully.";
                }
            }
        }
        // Saves the currently displayed screenshot to the Desktop.
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (picPreview.Source == null)
            {
                lblStatus.Text = "Capture a screen before saving.";
                return;
            }

            string desktopPath = Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop);

            string fileName = "Screenshot_" +
                              DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") +
                              ".png";

            string filePath =
                System.IO.Path.Combine(desktopPath, fileName);

            BitmapEncoder encoder = new PngBitmapEncoder();

            encoder.Frames.Add(
                BitmapFrame.Create((BitmapSource)picPreview.Source));

            using (FileStream fileStream = new FileStream(
                filePath,
                FileMode.Create))
            {
                encoder.Save(fileStream);
            }

            lblStatus.Text =
                "Screenshot saved to Desktop: " + fileName;
        }
    }
 }
