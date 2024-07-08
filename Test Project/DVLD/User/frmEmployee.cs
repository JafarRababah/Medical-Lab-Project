using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Classes;
using DVLD.People;
using DVLD.Controls;
using System.Runtime.Remoting.Messaging;

namespace DVLD.Employee
{
    public partial class frmEmployee : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _EmployeeID = -1;
        clsEmployee _Employee;
        clsPerson _Person;
        public frmEmployee()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmEmployee(int EmployeeID)
        {
            InitializeComponent();
            
            _Mode = enMode.Update;
            _EmployeeID = EmployeeID;
        }
        private void _FillDepartmentsInComoboBox()
        {
            DataTable dtDepartments = clsDepartment.GetAllDepartments();

            foreach (DataRow row in dtDepartments.Rows)
            {
                cbDepartment.Items.Add(row["DepartmentName"]);
            }
        }
        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillDepartmentsInComoboBox();
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Employee";
                this.Text = "Add New Employee";
                _Employee = new clsEmployee();

                tpEmployeeInfo.Enabled = false;

                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update Employee";
                this.Text = "Update Employee";

                tpEmployeeInfo.Enabled = true;
                btnSave.Enabled = true;


            }

            txtEmployeeSalary.Text = "";
            txtPosition.Text = "";
            cbDepartment.Text = "";
            cbQualification.Text = "";


        }

        private void _LoadData()
        {

            _Employee = clsEmployee.FindByEmployeeID(_EmployeeID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_Employee == null)
            {
                MessageBox.Show("No Employee with ID = " + _Employee, "Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            //the following code will not be executed if the person was not found
            lblEmployeeID.Text = _Employee.EmployeeID.ToString();
            txtEmployeeSalary.Text = _Employee.Salary.ToString();
            txtPosition.Text = _Employee.Position.ToString();
            cbDepartment.Text = _Employee.DepartmentID;
            cbQualification.Text = _Employee.QualificationID;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_Employee.PersonID);
            cbDepartment.SelectedIndex = cbDepartment.FindString(_Employee.DepartmentID);

        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            string DepartmentID = clsDepartment.FindByName(cbDepartment.Text).DepartmentID;
            _Employee.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _Employee.Salary = Convert.ToSingle(txtEmployeeSalary.Text);
            _Employee.Position = txtPosition.Text.Trim();
            // _Employee.DepartmentID = cbDepartment.Text;
            _Employee.DepartmentID = DepartmentID;
            _Employee.QualificationID = cbQualification.Text;
            _Employee.CreatedByUserID = clsGlobal.CurrentUser.UserID;



            if (_Employee.Save())
            {
                lblEmployeeID.Text = _Employee.EmployeeID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Employee";
                this.Text = "Update Employee";
                btnSave.Enabled = false;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpEmployeeInfo.Enabled = true;
                tcEmployeeInfo.SelectedTab = tcEmployeeInfo.TabPages["tpLoginInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

                if (clsEmployee.isEmployeeExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected Person already has a Employee, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    btnSave.Enabled = true;
                    tpEmployeeInfo.Enabled = true;
                    tcEmployeeInfo.SelectedTab = tcEmployeeInfo.TabPages["tpEmployeeInfo"];
                }
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
