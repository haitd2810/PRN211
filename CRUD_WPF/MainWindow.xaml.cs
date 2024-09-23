using CRUD_WPF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Windows.Themes;
using System;
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
using System.Xml.Linq;

namespace CRUD_WPF
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
            loadDepart();
            cbxSearch.ItemsSource = element;
            cbxSearch.SelectedIndex = 1;
        }
        string[] element = { "Id", "Name", "Gender", "Depart Name", "Dob", "GPA" };
        private void Load()
        {
            var student = PRN221Context.Instance.Students.Include(depart => depart.Depart)
                                                         .ToList();
            dgvDisplay.ItemsSource = student;
        }

        private void loadDepart()
        {
            var dept = PRN221Context.Instance.Departments.Select(x => x.Name).ToList();
            dept.Insert(0,"All");
            cbxDepartFilter.Items.Clear();
            cbxDepartFilter.ItemsSource = dept;
            cbxDepartFilter.SelectedIndex = 0;
            CbxDepart.ItemsSource = PRN221Context.Instance.Departments.Select(x => x.Name).ToList();
            CbxDepart.SelectedIndex = 0;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            //string dept = cbxDepartFilter.SelectedValue.ToString();
            //var st = new List<Student>();
            //if (dept == "All") {
            //    Load();
            //}
            //else
            //{
            //    string deptId = PRN221Context.Instance.Departments.FirstOrDefault(x => x.Name.Equals(dept)).Id;
            //    st = PRN221Context.Instance.Students.Include(x => x.Depart).Where(x => x.DepartId.Equals(deptId)).ToList();
            //    dgvDisplay.ItemsSource = st;
            //}

            Filter();
            //var st = PRN221Context.Instance.Students.Include(x => x.Depart)
            //    .Where(x => dept.Equals("All") ? true : x.Depart.Name.Equals(dept)).ToList();
            //dgvDisplay.ItemsSource= st;
        }

        private void btnSeach_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
        private void Filter()
        {
            string dept = cbxDepartFilter.SelectedValue.ToString();
            var st = PRN221Context.Instance.Students.Include(x => x.Depart)
                .Where(x => dept.Equals("All") ? true : x.Depart.Name.Equals(dept)).ToList();
            dgvDisplay.ItemsSource = st;
        }
        private void cbxDepartFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Search()
        {
            string txt = txtSearch.Text;
            switch (cbxSearch.SelectedValue.ToString())
            {
                case "Name":
                    dgvDisplay.ItemsSource = PRN221Context.Instance.Students.Include(x => x.Depart)
                        .Where(x => string.IsNullOrEmpty(txt) ? true : x.Name.Contains(txt)).ToList();
                    break;
            }
        }
        private void txtSearch_TextChange(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            Student st = getStudent();
            if (st == null) {
                ClearForm();
                return;
            }
            var x = PRN221Context.Instance.Students.Find(st.Id);
            if (x == null) {
                PRN221Context.Instance.Students.Add(st);
                PRN221Context.Instance.SaveChanges();
                Filter();
            }
            
        }

        private void ClearForm()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            rdbMale.IsChecked = false;
            rdbFemale.IsChecked = true;
            //cbFemale.IsChecked = false;
            //cbFemale.IsChecked = false;
            CbxDepart.SelectedIndex = 0;
            dkbdob.SelectedDate = null;
            txtGPA.Text = string.Empty;
        }
        private Student getStudent()
        {
            try
            {
                int id = int.Parse(txtId.Text);
                string name = txtName.Text;
                //bool gender = cbMale.IsChecked.Value;
                bool gender = rdbMale.IsChecked == true;
                string departId = PRN221Context.Instance.Departments.Where(x => x.Name.Equals(CbxDepart.SelectedValue))
                                                                    .Select(x => x.Id).FirstOrDefault();
                float gpa = float.Parse(txtGPA.Text);
                DateTime? dob = dkbdob.SelectedDate.Value;
                return new Student()
                {
                    Id = id,
                    Name = name,
                    Gender = gender,
                    DepartId = departId,
                    Gpa = gpa,
                    Dob = dob
                };
            }catch(Exception ex)
            {
                return null;
            }
        }

        private void dgvDisplay_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Student st = dgvDisplay.SelectedItem as Student;
            /* Có thể xài if nhưng hiện tại đang để comment để sử dụng binding data trong UI */
            //if (st != null) {
            //    txtId.Text = st.Id.ToString();
            //    txtName.Text = st.Name.ToString();
            //    cbMale.IsChecked = st.Gender;
            //    cbFemale.IsChecked = !st.Gender;
            //    CbxDepart.SelectedItem = st.Depart.Name;
            //    dkbdob.SelectedDate = st.Dob;
            //    txtGPA.Text = st.Gpa.ToString();
            //}
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student st = getStudent();
            var x = PRN221Context.Instance.Students.Find(st.Id);
            if (x != null)
            {
                x.Name = st.Name;
                x.Gender = st.Gender;
                x.DepartId = st.DepartId;
                x.Gpa = st.Gpa;
                x.Dob = st.Dob;
                PRN221Context.Instance.Students.Update(x);
                PRN221Context.Instance.SaveChanges();
                Filter();
            }
            else
            {
                MessageBox.Show("Not exist student!");
            }
            
        }
        private Student setValue(Student st)
        {
            st.Name = txtName.Text;
            //st.Gender = cbMale.IsChecked.Value;
            st.Gender = rdbMale.IsChecked.Value;
            st.DepartId = PRN221Context.Instance.Departments.Where(x => x.Name.Equals(CbxDepart.SelectedValue))
                                                                    .Select(x => x.Id).FirstOrDefault();
            st.Gpa = float.Parse(txtGPA.Text);
            st.Dob = dkbdob.SelectedDate.Value;
            return st;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = PRN221Context.Instance.Students.Find(int.Parse(txtId.Text));
                if (x != null) {
                    var result = MessageBox.Show("Do you want to delete this?", "Confirm",MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        PRN221Context.Instance.Students.Remove(x);
                        PRN221Context.Instance.SaveChanges();
                        Load();
                    }
                }
            }
            catch (Exception ex) {
                return;
            }
        }
    }
}
//BTVN: 
//thoi gian nhap sau datetime.now thi se bao loi
//xoa them messagebox de comfirm lai co xoa thong tin hay khong
//thay the Gender = checkbox, combobox
//thay the department = group radiobutton, group checkbox
//lam the nao de binding bang radio button
