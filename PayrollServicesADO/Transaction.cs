using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace PayrollServicesADO
{
    class Transaction
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Payroll_Service;Integrated Security=True";
        SqlConnection SqlConnection = new SqlConnection(connectionString);
        public int InsertIntoTables()
        {
            int flag = 0;
            using (SqlConnection)
            {
                SqlConnection.Open();
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                { 
                    sqlCommand.CommandText = "Insert into Employee(Company_Id, EmployeeName, EmployeePhoneNum, EmployeeAddress, StartDate, Gender)values(1,'abc','1234567890','ttt street','2020-05-01','M')";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "Insert into PayRollCalculate (Employee_Id,BasicPay) values('5','90000')";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "update PayRollCalculate set Deductions = (BasicPay *20)/100 where Employee_Id = '2'";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "update PayRollCalculate set TaxablePay = (BasicPay - Deductions) where Employee_Id = '2'";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "update PayRollCalculate set IncomeTax = (TaxablePay * 10) / 100 where Employee_Id = '2'";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "update PayRollCalculate set NetPay = (BasicPay - IncomeTax) where Employee_Id = '2'";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "Insert into EmployeeDept values('1','2')";
                    sqlCommand.ExecuteNonQuery();
                 
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated Successfully!");
                    flag = 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlTransaction.Rollback();
                    flag = 0;
                }
            }
            return flag;
        }
        public int DeleteUsingCasadeDelete()
        {
            int result = 0;
            using (SqlConnection)
            {
                SqlConnection.Open();
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = "Delete from Employee where EmployeeName='Vishnu'";
                    sqlCommand.ExecuteNonQuery();
                    result++;
                    sqlTransaction.Commit();
                    Console.WriteLine("Deleted Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlTransaction.Rollback();
                }
            }
            return result;
        }
        public string AddIsActiveColumn()
        {
            string result = null;
            using (SqlConnection)
            {
                SqlConnection.Open();
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = "Alter table Employee add IsActive int NOT NULL default 1";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    result = "IsActive Column Added";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlTransaction.Rollback();
                    result = "Column not Updated";
                }
            }
            SqlConnection.Close();
            return result;
        }
        public int MaintainListforAudit(int IDValue)
        {
            int res = 0;
            SqlConnection.Open();
            using (SqlConnection)
            {
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = @"Update Employee set IsActive = 0 where EmployeeID = '" + IDValue + "'";
                    sqlCommand.ExecuteNonQuery();
                    res++;
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    
                    sqlTransaction.Rollback();
                }
            }
            SqlConnection.Close();
            return res;
        }
        public void RetrieveAllData()
        {
            SqlConnection.Open();

            try
            {
                string query = @"Select CompanyID,CompanyName,EmployeeID,EmployeeName,EmployeeAddress,IsActive,EmployeePhoneNum,StartDate,Gender,BasicPay,TaxablePay,IncomeTax,NetPay,Deductions,DepartmentId,DepartName
from Company inner join Employee on Company.CompanyID=Employee.Company_Id and Employee.IsActive=1
inner join PayRollCalculate on PayRollCalculate.Employee_Id=Employee.EmployeeId
inner join EmployeeDept on EmployeeDept.Employee_Id=Employee.EmployeeID
inner join DepartmentTable on DepartmentTable.DepartmentId=EmployeeDept.Dept_Id";
                SqlCommand sqlCommand = new SqlCommand(query, SqlConnection);
                DisplayEmployeeDetails(sqlCommand);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlConnection.Close();
        }
        public void DisplayEmployeeDetails(SqlCommand sqlCommand)
        {
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<EmployeeModel> employeeList = new List<EmployeeModel>();

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
                    model.Deductions = Convert.ToDouble(sqlDataReader["Deductions"]);
                    model.IncomeTax = Convert.ToDouble(sqlDataReader["IncomeTax"]);
                    model.TaxablePay = Convert.ToDouble(sqlDataReader["TaxablePay"]);
                    model.NetPay = Convert.ToDouble(sqlDataReader["NetPay"]);
                    model.Gender = Convert.ToString(sqlDataReader["Gender"]);
                    model.PhoneNumber = Convert.ToInt64(sqlDataReader["EmployeePhoneNum"]);
                    model.Department = sqlDataReader["DepartName"].ToString();
                    model.Address = sqlDataReader["EmployeeAddress"].ToString();
                    model.startDate = Convert.ToDateTime(sqlDataReader["StartDate"]);
                    model.IsActive = Convert.ToInt32(sqlDataReader["IsActive"]);
                    Console.WriteLine("\nCompany ID: {0} \t Company Name: {1} \nEmployee ID: {2} \t Employee Name: {3} \nBasic Pay: {4} \t Deduction: {5} \t Income Tax: {6} \t Taxable Pay: {7} \t NetPay: {8} \nGender: {9} \t PhoneNumber: {10} \t Department: {11} \t Address: {12} \t Start Date: {13} \t IsActive: {14}", model.CompanyID, model.CompanyName, model.empId, model.name, model.BasicPay, model.Deductions, model.IncomeTax, model.TaxablePay, model.NetPay, model.Gender, model.PhoneNumber, model.Department, model.Address, model.startDate, model.IsActive);
                    employeeList.Add(model);
                }
                sqlDataReader.Close();
            }
        }
        public bool ImplementingWithoutUsingThread()
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                RetrieveAllData();

                stopWatch.Stop();
                Console.WriteLine("Duration without Thread excecution : {0} ", stopWatch.ElapsedMilliseconds);
                int elapsedTime = Convert.ToInt32(stopWatch.ElapsedMilliseconds);
                if (elapsedTime != 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return false;
        }
    }
}
