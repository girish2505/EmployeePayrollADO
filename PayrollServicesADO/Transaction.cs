using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    }
}
