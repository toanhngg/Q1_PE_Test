using Microsoft.EntityFrameworkCore;
using Q1_PE_SP23_01.Models;
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

namespace Q1_PE_SP23_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly PRN_Spr23_B1Context _context;

        public MainWindow(PRN_Spr23_B1Context context)
        {
            InitializeComponent();
            _context = context;
            LoadData();
        }
        private void LoadData()
        {
            UpdateGridView();
        }
        private void UpdateGridView()
        {
          //  lvEmployee.ItemsSource = null;
            List<Employee> employees = _context.Employees.Include(x => x.Department).ToList(); // phải join cac bang vơi nhau
            lvEmployee.ItemsSource = employees;
            cboDepartmentId.ItemsSource = _context.Departments.ToList();

            //   lvEmployee.ItemsSource = _context.Departments.ToList();
            // Đảm bảo bạn đã thêm các mục vào ComboBox
            cboCourtesy.Items.Add("Dr.");
            cboCourtesy.Items.Add("Mr.");
            cboCourtesy.Items.Add("Mrs.");
            cboCourtesy.Items.Add("Ms.");
            // Đặt giá trị cho ComboBox
            cboCourtesy.SelectedItem = "Ms."; // Chọn mục "Ms."
        }
        public Employee getInfor()
        {
            Employee employee = null;
            try
            {
                employee = new Employee
                {
                    EmployeeId = string.IsNullOrEmpty(emId.Text) ? 0 : int.Parse(emId.Text),
                    FirstName = emNamefirst.Text,
                    LastName = emNamelast.Text,
                    DepartmentId = Int32.Parse(cboDepartmentId.SelectedValue.ToString()), //lay theo selected value se dung 
                    Title = emTitle.Text,
                    TitleOfCourtesy = cboCourtesy.SelectedValue.ToString(), //lay theo selected value se dung 
                    BirthDate = dob.SelectedDate
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot get Employee", "Get Employee");
            }
            return employee;
        }

        private void Btn_refresh(object sender, RoutedEventArgs e)
        {
            emId.Text = string.Empty;
            emNamefirst.Text = string.Empty;
            emNamelast.Text = string.Empty;
            emTitle.Text = string.Empty;
            cboDepartmentId.SelectedIndex = 0;
            cboCourtesy.SelectedIndex = 0;
            dob.SelectedDate = null;

        }

        private void Btn_add(object sender, RoutedEventArgs e)
        {
            try
            {
                var p = getInfor();
                if (p != null)
                {
                    p.EmployeeId = 0;
                    _context.Employees.Add(p);
                    _context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Add Employee Success!", "Add Employee");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Employee");
            }

        }

        private void Btn_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee1 = getInfor();
                if (employee1 != null)
                {
                    var oldEmployee = _context.Employees.FirstOrDefault(x => x.EmployeeId == employee1.EmployeeId);
                    if (oldEmployee != null)
                    {
                        oldEmployee.BirthDate = employee1.BirthDate;
                        oldEmployee.LastName = employee1.LastName;
                        oldEmployee.FirstName = employee1.FirstName;
                        oldEmployee.Title = employee1.Title;
                        oldEmployee.DepartmentId = employee1.DepartmentId;
                        oldEmployee.TitleOfCourtesy = employee1.TitleOfCourtesy;

                        _context.Employees.Update(oldEmployee);
                        _context.SaveChanges();
                        LoadData();
                        MessageBox.Show("Update employee success", "Update employee");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update employee fail", "Update Employee");

            }
        }
    }
}
