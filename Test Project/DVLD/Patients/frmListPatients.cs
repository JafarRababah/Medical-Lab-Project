using DVLD.Licenses.International_License;
using DVLD.People;
using DVLD.Tests;
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

namespace DVLD.Patients
{
    public partial class frmListPatients : Form
    {
        private DataTable _dtAllPatients;
        
        
        public frmListPatients()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frmListPatients_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dtAllPatients = clsPatient.GetAllPatients();
            dgvPatients.DataSource = _dtAllPatients;
            lblRecordsCount.Text = dgvPatients.Rows.Count.ToString();
            if (dgvPatients.Rows.Count>0)
            {
                dgvPatients.Columns[0].HeaderText = "Patient ID";
                dgvPatients.Columns[0].Width = 120;

                dgvPatients.Columns[1].HeaderText = "Person ID";
                dgvPatients.Columns[1].Width = 120;

                dgvPatients.Columns[2].HeaderText = "National No.";
                dgvPatients.Columns[2].Width = 140;

                dgvPatients.Columns[3].HeaderText = "Full Name";
                dgvPatients.Columns[3].Width = 320;

                dgvPatients.Columns[4].HeaderText = "Date";
                dgvPatients.Columns[4].Width = 170;

                dgvPatients.Columns[5].HeaderText = "Active Licenses";
                dgvPatients.Columns[5].Width = 150;
            }
          


        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
         

            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Enabled = false;
            }
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Patient ID":
                    FilterColumn = "PatientID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllPatients.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPatients.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                //in this case we deal with numbers not string.
                _dtAllPatients.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllPatients.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAllPatients.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "Patient ID" || cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPatients.CurrentRow.Cells[1].Value;
            //frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frmPatientTest frm = new frmPatientTest(PersonID);
            frm.ShowDialog();
            //refresh
            frmListPatients_Load(null, null);

        }

        private void issueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.");
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPatients.CurrentRow.Cells[1].Value;

          
            frmShowPersonAppHistory frm = new frmShowPersonAppHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
