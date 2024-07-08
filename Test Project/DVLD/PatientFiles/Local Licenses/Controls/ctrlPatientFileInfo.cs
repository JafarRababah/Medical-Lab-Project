using DVLD.Classes;
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
using System.IO;
using DVLD.Controls;

namespace DVLD.PatientLicense
{
    public partial class ctrlPatientFileInfo : UserControl
    {
        private int _LicenseID;
        private int _PatientID;
        private clsLicense _License;
        private clsPatient _Patient;
        public ctrlPatientFileInfo()
        {
            InitializeComponent();
           
        }

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        { get { return _License; } }

        private void _LoadPersonImage()
        {
            if (_License.PatientInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.PatientInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblFileID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            //lblClass.Text = _License.LicenseClassIfo.ClassName;
            lblFullName.Text = _License.PatientInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.PatientInfo.PersonInfo.NationalNo;
            lblGendor.Text = _License.PatientInfo.PersonInfo.Gendor ==0 ? "Male":"Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.PatientInfo.PersonInfo.DateOfBirth);

            lblPatientID.Text= _License.PatientID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            //lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text= _License.Notes=="" ? "No Notes":_License.Notes;
            _LoadPersonImage();



        }
        public void LoadInfoByPatientID(int PatientID)
        {
            _PatientID = PatientID;
            _Patient = clsPatient.FindByPatientID(_PatientID);
            if (_License == null)
            {
                MessageBox.Show("Could not find Patient ID = " + _PatientID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _PatientID = -1;
                return;
            }

            lblFileID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassIfo.ClassName;
            lblFullName.Text = _License.PatientInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.PatientInfo.PersonInfo.NationalNo;
            lblGendor.Text = _License.PatientInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.PatientInfo.PersonInfo.DateOfBirth);

            lblPatientID.Text = _License.PatientID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            //lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadPersonImage();



        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
