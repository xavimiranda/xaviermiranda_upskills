using System;
using System.Collections.Generic;
using System.Text;

namespace Employee
{
    class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }
        public decimal Salary { get; set; }
        public decimal MonthlySalary
        {
            get
            {
                return Salary / 12;
            }
        }
        public Employee (string firstName, string lastName, decimal salary = 0)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
        }
    }
}
