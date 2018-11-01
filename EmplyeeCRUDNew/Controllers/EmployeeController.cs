using EmplyeeCRUDNew.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmplyeeCRUDNew.Controllers
{
    public class EmployeeController : IEmployeeRepository
    {
       // SqlConnection con = new SqlConnection(@"data source=.;initial catalog=EmployeeCRUD;user id=sa;pwd=tdapower;integrated security=true;");
        SqlConnection con=new SqlConnection(@Properties.Settings.Default.conStr);
        public void SaveOrUpdateEmployee(Models.Employee emp)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("EmployeeCreateOrUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@EPF", emp.EPF);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

        }

        public void DeleteEmployeeById(int id)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("EmployeeDeleteByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }

        public System.Data.DataTable GetAllEmployees()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("EmployeeGetAll",con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                
                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Close();

                return dt;

            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }

        public Models.Employee GetEmployeeById(int id)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("EmployeeGetByID", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@EmployeeID", id);

                DataTable dt = new DataTable();
                da.Fill(dt);

                Employee emp = new Employee();
                emp.EmployeeID=Convert.ToInt32( dt.Rows[0]["EmployeeID"]);
                emp.Name=dt.Rows[0]["Name"].ToString();
                emp.EPF = dt.Rows[0]["EPF"].ToString();
                emp.Department = dt.Rows[0]["Department"].ToString();
                emp.Designation = dt.Rows[0]["Designation"].ToString();
         



                con.Close();

                return emp;

            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }


        public DataTable GetAllDepartments()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter("GetAllDepartments", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}