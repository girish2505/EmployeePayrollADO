using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollServicesADO
{
    public class EmployeeModel
    {
        public int empId { get; set; }
        public string name { get; set; }
        public double Salary { get; set; }
        public DateTime startDate { get; set; }
        public string emailId { get; set; }
        public string Gender { get; set; }
        public double PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public double Deductions { get; set; }
        public double TaxablePay { get; set; }
        public double IncomeTax { get; set; }
        public double NetPay { get; set; }
        public double BasicPay { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int IsActive { get; set; }


    }
}
