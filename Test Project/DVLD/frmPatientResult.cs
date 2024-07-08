using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmPatientResult : Form
    {
        public frmPatientResult()
        {
            InitializeComponent();
        }

        private void frmPatientResult_Load(object sender, EventArgs e)
        {

            this.rptPatientResult.RefreshReport();
        }
    }
}
