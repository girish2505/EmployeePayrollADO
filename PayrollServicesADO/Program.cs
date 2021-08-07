using System;

namespace PayrollServicesADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Employee payroll services ADO");
            EmployeeRepository repo = new EmployeeRepository();
            repo.GetEmployeeDetails();
        }
    }
}
