using EmplyeeCRUDNew.Controllers;
using EmplyeeCRUDNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmplyeeCRUDNew.Views
{
    public partial class EmployeeManagement : System.Web.UI.Page
    {
        IEmployeeRepository EmployeeRepository = new EmployeeController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Enabled = false;
                btnDelete.Attributes.Add("onClick", "if(confirm('Are you sure to delete?','Confirmation')){}else{return false;}");
                FIllEmployeeGrid();
                LoadDepartments();
            }
        }

        protected void btnSaveOrUpdate_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();

            emp.EmployeeID = (hfEmployeeId.Value == "" ? 0 : Convert.ToInt32(hfEmployeeId.Value));
            emp.Name = txtName.Text;
            emp.EPF = txtEPF.Text;
            emp.Department = ddlDepartment.SelectedValue;
            emp.Designation = txtDesignation.Text;

            EmployeeRepository.SaveOrUpdateEmployee(emp);
            Clear();
            FIllEmployeeGrid();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (hfEmployeeId.Value != "")
            {
                EmployeeRepository.DeleteEmployeeById(Convert.ToInt32(hfEmployeeId.Value));
                Clear();
                FIllEmployeeGrid();
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }


        public void Clear()
        {
            hfEmployeeId.Value = "";
            txtName.Text = "";
            txtEPF.Text = "";
            txtDepartment.Text = "";
            txtDesignation.Text = "";

            lblMsg.Text = "";
            btnDelete.Enabled = false;
            btnSaveOrUpdate.Text = "Save";
        }


        protected void lbtnView_Click(object sender, EventArgs e)
        {
            int empId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            Employee emp = new Employee();
            emp = EmployeeRepository.GetEmployeeById(empId);

            hfEmployeeId.Value = emp.EmployeeID.ToString();
            txtName.Text = emp.Name;
            txtEPF.Text = emp.EPF;
            txtDepartment.Text = emp.Department;
            ddlDepartment.SelectedValue = emp.Department;
            txtDesignation.Text = emp.Designation;

            btnDelete.Enabled = true;
        }


        public void FIllEmployeeGrid()
        {
            grdEmployees.DataSource = EmployeeRepository.GetAllEmployees();
            grdEmployees.DataBind();
        }


        private void LoadDepartments()
        {
            ddlDepartment.DataSource = EmployeeRepository.GetAllDepartments();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();
        }

    }
}