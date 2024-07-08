namespace DVLD
{
    partial class DelegateForm1
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
            this.lblDataBack = new System.Windows.Forms.Label();
            this.btnOpenForm2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDataBack
            // 
            this.lblDataBack.AutoSize = true;
            this.lblDataBack.Location = new System.Drawing.Point(107, 102);
            this.lblDataBack.Name = "lblDataBack";
            this.lblDataBack.Size = new System.Drawing.Size(35, 13);
            this.lblDataBack.TabIndex = 0;
            this.lblDataBack.Text = "label1";
            // 
            // btnOpenForm2
            // 
            this.btnOpenForm2.Location = new System.Drawing.Point(209, 92);
            this.btnOpenForm2.Name = "btnOpenForm2";
            this.btnOpenForm2.Size = new System.Drawing.Size(75, 23);
            this.btnOpenForm2.TabIndex = 1;
            this.btnOpenForm2.Text = "Open Form2";
            this.btnOpenForm2.UseVisualStyleBackColor = true;
            this.btnOpenForm2.Click += new System.EventHandler(this.btnOpenForm2_Click);
            // 
            // DelegateForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOpenForm2);
            this.Controls.Add(this.lblDataBack);
            this.Name = "DelegateForm1";
            this.Text = "DelegateForm1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDataBack;
        private System.Windows.Forms.Button btnOpenForm2;
    }
}