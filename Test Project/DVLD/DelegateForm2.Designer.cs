namespace DVLD
{
    partial class DelegateForm2
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
            this.txtBackData = new System.Windows.Forms.TextBox();
            this.btnSendData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBackData
            // 
            this.txtBackData.Location = new System.Drawing.Point(165, 78);
            this.txtBackData.Name = "txtBackData";
            this.txtBackData.Size = new System.Drawing.Size(153, 20);
            this.txtBackData.TabIndex = 0;
            // 
            // btnSendData
            // 
            this.btnSendData.Location = new System.Drawing.Point(363, 74);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(109, 23);
            this.btnSendData.TabIndex = 1;
            this.btnSendData.Text = "Send Back Data";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // DelegateForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSendData);
            this.Controls.Add(this.txtBackData);
            this.Name = "DelegateForm2";
            this.Text = "DelegateForm";
            this.Load += new System.EventHandler(this.DelegateForm2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBackData;
        private System.Windows.Forms.Button btnSendData;
    }
}