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

namespace FunnyWaterDelivery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.department.ItemsSource = ConnectToDB.GetDepartmentList();
            this.orderListBox.ItemsSource = ConnectToDB.GetDepartmentList();
            this.sellerComboBox.ItemsSource = ConnectToDB.GetEmployeeList(1);
            this.ordersGrid.ItemsSource = GetOrderListForTable();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = ConnectToDB.GetEmployee(this.surname.Text);
            if (employee != null)
            {
                this.name.Text = employee.Name;
                this.middlename.Text = employee.Middlename;
                this.bitrhDate.Text = $"{employee.BirthDate.Day}.{employee.BirthDate.Month}.{employee.BirthDate.Year}";
                this.sex.SelectedIndex = employee.Sex1;
                this.department.SelectedIndex = employee.DepartmentId - 1;
            }
            else MessageBox.Show("Сотрудник не найден.");
        }

        public void Update_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = ConnectToDB.GetEmployee(this.surname.Text);
            if (employee != null)
            {
                MessageBox.Show("Сотрудник с такой фамилией уже есть в базе.");
                return;
            }
            string[] stringDate = this.bitrhDate.Text.Split('.');            
            int d = 0;
            int m = 0;
            int y = 0;

            try
            {
                if (stringDate.Length != 3) throw new ArgumentException();
                d = int.Parse(stringDate[0]);
                m = int.Parse(stringDate[1]);
                y = int.Parse(stringDate[2]);                
            }
            catch
            {
                MessageBox.Show("Не верный формат даты.");
                return;
            }

            DateTime birthDate = new DateTime(y, m, d);
             
            employee = new Employee(this.surname.Text, this.name.Text, this.middlename.Text, birthDate, this.sex.Text, this.department.Text);

            if (ConnectToDB.AddEmployee(employee))
            {
                MessageBox.Show("Сотрудник успешно добавлен в базу.");
                this.surname.Text = "";
                this.name.Text = "";
                this.middlename.Text = "";
                this.bitrhDate.Text = "";
            }
            else MessageBox.Show($"Ошибка добавления в базу.");
        }

        public void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectToDB.DeleteEmployee(this.surname.Text)) MessageBox.Show("Сотрудник успешно удален.");
            else MessageBox.Show("Сотрудник не найден.");
        }

        private void AddDepartmentClick(object sender, RoutedEventArgs e)
        {
            if (ConnectToDB.AddDepartment(this.newDepartment.Text)) MessageBox.Show("Отдел успешно добавлен.");
            else MessageBox.Show("Отдел не добавлен.");

            this.orderListBox.ItemsSource = ConnectToDB.GetDepartmentList();
        }
                
        private void ClickAddLeader(object sender, RoutedEventArgs e)
        {
            if (ConnectToDB.AddLeader(this.leader.Text)) MessageBox.Show("Руководитель назначен.");
            else MessageBox.Show("Ошибка, руководитель назначен.");
        }

        private void orderListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Department dept = ConnectToDB.GetDepartment(this.orderListBox.SelectedItem.ToString());
            this.leader.ItemsSource = ConnectToDB.GetEmployeeList(dept.Id);
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.clientTextBox.Text == "")
            {
                MessageBox.Show("Укажите заказчика.");
                return;
            }
            Order order = new Order();
            Employee employee = ConnectToDB.GetEmployee(this.sellerComboBox.Text);
            order.AuthorId = employee.Id;
            order.Name = this.clientTextBox.Text;
            order.Number = (new Random()).Next(1, 10000);
            order.Date = DateTime.Now;
            if (!ConnectToDB.AddOrder(order))
            {
                MessageBox.Show("Ошибка. Заказ не добавлен.");
                return;
            }

            MessageBox.Show("Заказ добавлен.");
            this.clientTextBox.Text = "";
            this.ordersGrid.ItemsSource = ConnectToDB.GetOrderList();
        }

        private static List<OrderTable> GetOrderListForTable()
        {
            List<OrderTable> list = new List<OrderTable>();
            var orders = ConnectToDB.GetOrderList();
            foreach (var v in orders)
            {
                OrderTable o = new OrderTable();
                Employee employee = ConnectToDB.GetEmployee(v.AuthorId);
                o.ClientName = v.Name;
                o.Number = v.Id;
                o.OrderDate = v.Date;
                o.OrderNumber = v.Number;
                o.SellerSurname = employee.Surname;
                list.Add(o);
            }
            return list;
        }
    }
}
