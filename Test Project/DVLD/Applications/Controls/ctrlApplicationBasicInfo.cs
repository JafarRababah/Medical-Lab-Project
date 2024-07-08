using DVLD.Classes;
using DVLD.People;
using DVLD.Properties;
using DVLD.Tests;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Controls.ApplicationControls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {

        private clsApplication _Application;
        private int _PersonID = -1;
        private int _ApplicationID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private DataTable _dtAllTests;
        public static string ConnectionString = "Server=.;Database=DVLD; Integrated Security=True;";
        SqlConnection connection = new SqlConnection(ConnectionString);
        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
       
        public int ApplicationID
        {
            get { return _ApplicationID; }
        }
       
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
  
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application = clsApplication.FindBaseApplication(ApplicationID);
            _ApplicationID = ApplicationID;
            _PersonID = _Application.ApplicantPersonID;
            _LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID).LocalDrivingLicenseApplicationID;
            if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                cbFilterBy.SelectedIndex = 2;
                _FillApplicationInfo();
                     
        }

        private void _FillApplicationInfo()
        {
            this.dgvTests.Columns.Clear();
            _dtAllTests = clsTestList.GetAllTestLists();
            dgvTests.DataSource = _dtAllTests;

            try
            {
                if (dgvTests.Rows.Count != 0)
                {
                    dgvTests.Columns.Insert(0, chk);
                    chk.Name = "Select";
                    chk.HeaderText = "Select";
                    dgvTests.Columns[1].ReadOnly = true;
                    dgvTests.Columns[2].ReadOnly = true;
                    dgvTests.Columns[3].ReadOnly = true;
                    dgvTests.Columns[4].ReadOnly = true;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            _ApplicationID = _Application.ApplicationID;
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.StatusText;
            lblType.Text = _Application.ApplicationTypeInfo.Title;
            lblFees.Text = _Application.PaidFees.ToString();
            lblApplicant.Text = _Application.ApplicantFullName;
            lblDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            lblCreatedByUser.Text = _Application.CreatedByUserInfo.UserName;
            lblDoctor.Text = _Application.Doctor;
        }

        public void ResetApplicationInfo()
        {
            _ApplicationID = -1;

            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblType.Text = "[????]";
            lblFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
            lblDoctor.Text = "[????]";
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


            // frmShowPersonInfo frm = new frmShowPersonInfo(_Application.ApplicantPersonID);
            frmPatientTest frm = new frmPatientTest(_Application.ApplicantPersonID,_LocalDrivingLicenseApplicationID);
            frm.ShowDialog();

            //Refresh
            LoadApplicationInfo(_ApplicationID);

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

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            dgvTests.AllowUserToAddRows = false;
           // _patientTest.ID = clsLicense.GetActiveLicenseIDByPersonID(ctrlPersonCard1.PersonID);
            int ID = _LocalDrivingLicenseApplicationID;
            int PatientID = clsPatient.FindByPersonID(_PersonID).PatientID;

            foreach (DataGridViewRow row in dgvTests.Rows)
            {
                bool Select = Convert.ToBoolean(row.Cells["Select"].Value);

                if (Select)
                {

                   
                    string query = @"Insert Into PatientTests (ID,TestListID, PatientID)
                            Values (@ID,@TestListID,@PatientID)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PatientID", PatientID);
                    command.Parameters.AddWithValue("@TestListID", row.Cells["TestListID"].Value);
                    command.Parameters.AddWithValue("ID", ID);
                    //  command.Parameters.AddWithValue("@PatientID", PatientID);
                   // dgvTests.Rows.RemoveAt (Convert.ToInt32(Select));
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

        private void dgvTests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTests.Rows.Count >= 0)
            {
                this.dgvTests.Rows[e.RowIndex].Cells["Select"].Value = true;
            }
        }

        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
