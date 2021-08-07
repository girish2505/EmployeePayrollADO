using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PayrollServicesADO
{
    class ER
    {
        public static string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Payroll_Service;Integrated Security=True";

        SqlConnection sqlConnection = new SqlConnection(connection);

        EmployeeModel employeeModel = new EmployeeModel();

        public int RetrieveAllData()
        {
            int count = 0;
            sqlConnection.Open();

            string query = @"Select CompanyID,CompanyName,
EmployeeID,EmployeeName,EmployeeAddress,EmployeePhoneNum,StartDate,Gender,
BasicPay,TaxablePay,IncomeTax,NetPay,Deductions,DepartmentId,DepartName
from Company
inner join Employee on Company.CompanyID = Employee.Company_Id
inner join PayRollCalculate on PayRollCalculate.Employee_Id = Employee.EmployeeId
inner join EmployeeDept on EmployeeDept.Employee_Id = Employee.EmployeeID
inner join DepartmentTable on DepartmentTable.DepartmentId = EmployeeDept.Dept_Id";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read())
                {

                    DisplayEmployeeDetails(reader);
                    count++;
                }

                reader.Close();
            }
            sqlConnection.Close();
            return count;
        }
        
        public int UpdateSalaryQuery()
        {
            sqlConnection.Open();
            string query = @"update PayrollCalculate set BasicPay = 3000000 where Employee_Id = (Select EmployeeID from Employee where EmployeeName = 'lahari')";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            int result = sqlCommand.ExecuteNonQuery();
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (result != 0)
            {
                Console.WriteLine("Salary Updated Successfully!");
            }
            else
            {
                Console.WriteLine("Not Updated!");
            }
            sqlConnection.Close();
            return result;
        }
        public int DataBasedOnDateRange()
        {
            int count = 0;

            using (sqlConnection)
            {
                string query = @"select CompanyID,CompanyName,EmployeeID,EmployeeName,StartDate,BasicPay from Company
            inner join Employee on Company.CompanyID=Employee.Company_Id and StartDate between Cast('2020-01-01' as Date) and GetDate()
            inner join PayRollCalculate on Employee.EmployeeID=PayRollCalculate.Employee_Id;";
                SqlCommand command = new SqlCommand(query, this.sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        EmployeeModel model = new EmployeeModel();

                        model.empId = Convert.ToInt32(sqlDataReader["EmployeeID"]);
                        model.CompanyID = Convert.ToInt32(sqlDataReader["CompanyID"]);
                        model.name = sqlDataReader["EmployeeName"].ToString();
                        model.CompanyName = sqlDataReader["CompanyName"].ToString();
                        model.BasicPay = Convert.ToDouble(sqlDataReader["BasicPay"]);
                        model.startDate = sqlDataReader.GetDateTime(4);

                        Console.WriteLine("EmployeeId :{0}\t CompanyId :{1}\t EmployeeName:{2}\t CompanyName:{3}\t BasicPay:{4},StartDate:{5}", model.empId, model.CompanyID, model.name, model.CompanyName, model.BasicPay, model.startDate);
                        Console.WriteLine("\n");
                        count++;
                    }
                }
                sqlDataReader.Close();
            }
            return count;
        }
        public string PerformAggregateFunctions(string Gender)
        {
            string result = null;

            string query1 =
@"select sum(Salary) as TotalSalary,min(Salary) as MinSalary,max(Salary) as MaxSalary,Round(avg(Salary),0) as AvgSalary,Gender,Count(*)  
from Employee
inner join PayRollCalculate on Employee.EmployeeID=PayRollCalculate.Employee_Id
where Gender =" + "'" + Gender + "'" + " group by Gender";

            SqlCommand sqlCommand = new SqlCommand(query1, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {

                while (sqlDataReader.Read())
                {
                    Console.WriteLine("Total Salary {0}", sqlDataReader[0]);
                    Console.WriteLine("Average Salary {0}", sqlDataReader[1]);
                    Console.WriteLine("Minimum Salary {0}", sqlDataReader[2]);
                    Console.WriteLine(" Maximum Salary {0}", sqlDataReader[3]);
                    Console.WriteLine("No of employess {0}", sqlDataReader[4]);
                    result += sqlDataReader[4] + " " + sqlDataReader[0] + " " + sqlDataReader[1] + " " + sqlDataReader[2] + " " + sqlDataReader[3];
                }
            }
            else
            {
                result = "empty";
            }
            sqlDataReader.Close();
            return result;

        }
        public void DisplayEmployeeDetails(SqlDataReader reader)
        {
            EmployeeModel model = new EmployeeModel();


            model.empId = Convert.ToInt32(reader["EmployeeID"]);
            model.name = reader["EmployeeName"].ToString();
           // model.Salary = Convert.ToDouble(reader["Salary"]);
            model.startDate = reader.GetDateTime(6);
            //model.emailId = reader["emailId"].ToString();
            model.Gender = reader["Gender"].ToString();
            model.Department = reader["DepartName"].ToString();
            model.PhoneNumber = Convert.ToDouble(reader["EmployeePhoneNum"]);
            model.Address = reader["EmployeeAddress"].ToString();
            model.Deductions = Convert.ToDouble(reader["Deductions"]);
            model.TaxablePay = Convert.ToDouble(reader["TaxablePay"]);
            model.IncomeTax = Convert.ToDouble(reader["IncomeTax"]);
            model.BasicPay = Convert.ToDouble(reader["BasicPay"]);
            model.NetPay = Convert.ToDouble(reader["NetPay"]);
            model.CompanyID = Convert.ToInt32(reader["CompanyID"]);
            model.CompanyName = reader["CompanyName"].ToString();
            Console.WriteLine("\nCompany ID: {0} \t Company Name: {1} \nEmployee ID: {2} \t Employee Name: {3} \t Salary {4} \t Deduction: {5} \t Income Tax: {6} \t Taxable Pay: {7} \t NetPay: {8} \nGender: {9} \t PhoneNumber: {10} \t Department: {11} \t Address: {12} \t Start Date: {13} \t BasicPay {14} :", model.CompanyID, model.CompanyName, model.empId, model.name, model.Salary, model.Deductions, model.IncomeTax, model.TaxablePay, model.NetPay, model.Gender, model.PhoneNumber, model.Department, model.Address, model.startDate,model.BasicPay);
        }
    }
}
