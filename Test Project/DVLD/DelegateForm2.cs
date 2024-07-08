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
    public partial class DelegateForm2 : Form
    {
        public delegate void BackData(object sender, int PersonID);
        public event BackData backdata1;
        public DelegateForm2()
        {
            InitializeComponent();
        }

        private void DelegateForm2_Load(object sender, EventArgs e)
        {

        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            int PersonID=int.Parse(txtBackData.Text);
            backdata1?.Invoke(this, PersonID);
            this.Close();
        }
    }
}
