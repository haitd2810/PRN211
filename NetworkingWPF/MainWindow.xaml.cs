using System.Net.Http;
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

namespace NetworkingWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnViewHTML_Click(object sender, RoutedEventArgs e)
        {
            string uri = txtURL.Text;
            try
            {
                string response = await client.GetStringAsync(uri);
                txtContent.Text = response.Trim();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtURL.Clear();
            txtContent.Clear();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}