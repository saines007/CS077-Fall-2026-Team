using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoryProduct
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
        private void Build_Click(object sender, RoutedEventArgs e)
        {
            ResultText.Text = BuildChecklist(NameInput.Text);
        }
        private string BuildChecklist(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return "Enter a name before continuing.";
            }

            string[] tasks = { "Check the item", "Record the result", "Return the tool" };
            string result = $"Checklist for {userName.Trim()}:\n";

            for (int index = 0; index < tasks.Length; index++)
            {
                result += $"{index + 1}. {tasks[index]}\n";
            }

            return result;
        }
    }
}