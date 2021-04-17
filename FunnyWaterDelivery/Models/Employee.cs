using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyWaterDelivery
{
    class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Middlename { get; set; }
        public DateTime BirthDate { get; set; }
        [NotMapped]
        private Sex sexField;
        [NotMapped]
        public Sex SexField 
        { 
            get 
            {
                return sexField;
            } 
            set
            {
                sexField = value;
                if (sexField == FunnyWaterDelivery.Sex.male) Sex1 = 0;
                else Sex1 = 1;
            }
        }
        [NotMapped]
        private Department dept;
        [NotMapped]
        public Department Dept 
        {
            get
            { 
                return dept;
            } 
            set
            {
                dept = value;
                DepartmentId = dept.Id;
            }
        }

        [Column("Sex")]
        public int Sex1 { get; set; }
        [Column("DepartmentId")]
        public int DepartmentId { get; set; }

        public Employee() { }

        public Employee(string surname, string name, string middlename, DateTime birthDate, string sex, string department)
        {
            this.Surname = surname;
            this.Name = name;
            this.Middlename = middlename;
            this.BirthDate = birthDate;
            this.SexField = Sex.male;
            if (sex == "Ж") this.SexField = Sex.female;
            this.Dept = ConnectToDB.GetDepartment(department);
        }
    }
}
