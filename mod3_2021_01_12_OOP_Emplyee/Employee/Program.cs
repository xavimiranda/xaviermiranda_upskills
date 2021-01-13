using System;
using System.Collections.Generic;

namespace Employee
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee("Arthur", "Dent", 9000));
            employees.Add(new Employee("Ford", "Prefect", 20000));
            employees.Add(new Employee("Zaphod", "Beeblebrox", 1000000));

            foreach (Employee e in employees)
                Console.WriteLine($"{e.FullName,30} -> Yearly: {e.Salary,12:N2}   Monthly: {e.MonthlySalary,10:N2}");
        }
    }
}
