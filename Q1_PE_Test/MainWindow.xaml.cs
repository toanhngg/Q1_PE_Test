using Microsoft.EntityFrameworkCore.Diagnostics;
using Q1_PE_Test.Models;
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

namespace Q1_PE_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PRN221_Spr22Context _context;
        Employee originalContext;

        public MainWindow(PRN221_Spr22Context context)
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
            listEmployee.ItemsSource = null;
            listEmployee.ItemsSource = _context.Employees.ToList();
        }
        public Employee GetEmployeeObject()
        {
            Employee employee = null;
            try
            {
                employee = new Employee
                {
                    Id = string.IsNullOrEmpty(employeeId.Text) ? 0 : int.Parse(employeeId.Text),
                    Name = employeeName.Text,
                    Gender = male.IsChecked == true ? "Male" : "Female",
                    Dob = dob.SelectedDate,
                    Phone = phone.Text,
                    Idnumber = idnumber.Text,

                };

            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot get Employee", "Get Employee");
            }
            return employee;
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            employeeId.Text = string.Empty;
            employeeName.Text = string.Empty;
            male.IsChecked = true;
            phone.Text = string.Empty;
            idnumber.Text = string.Empty;
            dob.SelectedDate = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = GetEmployeeObject();
            if(employee != null)
            {
                employee.Id = 0;
                _context.Employees.Add(employee);
                _context.SaveChanges();
                LoadData();
                MessageBox.Show($"{employee.Name} insert succesfully", "Insert Employee");
            }


        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee1 = GetEmployeeObject();
                if (employee1 != null)
                {
                    var oldEmployee = _context.Employees.FirstOrDefault(x => x.Id == employee1.Id);
                    if (oldEmployee != null)
                    {
                        oldEmployee.Dob = employee1.Dob;
                        oldEmployee.Phone = employee1.Phone;
                        oldEmployee.Name = employee1.Name;
                        oldEmployee.Idnumber = employee1.Idnumber;
                        oldEmployee.Gender = employee1.Gender;

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
        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
       //     originalContext = (Employee)listEmployee.SelectedItem;

            if (item != null)
            {
                if (item == originalContext)
                {
                    originalContext = null;
                }
                //  originalContext = (Employee)item;
                var gender = ((Employee)item).Gender;
                if (!string.IsNullOrEmpty(gender))
                {
                    if (gender.ToLower() == "female")
                    {
                        female.IsChecked = true;
                    }
                    else
                    {
                        male.IsChecked = true;
                    }
                }
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee = GetEmployeeObject();
                if (employee != null)
                {
                    var oldEmployee = _context.Employees.FirstOrDefault(x => x.Id == employee.Id);
                    if (oldEmployee != null)
                    {
                        _context.Employees.Remove(oldEmployee);
                        _context.SaveChanges();
                        LoadData();
                        MessageBox.Show("Delete employee success", "Delete employee");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete employee fail", "Delete Employee");

            }
        }
    }
}
