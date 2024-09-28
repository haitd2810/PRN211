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
using System.Xml.Serialization;
using System.Xml.Linq;
using DemoXML_Json.Models;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Xml;
using System.Runtime.Intrinsics.Arm;

namespace DemoXML_Json
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
        List<catalogBook> books = new List<catalogBook>();
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //var ofd = new OpenFileDialog()
            //{
            //    Filter = "XML Files (*.xml)|*.xml|All File(*.*)|*.*"
            //};
            //var result = ofd.ShowDialog();
            //if (result == false) return;
            //var xs = new XmlSerializer(typeof(catalog));
            //using Stream s2 = File.OpenRead(ofd.FileName);
            //var cata = (catalog)xs.Deserialize(s2);
            //books = cata.book.ToList();
            //dgvDisplay.ItemsSource = cata.book.ToList();
            //s2.Close();

            var ofd = new OpenFileDialog()
            {
                Filter = "Json Files (*.json)|*.json|All File(*.*)|*.*"
            };
            var result = ofd.ShowDialog();
            if (result == false) return;
            using (StreamReader s2 = new StreamReader(ofd.FileName))
            {
                string json = s2.ReadToEnd();
                Rootobject quiz = JsonSerializer.Deserialize<Rootobject>(json);

                var displayData = new List<dynamic>();
                displayData.Add(quiz.quiz);

                dgvDisplay.ItemsSource = displayData;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new OpenFileDialog()
            {
                Filter = "XML Files (*.xml)|*.xml|All File(*.*)|*.*"
            };
            var result = sfd.ShowDialog();
            if (result == false) return;
            var xs = new XmlSerializer(typeof(List<catalogBook>));
            using Stream s1 = File.Create(sfd.FileName);
            xs.Serialize(s1, books);
            s1.Close();

            //var sfd = new OpenFileDialog()
            //{
            //    Filter = "Json Files (*.json)|*.json|All File(*.*)|*.*"
            //};
            //var result = sfd.ShowDialog();
            //if (result == false) return;
            //string jsonString = JsonSerializer.Serialize(books, new JsonSerializerOptions()
            //{
            //    WriteIndented = true
            //});
            //using (StreamWriter outputFile = new StreamWriter(jsonString))
            //{
            //    outputFile.WriteLine(jsonString);
            //}
        }
    }
}