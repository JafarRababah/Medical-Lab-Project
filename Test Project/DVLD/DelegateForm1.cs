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
    public partial class DelegateForm1 : Form
    {
        public DelegateForm1()
        {
            InitializeComponent();
        }

        private void btnOpenForm2_Click(object sender, EventArgs e)
        {
            DelegateForm2 frm=new DelegateForm2();
            frm.backdata1 += Form2_BackData;
            frm.Show();
        }
        private void Form2_BackData(object sender,int personID)
        {
            lblDataBack.Text = personID.ToString();
        }
    }
}
