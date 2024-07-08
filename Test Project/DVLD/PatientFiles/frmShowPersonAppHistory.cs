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

namespace DVLD.Licenses.International_License
{
    public partial class frmShowPersonAppHistory : Form
    {
        private int _PersonID = -1;

        public frmShowPersonAppHistory()
        {
            InitializeComponent();
           

        }

        public frmShowPersonAppHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;   
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {

            if (_PersonID!= -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                ctrlPatientLicenses1.LoadInfoByPersonID(_PersonID);
            } 
                else
            {
                ctrlPersonCardWithFilter1.Enabled= true;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            
           
            
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID= obj;
            if (_PersonID==-1)
            {
                ctrlPatientLicenses1.Clear();
            }
            else
            ctrlPatientLicenses1.LoadInfoByPersonID(_PersonID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPatientLicenses1_Load(object sender, EventArgs e)
        {

        }
    }
}
