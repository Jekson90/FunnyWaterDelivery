using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyWaterDelivery
{
    class ConnectToDB
    {
        public static Employee GetEmployee(string surname)
        {
            Employee e = null;
            using (VodovozContext data = new VodovozContext())
            {
                var employees = data.Employees.ToList().Where<Employee>(e => e.Surname == surname);
                foreach (var v in employees)
                    e = v;
            }  
            return e;
        }

        public static Employee GetEmployee(int id)
        {
            Employee e = null;
            using (VodovozContext data = new VodovozContext())
            {
                var employees = data.Employees.ToList().Where<Employee>(e => e.Id == id);
                foreach (var v in employees)
                    e = v;
            }
            return e;
        }

        public static List<string> GetEmployeeList(int departmentId)
        {
            List<string> list = new List<string>();

            using(VodovozContext data = new VodovozContext())
            {
                var employeeList = data.Employees.ToList().Where<Employee>(e => e.DepartmentId == departmentId);
                foreach (var v in employeeList)
                    list.Add(v.Surname);
            }
            return list;
        }

        public static bool AddEmployee(Employee employee)
        {
            try
            {
                using (VodovozContext data = new VodovozContext())
                {
                    data.Employees.Add(employee);
                    data.SaveChanges();
                }
                return true;
            } catch
            {
                return false;
            }
        }

        public static bool DeleteEmployee(string surname)
        {
            try
            {
                using (VodovozContext data = new VodovozContext())
                {
                    var employees = data.Employees.ToList().Where<Employee>(e => e.Surname == surname);
                    foreach (var v in employees)
                    {
                        data.Employees.Remove(v);
                        data.SaveChanges();
                        
                    }
                }
                return true;
            } catch
            {
                return false;
            }
        }

        public static List<string> GetDepartmentList()
        {
            List<string> list = new List<string>();

            using (VodovozContext data = new VodovozContext())
            {
                var departmensList = data.Departments.ToList();
                foreach (var v in departmensList)
                    list.Add(v.Name);
            }
            return list;
        }

        public static Department GetDepartment(string department)
        {
            Department dpt = null;
            using (VodovozContext data = new VodovozContext())
            {
                var departmensList = data.Departments.ToList().Where<Department>(d => d.Name == department);
                foreach (var v in departmensList)
                    dpt = v;
            }

            return dpt;
        }

        public static bool AddDepartment(string name)
        {
            try
            {
                using (VodovozContext data = new VodovozContext())
                {
                    Department dpt = new Department();
                    dpt.Name = name;
                    data.Departments.Add(dpt);
                    data.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddLeader(string surname)
        {
            Employee employee = ConnectToDB.GetEmployee(surname);
            Department dpt = new Department();
            try
            {
                using (VodovozContext data = new VodovozContext())
                {
                    var department = data.Departments.ToList().Where<Department>(d => d.Id == employee.DepartmentId);
                    foreach (var v in department)
                        v.LeaderId = employee.Id;
                    data.SaveChanges();
                }
                return true;
            } catch
            {
                return false;
            }
        }

        public static bool AddOrder(Order order)
        {
            try
            {
                using (VodovozContext data = new VodovozContext())
                {
                    data.Orders.Add(order);
                    data.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<Order> GetOrderList()
        {
            List<Order> orderList = new List<Order>();
            using (VodovozContext data = new VodovozContext())
            {
                var list = data.Orders.ToList();
                foreach (var v in list)
                    orderList.Add(v);
            }
            return orderList;
        }
    }
}
