using DVLD.Classes;
using DVLD.People;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications
{


    public partial class frmAddUpdateApplication: Form
    {

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _SelectedPersonID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmAddUpdateApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

        }

        private void _FillDoctorInComoboBox()
        {
            DataTable dtDoctors = clsApplication.GetAllDoctors();

            foreach (DataRow row in dtDoctors.Rows)
            {
                cbDoctors.Items.Add(row["FirstName"]);
            }
        }
        //private void _FillLicenseClassesInComoboBox()
        //{
        //    DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

        //    foreach (DataRow row in dtLicenseClasses.Rows)
        //    {
        //        cbLicenseClass.Items.Add(row["ClassName"]);
        //    }
        //}
        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillDoctorInComoboBox();


            if (_Mode == enMode.AddNew)
            {
                
                lblTitle.Text = "New Application";
                this.Text = "New Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                ctrlPersonCardWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;
              
                //cbDoctors.SelectedIndex = 0;
                lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();  
                lblApplicationDate.Text= DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblTitle.Text = "Update Application";
                this.Text = "Update Application";
           
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
             

            }

        }

        private void _LoadData()
        {

            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort( _LocalDrivingLicenseApplication.ApplicationDate);
           // cbDoctors.SelectedIndex = cbDoctors.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
           // cbDoctors.SelectedIndex = cbDoctors.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblFees.Text= _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUser.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;

        }

        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received
            _SelectedPersonID=PersonID;
            ctrlPersonCardWithFilter1.LoadPersonInfo(PersonID);
           

        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode==enMode.Update)
            {
                _LoadData();
            }
           
        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {

            if (clsApplication.DoesPersonHaveActiveApplication(ctrlPersonCardWithFilter1.PersonID, 1))
            {
                MessageBox.Show("Person already have an application", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                
                return;
            }


            //incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
        
                    btnSave.Enabled = true;
                    tpApplicationInfo.Enabled = true;
                lblPersonID.Text  = ctrlPersonCardWithFilter1.PersonID.ToString();
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
   
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           // _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

           // int LicenseClassID = clsLicenseClass.Find(cbDoctors.Text).LicenseClassID;


           // int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            //if (ActiveApplicationID != -1)
            //{
            //    MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cbDoctors.Focus();
            //    return;
            //}


            //check if user already have issued license of the same driving  class.
           if( clsLicense.IsLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID))
           {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsApplication.DoesPersonHaveActiveApplication(ctrlPersonCardWithFilter1.PersonID,1))
            {
                MessageBox.Show("Person already have an application", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            //MessageBox.Show(_LocalDrivingLicenseApplication.ApplicantPersonID.ToString());
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
            _LocalDrivingLicenseApplication.Doctor = cbDoctors.SelectedItem.ToString();
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            int PatientID = _LocalDrivingLicenseApplication.GetPatientID();
            _LocalDrivingLicenseApplication.PatientID = PatientID;
            // _LocalDrivingLicenseApplication.LicenseClassID= LicenseClassID;
            //  MessageBox.Show(_LocalDrivingLicenseApplication.Doctor);
           // MessageBox.Show(_LocalDrivingLicenseApplication.Test(_LocalDrivingLicenseApplication.PatientID).ToString());
           
             if (_LocalDrivingLicenseApplication.Save())
           
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Application";
                lblPatient.Text=PatientID.ToString();
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled=false;
                tpPersonalInfo.Enabled=false;
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            

        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;

        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
