using System;

namespace PayrollServicesADO
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Employee payroll services ADO");
            EmployeeRepository repo = new EmployeeRepository();
            EmployeeModel model = new EmployeeModel();
            Console.WriteLine("1.To Retrieve all Data from Sql server");
            Console.WriteLine("2.To Update Salary to 3000000");
            Console.WriteLine("3.Update the Salary Using Stored Procedure");
            Console.WriteLine("4.Retrieve employee details between the date range");
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
                case 3:
                    repo.UpdateSalaryUsingStoredProcedure(model);
                    repo.GetEmployeeDetails();
                    break;
                case 4:
                    EmployeeRepository repository = new EmployeeRepository();
                    repository.RetrieveDataBasedOnDate(model);
                    break;

            }
        }
    }
}
