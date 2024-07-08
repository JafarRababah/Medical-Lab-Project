using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Classes;
using DVLD.Properties;
using DVLD_Buisness;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Runtime.ConstrainedExecution;
using System.Data.SqlClient;
using DVLD.Controls.ApplicationControls;
using Microsoft.Reporting.WinForms;

namespace DVLD.Tests
{
    public partial class frmPatientTest : Form
    {
        public static List<Tuple<int, string>> Tests = new List<Tuple<int, string>>();
        private DataTable _dtAllTests;
        
        //private int _PersonID;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private clsPatientTest _patientTest = new clsPatientTest();
        public static string ConnectionString = "Server=.;Database=DVLD; Integrated Security=True;";
        SqlConnection connection = new SqlConnection(ConnectionString);
        private int _PersonID=-1;
        private int _LocalDrivingLicenseApplicationID = -1;
        public frmPatientTest()
        {
            InitializeComponent();
            //ctrlPersonCard1.LoadPersonInfo(PersonID);
            _Mode=enMode.AddNew;
        }
        public frmPatientTest(int PersonID,int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
            _PersonID=PersonID;
            _LocalDrivingLicenseApplicationID=LocalDrivingLicenseApplicationID;
            _Mode =enMode.Update;
        }
        public frmPatientTest(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
            _PersonID = PersonID;
           
            _Mode = enMode.Update;
        }
        public void DataLoad()
        {
            
           
            _dtAllTests = clsPatientTest.GetAllTests(_LocalDrivingLicenseApplicationID);

            dgvTests.DataSource = _dtAllTests;
            if (dgvTests.Rows.Count != 0)
            {
                dgvTests.Columns[1].ReadOnly = true;
                dgvTests.Columns[2].ReadOnly = true;
                dgvTests.Columns[3].ReadOnly = true;
                dgvTests.Columns[5].ReadOnly = true;
            }
          

            cbFilterBy.SelectedIndex = 2;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        List<Tuple<int, string>> data = new List<Tuple<int, string>>();
        //private void SetItems()
        //{
        //    lbxTestSelected.ValueMember = "Item1";
        //    lbxTestSelected.DisplayMember = "Item2";
        //    BindingSource bs = new BindingSource();
        //    bs.DataSource = data.Distinct();
        //    lbxTestSelected.DataSource = bs;
        //}
        
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            dgvTests.AllowUserToAddRows = false;
           
            int ID = _LocalDrivingLicenseApplicationID;
           
            for (int i = 0; i < dgvTests.Rows.Count; i++)
            {
                SqlConnection connection = new SqlConnection(ConnectionString);

                string query = @"Update  PatientTests  
                            set 
                                Result=@Result
                                
                                where ID = @ID and TestListID=@TestListID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Result", dgvTests.Rows[i].Cells[4].Value);
                command.Parameters.AddWithValue("@TestListID", dgvTests.Rows[i].Cells[0].Value);
                command.Parameters.AddWithValue("ID", ID);
                //  command.Parameters.AddWithValue("@PatientID", PatientID);

                try
                {
                    connection.Open();
                    // rowsAffected= command.ExecuteNonQuery();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    // return false;
                }

                finally
                {
                    connection.Close();
                }
            }
            MessageBox.Show("good job");

        

    }

        private void frmPateintTest_Load(object sender, EventArgs e)
        {
           
            
            DataLoad();
           
        }

        private void dgvTests_DoubleClick(object sender, EventArgs e)
        {
            //if (dgvTests.CurrentRow != null)
            //{
            //    data.Add(new Tuple<int, string>((int)dgvTests.CurrentRow.Cells[0].Value, dgvTests.CurrentRow.Cells[1].Value.ToString()));

            //}
            ////SetItems();
            //btnDeleteAll.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Tests.Clear();
            //for (int i = 0; i < lbxTestSelected.Items.Count; i++)
            //{
            //    lbxTestSelected.SelectedIndex = i;
            //    Tests.Add(new Tuple<int, string>((int)lbxTestSelected.SelectedValue, lbxTestSelected.GetItemText(lbxTestSelected.Items[i])));
            //    //lbCategory.SelectedValue= lbCategory.Items[i].ToString();
            //}
            //txtTest.Text = "";
            //foreach (var i in Tests)
            //{
            //    txtTest.Text += i.Item2 + " , ";
            //}
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 

            switch (cbFilterBy.Text)
            {
                case "Test List ID":
                    FilterColumn = "TestListID";
                    break;

               
                case "Test Title":
                    FilterColumn = "TestTitle";
                    break;


                case "Test Description":
                    FilterColumn = "TestDescription";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }
            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtSearch.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllTests.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvTests.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "TestTitle" && FilterColumn != "TestDescription")
                //in this case we deal with numbers not string.
                 
                _dtAllTests.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtSearch.Text.Trim());
            else
                _dtAllTests.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtSearch.Text.Trim());

            lblRecordsCount.Text = _dtAllTests.Rows.Count.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Visible = (cbFilterBy.Text != "None");


            if (cbFilterBy.Text == "None")
            {
                txtSearch.Enabled = false;
            }
            else
                txtSearch.Enabled = true;

            txtSearch.Text = "";
            txtSearch.Focus();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Test List ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            dgvTests.AllowUserToAddRows = false;
            _patientTest.ID = clsLicense.GetActiveLicenseIDByPersonID(ctrlPersonCard1.PersonID);
            _patientTest.PatientID = clsPatient.FindByPersonID(ctrlPersonCard1.PersonID).PatientID;
            
            
            foreach (DataGridViewRow row in dgvTests.Rows)
            {
                _patientTest.TestListID = (int)row.Cells["TestListID"].Value;
               
                
                   
                if (_patientTest.Save())


                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                else
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

            

        }

        private void dgvTests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvTests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            string Result = dgvTests.CurrentRow.Cells[5].Value.ToString();
            int _LicenseID = clsLicense.GetActiveLicenseIDByPersonID(_PersonID);
            clsPatientTest _Patient = new clsPatientTest();
            _Patient.Result = Result;
            _Patient.ID = _LicenseID;
            int ID = _Patient.ID;
            // _Patient.PatientID = 12;
            //DataGridViewRow row = dgvPatientTestList.CurrentRow;
            // _Patient.TestListID = (int)row.Cells["TestListID"].Value;
            MessageBox.Show(dgvTests.Rows[0].Cells[5].Value + "" + dgvTests.Rows[0].Cells[1].Value+_LicenseID.ToString());
            //int rowsAffected = 0;
            foreach (DataGridViewRow row in dgvTests.Rows)
            {
                bool Select = Convert.ToBoolean(row.Cells["Select"].Value);

                if (Select)
                {
                    SqlConnection connection = new SqlConnection(ConnectionString);

                    string query = @"Update  PatientTests  
                            set 
                                Result=@Result
                                
                                where ID = @ID and TestListID=@TestListID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Result", row.Cells["Result"].Value);
                    command.Parameters.AddWithValue("@TestListID", row.Cells["TestListID"].Value);
                    command.Parameters.AddWithValue("ID", ID);
                    //  command.Parameters.AddWithValue("@PatientID", PatientID);

                    try
                    {
                        connection.Open();
                        // rowsAffected= command.ExecuteNonQuery();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        // return false;
                    }

                    finally
                    {
                        connection.Close();
                    }
                }
            }
            MessageBox.Show("good job");

        }
        public static void OpenFormInMain(Form form)
        {
            ((frmMain)Application.OpenForms["frmMain"]).OpenForm(form);
        }
        public static void OpenFormInMainWithClose(Form form)
        {
            if (Application.OpenForms[form.Name] != null)
                Application.OpenForms[form.Name].Close();
            ((frmMain)Application.OpenForms["frmMain"]).OpenForm(form);
        }
        public  void RunReportTest(DataTable table = null)
        {
            try
            {
                if (table == null)
                {

                    table = clsPatientTest.GetAllTests(_LocalDrivingLicenseApplicationID);
                }
                ReportDataSource rds = new ReportDataSource("DataSet1", table);
                frmPatientResult f = new frmPatientResult();
                f.rptPatientResult.LocalReport.DataSources.Clear();
                f.rptPatientResult.LocalReport.DataSources.Add(rds);
                f.rptPatientResult.LocalReport.ReportPath = "D:\\LAB\\Test Project\\DVLD\\PatientResultReport.rdlc";
                f.rptPatientResult.LocalReport.Refresh();
                // OpenFormInMainWithClose(f);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnResult_Click(object sender, EventArgs e)
        {
            //frmPatientResult frm = new frmPatientResult();
            //frm.Show();
            RunReportTest();
        }
    }
}