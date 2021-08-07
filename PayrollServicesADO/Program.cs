using System;

namespace PayrollServicesADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Employee payroll services ADO");
            EmployeeRepository repo = new EmployeeRepository();
            EmployeeModel model = new EmployeeModel();
            Console.WriteLine("Enter 1-To Retrieve all Data from Sql server");
            Console.WriteLine("Enter 2-To Update Salary to 3000000");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    repo.GetEmployeeDetails();
                    break;
                case 2:
                    repo.UpdateSalaryColumn(model);
                    repo.GetEmployeeDetails();
                    break;
            }
        }
    }
}
