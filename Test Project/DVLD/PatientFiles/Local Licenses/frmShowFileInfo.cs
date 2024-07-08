using DVLD.Controls;
using DVLD.Licenses.Local_Licenses.Controls;
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

namespace DVLD.PatientLicense
{
    public partial class frmShowFileInfo : Form
    {
        private int _LicenseID;

        public static string ConnectionString = "Server=.;Database=DVLD; Integrated Security=True;";
        DataTable _dtPatientList = new DataTable();

       // DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
        public frmShowFileInfo()
        {
            InitializeComponent();


        }
        public frmShowFileInfo(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void DataLoad(int LicenseID)
        {
            _dtPatientList = clsPatientTest.GetAllTests(LicenseID);
            dgvPatientTestList.DataSource = _dtPatientList;
            dgvPatientTestList.AllowUserToAddRows = false;
            if (dgvPatientTestList.Rows.Count != 0)
            {
                dgvPatientTestList.Columns[3].Visible = false;
                dgvPatientTestList.Columns[0].Visible = false;
                dgvPatientTestList.Columns[1].ReadOnly = true;
                dgvPatientTestList.Columns[2].ReadOnly = true;
                dgvPatientTestList.Columns[3].ReadOnly = true;
                dgvPatientTestList.Columns[4].ReadOnly = true;
            }
            //chk.Name = ("Select");
            //chk.HeaderText = "Select";

            //dgvPatientTestList.Columns.Insert(0, chk);

            int sum = 0;
            for (int i = 0; i <= dgvPatientTestList.Rows.Count - 1; i++)
            {
                sum = sum + int.Parse(dgvPatientTestList.Rows[i].Cells[2].Value.ToString());
            }
            //txtTotalAmount.Text = (from DataGridViewRow row in dgvPatientTestList.Rows
            //                       where row.Cells[0].FormattedValue.ToString() != String.Empty
            //                       select Convert.ToInt32(row.Cells[0].FormattedValue)).Sum().ToString();
            txtTotalAmount.Text = sum.ToString();
        }
        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {

            ctrlPatientLicenseInfo1.LoadInfo(_LicenseID);
            DataLoad(_LicenseID);


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            string Result = dgvPatientTestList.CurrentRow.Cells[5].Value.ToString();

            clsPatientTest _Patient = new clsPatientTest();
            _Patient.Result = Result;
            _Patient.ID = _LicenseID;
            // _Patient.PatientID = 12;
            DataGridViewRow row = dgvPatientTestList.CurrentRow;
            _Patient.TestListID = (int)row.Cells["TestListID"].Value;

            //  MessageBox.Show(_Patient.Result + _Patient.ID + _Patient.PatientID + _Patient.TestListID);
            if (_Patient.Update())
            {


                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);





        }

        private void dgvPatientTestList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            string Result = dgvPatientTestList.CurrentRow.Cells[5].Value.ToString();

            clsPatientTest _Patient = new clsPatientTest();
            _Patient.Result = Result;
            _Patient.ID = _LicenseID;
            int ID=_Patient.ID;
            // _Patient.PatientID = 12;
            //DataGridViewRow row = dgvPatientTestList.CurrentRow;
            // _Patient.TestListID = (int)row.Cells["TestListID"].Value;
            //MessageBox.Show(dgvPatientTestList.Rows[0].Cells[6].Value + "" + dgvPatientTestList.Rows[0].Cells[1].Value);
            //int rowsAffected = 0;
            for (int i = 0; i < dgvPatientTestList.Rows.Count; i++)
            {
                SqlConnection connection = new SqlConnection(ConnectionString);

                string query = @"Update  PatientTests  
                            set 
                                Result=@Result
                                
                                where ID = @ID and TestListID=@TestListID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Result", dgvPatientTestList.Rows[i].Cells[5].Value);
                command.Parameters.AddWithValue("@TestListID", dgvPatientTestList.Rows[i].Cells[0].Value);
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
    }
}
