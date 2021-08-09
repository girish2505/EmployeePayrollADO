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
            Console.WriteLine("5.Performing Aggregate functions");
            Console.WriteLine("6.Retrieve data from sql");
            Console.WriteLine("7.Update Salary data in table");
            Console.WriteLine("8.Print the employee details between date range");
            Console.WriteLine("9.Insert into Tables");
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
                case 5:
                    EmployeeRepository repository1 = new EmployeeRepository();
                    repository1.AggregateFunctions("M");
                    break;
                case 6:
                    ER er = new ER();
                    er.RetrieveAllData();
                    break;
                case 7:
                    ER eRRepository = new ER();
                    eRRepository.UpdateSalaryQuery();
                    eRRepository.RetrieveAllData();
                    break;
                case 8:
                    ER eR = new ER();
                    eR.DataBasedOnDateRange();
                    break;
                case 9:
                    Transaction transaction = new Transaction();
                    transaction.InsertIntoTables();
                    break;
                case 10:
                    Transaction transaction1 = new Transaction();
                    transaction1.AddIsActiveColumn();
                    break;
                case 11:
                    Transaction trans = new Transaction();
                    trans.MaintainListforAudit(1);
                    Transaction transaction2 = new Transaction();
                    transaction2.RetrieveAllData();
                    break;
            }
        }
    }
}
