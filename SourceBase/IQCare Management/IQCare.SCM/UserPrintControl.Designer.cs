namespace IQCare.SCM
{
    partial class UserPrintControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserPrintControl));
            this.lblexpire1 = new System.Windows.Forms.Label();
            this.pnlprint = new System.Windows.Forms.Panel();
            this.pctLogo = new System.Windows.Forms.PictureBox();
            this.lblStore = new System.Windows.Forms.Label();
            this.lblfacility = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.txtinstruction = new System.Windows.Forms.TextBox();
            this.lbldrgquantity = new System.Windows.Forms.Label();
            this.chkDrugName = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbnocopies = new System.Windows.Forms.ComboBox();
            this.lblnoofcopies = new System.Windows.Forms.Label();
            this.pnlprint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblexpire1
            // 
            this.lblexpire1.AutoSize = true;
            this.lblexpire1.Location = new System.Drawing.Point(110, 68);
            this.lblexpire1.Name = "lblexpire1";
            this.lblexpire1.Size = new System.Drawing.Size(0, 16);
            this.lblexpire1.TabIndex = 7;
            // 
            // pnlprint
            // 
            this.pnlprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlprint.Controls.Add(this.pctLogo);
            this.pnlprint.Controls.Add(this.lblStore);
            this.pnlprint.Controls.Add(this.lblfacility);
            this.pnlprint.Controls.Add(this.lblPatientName);
            this.pnlprint.Controls.Add(this.txtinstruction);
            this.pnlprint.Controls.Add(this.lbldrgquantity);
            this.pnlprint.Controls.Add(this.chkDrugName);
            this.pnlprint.Controls.Add(this.label1);
            this.pnlprint.Location = new System.Drawing.Point(-1, -1);
            this.pnlprint.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlprint.Name = "pnlprint";
            this.pnlprint.Size = new System.Drawing.Size(297, 148);
            this.pnlprint.TabIndex = 12;
            // 
            // pctLogo
            // 
            this.pctLogo.AccessibleRole = System.Windows.Forms.AccessibleRole.Cell;
            this.pctLogo.Image = ((System.Drawing.Image)(resources.GetObject("pctLogo.Image")));
            this.pctLogo.Location = new System.Drawing.Point(240, 79);
            this.pctLogo.Name = "pctLogo";
            this.pctLogo.Size = new System.Drawing.Size(49, 52);
            this.pctLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctLogo.TabIndex = 17;
            this.pctLogo.TabStop = false;
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStore.Location = new System.Drawing.Point(1, 96);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(59, 16);
            this.lblStore.TabIndex = 16;
            this.lblStore.Text = "StoreName";
            // 
            // lblfacility
            // 
            this.lblfacility.AutoSize = true;
            this.lblfacility.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfacility.Location = new System.Drawing.Point(2, 113);
            this.lblfacility.Name = "lblfacility";
            this.lblfacility.Size = new System.Drawing.Size(64, 16);
            this.lblfacility.TabIndex = 15;
            this.lblfacility.Text = "facilityName";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.Location = new System.Drawing.Point(2, 79);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(33, 16);
            this.lblPatientName.TabIndex = 14;
            this.lblPatientName.Tag = "";
            this.lblPatientName.Text = "name";
            // 
            // txtinstruction
            // 
            this.txtinstruction.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtinstruction.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtinstruction.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtinstruction.Location = new System.Drawing.Point(-1, 58);
            this.txtinstruction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtinstruction.Name = "txtinstruction";
            this.txtinstruction.Size = new System.Drawing.Size(295, 20);
            this.txtinstruction.TabIndex = 13;
            // 
            // lbldrgquantity
            // 
            this.lbldrgquantity.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldrgquantity.Location = new System.Drawing.Point(221, 4);
            this.lbldrgquantity.Name = "lbldrgquantity";
            this.lbldrgquantity.Size = new System.Drawing.Size(74, 35);
            this.lbldrgquantity.TabIndex = 12;
            this.lbldrgquantity.Text = "Drug Quantity";
            // 
            // chkDrugName
            // 
            this.chkDrugName.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDrugName.Location = new System.Drawing.Point(1, 1);
            this.chkDrugName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkDrugName.Name = "chkDrugName";
            this.chkDrugName.Size = new System.Drawing.Size(213, 57);
            this.chkDrugName.TabIndex = 11;
            this.chkDrugName.Text = "chkDrugName";
            this.chkDrugName.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Keep medicines in a cool dry place out of the reach of children";
            // 
            // cmbnocopies
            // 
            this.cmbnocopies.DisplayMember = "1";
            this.cmbnocopies.FormattingEnabled = true;
            this.cmbnocopies.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbnocopies.Location = new System.Drawing.Point(99, 162);
            this.cmbnocopies.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbnocopies.Name = "cmbnocopies";
            this.cmbnocopies.Size = new System.Drawing.Size(101, 24);
            this.cmbnocopies.TabIndex = 14;
            this.cmbnocopies.ValueMember = "1";
            // 
            // lblnoofcopies
            // 
            this.lblnoofcopies.AutoSize = true;
            this.lblnoofcopies.Location = new System.Drawing.Point(8, 163);
            this.lblnoofcopies.Name = "lblnoofcopies";
            this.lblnoofcopies.Size = new System.Drawing.Size(85, 16);
            this.lblnoofcopies.TabIndex = 13;
            this.lblnoofcopies.Text = "Number of copies:";
            // 
            // UserPrintControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbnocopies);
            this.Controls.Add(this.lblnoofcopies);
            this.Controls.Add(this.pnlprint);
            this.Controls.Add(this.lblexpire1);
            this.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserPrintControl";
            this.Size = new System.Drawing.Size(297, 191);
            this.Tag = "frmForm";
            this.Load += new System.EventHandler(this.UserPrintControl_Load);
            this.pnlprint.ResumeLayout(false);
            this.pnlprint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblexpire1;
        private System.Windows.Forms.Panel pnlprint;
        private System.Windows.Forms.Label lblfacility;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.TextBox txtinstruction;
        private System.Windows.Forms.Label lbldrgquantity;
        private System.Windows.Forms.CheckBox chkDrugName;
        private System.Windows.Forms.ComboBox cmbnocopies;
        private System.Windows.Forms.Label lblnoofcopies;
        private System.Windows.Forms.Label lblStore;
        private System.Windows.Forms.PictureBox pctLogo;
        private System.Windows.Forms.Label label1;
    }
}
