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
            EmployeeModel data = new EmployeeModel();
            string query = @"select * from employee_payroll";
            SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
            this.sqlconnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    data.empId = reader.GetInt32(0);
                    data.name = reader.GetString(1);
                    data.Salary = reader.GetDouble(2);
                    data.startDate = reader.GetDateTime(3);
                    data.emailId = reader.GetString(4);
                    data.Gender = reader.GetString(5);
                    data.PhoneNumber = reader.GetInt64(6);
                    data.Department = reader.GetString(7);
                    data.Address = reader.GetString(8);
                    data.Deductions = reader.GetDouble(9);
                    data.TaxablePay = reader.GetDouble(10);
                    data.IncomeTax = reader.GetDouble(11);
                    data.NetPay = reader.GetDouble(12);
                    //data.gender,data.Phone,data.Address,data.Departmenr,data.Basicpay,data.Deductions,data.TaxablePay,data.IncomeTax,data.Netpay);
                    Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", data.empId, data.name, data.Salary, data.startDate,data.emailId,data.Gender, data.PhoneNumber, data.Department, data.Address,data.Deductions, data.TaxablePay, data.IncomeTax, data.NetPay);
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
    }
}
