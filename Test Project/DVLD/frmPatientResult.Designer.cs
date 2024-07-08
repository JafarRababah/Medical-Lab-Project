namespace DVLD
{
    partial class frmPatientResult
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
       
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rptPatientResult = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rptPatientResult
            // 
            this.rptPatientResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptPatientResult.LocalReport.ReportEmbeddedResource = "DVLD.PatientResultReport.rdlc";
            this.rptPatientResult.Location = new System.Drawing.Point(0, 0);
            this.rptPatientResult.Name = "rptPatientResult";
            this.rptPatientResult.ServerReport.BearerToken = null;
            this.rptPatientResult.Size = new System.Drawing.Size(800, 450);
            this.rptPatientResult.TabIndex = 1;
            // 
            // frmPatientResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rptPatientResult);
            this.Name = "frmPatientResult";
            this.Text = "Patient Result";
            this.Load += new System.EventHandler(this.frmPatientResult_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public Microsoft.Reporting.WinForms.ReportViewer rptPatientResult;
    }
}