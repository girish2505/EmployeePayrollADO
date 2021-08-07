using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PayrollServicesADO
{
    public class EmployeeRepository
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Payroll_Service;Integrated Security=True";
        SqlConnection sqlconnection = new SqlConnection(connectionString);

        public void GetEmployeeDetails()
        {
            try
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
                        employeeModel.BasicPay = Convert.ToDouble(reader["BasicPay"]);
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
                        Console.WriteLine("{0} {1} {2}  {3} {4} {5}  {6}  {7} {8} {9} {10} {11} {12}", employeeModel.empId, employeeModel.name, employeeModel.BasicPay, employeeModel.startDate, employeeModel.emailId, employeeModel.Gender, employeeModel.Department, employeeModel.PhoneNumber, employeeModel.Address, employeeModel.Deductions, employeeModel.TaxablePay, employeeModel.IncomeTax, employeeModel.NetPay);
                        Console.WriteLine("\n");
                    }
                }
                else
                {
                    Console.WriteLine("data not found");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }
    }
}
