using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmplyeeCRUDNew.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string EPF { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
    }
}


