using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;

namespace PayrollServicesADO
{
    public class EmployeeRepository
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Payroll_Service";
        SqlConnection sqlconnection = new SqlConnection(connectionString);

        public void GetEmployeeDetails()
        {
            EmployeeModel employeeModel = new EmployeeModel();
            string query = @"select * from employee_payroll";
            SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
            this.sqlconnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    employeeModel.empId = Convert.ToInt32(reader["empId"]);
                    employeeModel.name = reader["name"].ToString();
                    employeeModel.Salary = Convert.ToDouble(reader["BasicPay"]);
                    employeeModel.startDate = reader.GetDateTime(3);
                    employeeModel.emailId = reader["emailId"].ToString();
                    employeeModel.Gender = reader["Gender"].ToString();
                    employeeModel.Department = reader["Department"].ToString();
                    employeeModel.PhoneNumber = Convert.ToDouble(reader["PhoneNumber"]);
                    employeeModel.Address = reader["Address"].ToString();
                    employeeModel.Deductions = Convert.ToDouble(reader["Deductions"]);
                    employeeModel.TaxablePay = Convert.ToDouble(reader["TaxablePay"]);
                    employeeModel.IncomeTax = Convert.ToDouble(reader["IncomeTax"]);
                    employeeModel.NetPay = Convert.ToDouble(reader["NetPay"]);
                    Console.WriteLine("{0} {1} {2}  {3} {4} {5}  {6}  {7} {8} {9} {10} {11} {12}", employeeModel.empId, employeeModel.name, employeeModel.Salary, employeeModel.startDate, employeeModel.emailId, employeeModel.Gender, employeeModel.Department, employeeModel.PhoneNumber, employeeModel.Address, employeeModel.Deductions, employeeModel.TaxablePay, employeeModel.IncomeTax, employeeModel.NetPay);
                    Console.WriteLine("\n");
                }
            }
            else
            {
                Console.WriteLine("data not found");
            }
            reader.Close();


        }
        public void UpdateSalaryColumn(EmployeeModel model)
        {
            try
            {
                sqlconnection.Open();
                string query = @"update employee_payroll set Salary=3000000 where name='girish'";
                SqlCommand command = new SqlCommand(query, sqlconnection);

                int result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Salary Updated Successfully ");
                }
                else
                {
                    Console.WriteLine("salary not updated successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        public int UpdateSalaryUsingStoredProcedure(EmployeeModel model)
        {
            int result;
            using (this.sqlconnection)
            {
                SqlCommand command = new SqlCommand("dbo.UpdateEmployeeDetails", this.sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("empId", model.empId);
                command.Parameters.AddWithValue("empName", model.name);
                command.Parameters.AddWithValue("BasicPay", model.Salary);
                sqlconnection.Open();
                result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Updated Successfully using stored procedure");
                }
                else
                {
                    Console.WriteLine("Not Updated!!!");
                    return default;
                }
                return result;
            }
        }
        public int RetrieveDataBasedOnDate(EmployeeModel model)
        {
            
            int count = 0;
            using (sqlconnection)
            {
                
                string query = @"select * from employee_payroll where startDate between('2021-01-01') and getdate()";
                
                SqlCommand command = new SqlCommand(query, this.sqlconnection);
                
                sqlconnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    { 
                        DisplayEmployeeDetails(reader);
                        count++;
                    }
                }
                reader.Close();
            }
            return count;
        }
        public void DisplayEmployeeDetails(SqlDataReader reader)
        {
            EmployeeModel model = new EmployeeModel();
           
            model.empId = Convert.ToInt32(reader["empId"]);
            model.name = reader["name"].ToString();
            model.Salary = Convert.ToDouble(reader["Salary"]);
            model.startDate = reader.GetDateTime(3);
            model.emailId = reader["emailId"].ToString();
            model.Gender = reader["Gender"].ToString();
            model.Department = reader["Department"].ToString();
            model.PhoneNumber = Convert.ToDouble(reader["PhoneNumber"]);
            model.Address = reader["Address"].ToString();
            model.Deductions = Convert.ToDouble(reader["Deductions"]);
            model.TaxablePay = Convert.ToDouble(reader["TaxablePay"]);
            model.IncomeTax = Convert.ToDouble(reader["IncomeTax"]);
            model.NetPay = Convert.ToDouble(reader["NetPay"]);
            Console.WriteLine("{0} {1} {2}  {3} {4} {5}  {6}  {7} {8} {9} {10} {11} {12}", model.empId, model.name, model.Salary, model.startDate, model.emailId, model.Gender, model.Department, model.PhoneNumber, model.Address, model.Deductions, model.TaxablePay, model.IncomeTax, model.NetPay);
            Console.WriteLine("\n");

        }
    }
}
