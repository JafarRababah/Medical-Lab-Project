using DVLD.Controls;
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
using DVLD;
using DVLD.Licenses.International_Licenses;
using DVLD.Applications;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlPatientFile : UserControl
    {
        private int _PatientID;
        private clsPatient _Patient ;
       // private DataTable _dtPatientLocalLicensesHistory;
        private DataTable _dtPatientAppHistory;
        private DataTable _dtPatientInternationalLicensesHistory;

        public ctrlPatientFile()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenseInfo()
        {

            //_dtPatientLocalLicensesHistory = clsPatient.GetLicenses(_PatientID);
            _dtPatientAppHistory= clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplicationsByPatientID(_PatientID);

            //dgvLocalLicensesHistory.DataSource = _dtPatientLocalLicensesHistory;
            dgvLocalLicensesHistory.DataSource = _dtPatientAppHistory;
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Application ID";
                dgvLocalLicensesHistory.Columns[0].Width = 200;
                dgvLocalLicensesHistory.Columns[1].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[1].Width = 170;
                dgvLocalLicensesHistory.Columns[2].HeaderText = "User ID";
                dgvLocalLicensesHistory.Columns[2].Width = 110;

                //dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                //dgvLocalLicensesHistory.Columns[2].Width = 270;

                

               /* dgvLocalLicensesHistory.Columns[3].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[4].Width = 110;*/

            }
        }

        private void _LoadInternationalLicenseInfo()
        {

            _dtPatientInternationalLicensesHistory = clsPatient.GetInternationalLicenses(_PatientID);


            dgvInternationalLicensesHistory.DataSource = _dtPatientInternationalLicensesHistory;
            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 130;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 180;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 180;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 120;

            }
        }

        public void LoadInfo(int PatientID)
        {
            _PatientID = PatientID;
            _Patient = clsPatient.FindByPatientID(_PatientID);

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }

        public void LoadInfoByPersonID(int PersonID)
        {
            
            _Patient = clsPatient.FindByPersonID(PersonID);
            if (_Patient != null)
            {
                _PatientID = clsPatient.FindByPersonID(PersonID).PatientID;
            }
           
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value;
            PatientLicense.frmShowFileInfo frm = new PatientLicense.frmShowFileInfo(LicenseID);
            frm.ShowDialog();
            
        }

        public void Clear()
        {
            //_dtPatientLocalLicensesHistory.Clear();
            _dtPatientAppHistory.Clear();
        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void dgvLocalLicensesHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvLocalLicensesHistory_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmApplicationInfo frm =
                        new frmApplicationInfo((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            //refresh
            //frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void dgvLocalLicensesHistory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmApplicationInfo frm =
                        new frmApplicationInfo((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            
            
        }
    }
}
