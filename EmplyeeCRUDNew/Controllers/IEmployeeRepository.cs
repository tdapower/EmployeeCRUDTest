using EmplyeeCRUDNew.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EmplyeeCRUDNew.Controllers
{
    interface IEmployeeRepository
    {

        void SaveOrUpdateEmployee(Employee emp);
        void DeleteEmployeeById(int id);

        DataTable GetAllEmployees();

        Employee GetEmployeeById(int id);


        DataTable GetAllDepartments();

    }
}
