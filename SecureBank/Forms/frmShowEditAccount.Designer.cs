namespace SecureBank.Forms
{
    partial class frmShowEditAccount
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
            this.ucShowAccAndEdit1 = new SecureBank.Forms.ucShowAccAndEdit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(267, 422);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucShowAccAndEdit1
            // 
            this.ucShowAccAndEdit1.BackColor = System.Drawing.Color.YellowGreen;
            this.ucShowAccAndEdit1.Location = new System.Drawing.Point(14, 12);
            this.ucShowAccAndEdit1.Name = "ucShowAccAndEdit1";
            this.ucShowAccAndEdit1.Size = new System.Drawing.Size(328, 404);
            this.ucShowAccAndEdit1.TabIndex = 0;
            // 
            // frmShowEditAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(354, 453);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucShowAccAndEdit1);
            this.Name = "frmShowEditAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmShowEditAccount";
            this.Load += new System.EventHandler(this.frmShowEditAccount_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucShowAccAndEdit ucShowAccAndEdit1;
        private System.Windows.Forms.Button button1;
    }
}