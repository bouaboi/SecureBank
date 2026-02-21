namespace SecureBank.Forms
{
    partial class frmShowAndEdit
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
            this.button1 = new System.Windows.Forms.Button();
            this.ucShowAndEdit1 = new SecureBank.Forms.ucShowAndEdit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 445);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucShowAndEdit1
            // 
            this.ucShowAndEdit1.BackColor = System.Drawing.Color.YellowGreen;
            this.ucShowAndEdit1.Location = new System.Drawing.Point(12, 22);
            this.ucShowAndEdit1.Name = "ucShowAndEdit1";
            this.ucShowAndEdit1.Size = new System.Drawing.Size(354, 408);
            this.ucShowAndEdit1.TabIndex = 0;
            // 
            // frmShowAndEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(382, 480);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucShowAndEdit1);
            this.Name = "frmShowAndEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmClientInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucShowAndEdit ucShowAndEdit1;
        private System.Windows.Forms.Button button1;
    }
}