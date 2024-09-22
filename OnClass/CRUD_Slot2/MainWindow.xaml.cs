using CRUD_Slot2.Models;
using Microsoft.EntityFrameworkCore;
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

namespace CRUD_Slot2
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
        private void load()
        {
            var student = PRN221_1Context.Ins.Students.Include(x => x.Depart).Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Gender = x.Gender ? "Male":"Female",
                DepartId = x.DepartId,
                Dob = x.Dob,
                Gpa = x.Gpa,
                Depart = x.Depart
            }).ToList();
            dgvDisplay.ItemsSource = student;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            load();
            loadDepart();
            cbDepartFilter.SelectedIndex = 0;
        }
        private void loadDepart()
        {
            var dept = PRN221_1Context.Ins.Departments.Select(x => x.Name).ToList();
            cbDepartFilter.ItemsSource = dept;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string dept = cbDepartFilter.SelectedValue.ToString();
            string deptId = PRN221_1Context.Ins.Departments.FirstOrDefault(x => x.Name.Equals(dept)).Id;
            var st = PRN221_1Context.Ins.Students.Include(x => x.Depart).Where(x => x.DepartId.Equals(deptId)).ToList();
            dgvDisplay.ItemsSource = st;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string txt = txtSearch.Text;
            
        }
    }
    //BTVN dinh dang lai db teo dd/mm/yyyy
    //thay the datagrid --> listview
    //filter theo gender (Male/Female/All)
    //Dockpanel -left-right
    //chia deu thanh ba cot grid
}