using DVLD.Applications;
using DVLD.Applications.Detain_License;
using DVLD.Applications.International_License;
using DVLD.Applications.ReplaceLostOrDamagedLicense;
using DVLD.Applications.Rlease_Detained_License;
using DVLD.Classes;
using DVLD.Patients;
using DVLD.Employee;
using DVLD.Licenses;
using DVLD.Licenses.International_License;
using DVLD.Login;
using DVLD.People;
using DVLD.Tests;
using DVLD.User;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace DVLD
{

    public partial class frmMain : Form
    {
        frmLogin _frmLogin;

        public frmMain( frmLogin frm )
        {
            InitializeComponent();
            _frmLogin= frm;
           // this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        public void OpenForm(Form form)
        {
            //foreach (Form frm in Application.OpenForms)
            //{
            //    if (frm.Name == "frmLogin" || frm.Name == "frmMain") continue;
            //    if (form.Name == frm.Name)
            //    {
            //        frm.Activate();
            //        return;
            //    }
            //}
            //form.BackColor = Color.FromArgb(Variables.G, Variables.R, Variables.B);
           // Size s = form.Size;
            form.Icon = this.Icon;
            //form.Font = new Font(form.Font.Name, Variables.FontSize, form.Font.Style);
            //form.Size = s;
            form.MdiParent = this;
            //form.WindowState = Variables.FormCurrentState;
            form.Show();
        }
        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            //frm.ShowDialog();
            OpenForm(new frmAddUpdateApplication());
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form frm = new frmListPeople();
            //frm.Show();
            OpenForm(new frmListPeople());

        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form frm = new frmListUsers();
            //frm.ShowDialog();
            OpenForm(new frmListUsers());

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            lblLoggedInUser.Text = "LoggedIn User: " + clsGlobal.CurrentUser.UserName;
            this.Refresh();

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            //frm.ShowDialog();
            OpenForm(new frmUserInfo(clsGlobal.CurrentUser.UserID));

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            //frm.ShowDialog();
            OpenForm(new frmChangePassword(clsGlobal.CurrentUser.UserID));
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmListApplicationTypes frm = new frmListApplicationTypes();
            //frm.ShowDialog();
            OpenForm(new frmListApplicationTypes());
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmListTestTypes frm = new frmListTestTypes();
            //frm.ShowDialog();
            OpenForm(new frmListTestTypes());
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            //frm.ShowDialog();
            OpenForm(new frmNewInternationalLicenseApplication());
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            //frm.ShowDialog();
            OpenForm(new frmRenewLocalDrivingLicenseApplication());
        }

       

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            //frm.ShowDialog();
            OpenForm(new frmReleaseDetainedLicenseApplication());
        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            //frm.ShowDialog();
            OpenForm(new frmApplicationsList());
        }

      
        private void vehiclesLicensesServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            //frm.ShowDialog();
            OpenForm(new frmApplicationsList());
        }

        private void PatientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmListPatients frm = new frmListPatients();
            //frm.ShowDialog();
            OpenForm(new frmListPatients());

        }



        private void ManageInternationaDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //frmListInternationalLicesnseApplications frm = new frmListInternationalLicesnseApplications();
            //frm.ShowDialog();
            OpenForm(new frmListInternationalLicesnseApplications());
        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            //frm.ShowDialog();
            OpenForm(new frmReplaceLostOrDamagedLicenseApplication());
        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //frmListDetainedLicenses frm = new frmListDetainedLicenses();
            //frm.ShowDialog();
            OpenForm(new frmListDetainedLicenses());
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            // frm.ShowDialog();
            OpenForm(new frmDetainLicenseApplication());
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmReleaseDetainedLicenseApplication frm= new frmReleaseDetainedLicenseApplication();   
            //frm.ShowDialog();
            OpenForm(new frmReleaseDetainedLicenseApplication());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void servicesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnDelegateForm_Click(object sender, EventArgs e)
        {
            DelegateForm1 frm = new DelegateForm1();
            frm.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void EmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmEmployee());
        }
    }
}
