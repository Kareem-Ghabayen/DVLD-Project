namespace DVLD.Applications
{
    partial class frmAddUpdateLocalDrivingLicenseApplication
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
            this.tcApplicationInfo = new Guna.UI2.WinForms.Guna2TabControl();
            this.tpPersonalInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new Guna.UI2.WinForms.Guna2Button();
            this.tpApplicationInfo = new System.Windows.Forms.TabPage();
            this.cbLicenseClasses = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lblCreatedByUser = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel7 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblFees = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblApplicationDate = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblLocalDrivingLicenseApplicationID = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel10 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblPassword = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTitle2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.ctrlPersonCardWithFilter1 = new DVLD.people.ctrlPersonCardWithFilter();
            this.tcApplicationInfo.SuspendLayout();
            this.tpPersonalInfo.SuspendLayout();
            this.tpApplicationInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // tcApplicationInfo
            // 
            this.tcApplicationInfo.Controls.Add(this.tpPersonalInfo);
            this.tcApplicationInfo.Controls.Add(this.tpApplicationInfo);
            this.tcApplicationInfo.ItemSize = new System.Drawing.Size(180, 40);
            this.tcApplicationInfo.Location = new System.Drawing.Point(2, 83);
            this.tcApplicationInfo.Name = "tcApplicationInfo";
            this.tcApplicationInfo.SelectedIndex = 0;
            this.tcApplicationInfo.Size = new System.Drawing.Size(707, 545);
            this.tcApplicationInfo.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tcApplicationInfo.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tcApplicationInfo.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcApplicationInfo.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tcApplicationInfo.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tcApplicationInfo.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tcApplicationInfo.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tcApplicationInfo.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcApplicationInfo.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.tcApplicationInfo.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tcApplicationInfo.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tcApplicationInfo.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tcApplicationInfo.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcApplicationInfo.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tcApplicationInfo.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tcApplicationInfo.TabButtonSize = new System.Drawing.Size(180, 40);
            this.tcApplicationInfo.TabIndex = 44;
            this.tcApplicationInfo.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tcApplicationInfo.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            // 
            // tpPersonalInfo
            // 
            this.tpPersonalInfo.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.tpPersonalInfo.Controls.Add(this.btnNext);
            this.tpPersonalInfo.Location = new System.Drawing.Point(4, 44);
            this.tpPersonalInfo.Name = "tpPersonalInfo";
            this.tpPersonalInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPersonalInfo.Size = new System.Drawing.Size(699, 497);
            this.tpPersonalInfo.TabIndex = 0;
            this.tpPersonalInfo.Text = "Personal Info";
            this.tpPersonalInfo.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.BorderRadius = 15;
            this.btnNext.BorderThickness = 1;
            this.btnNext.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNext.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNext.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNext.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNext.FillColor = System.Drawing.Color.Transparent;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Location = new System.Drawing.Point(571, 440);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(113, 37);
            this.btnNext.TabIndex = 43;
            this.btnNext.Text = "next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tpApplicationInfo
            // 
            this.tpApplicationInfo.Controls.Add(this.cbLicenseClasses);
            this.tpApplicationInfo.Controls.Add(this.pictureBox4);
            this.tpApplicationInfo.Controls.Add(this.lblCreatedByUser);
            this.tpApplicationInfo.Controls.Add(this.guna2HtmlLabel7);
            this.tpApplicationInfo.Controls.Add(this.lblFees);
            this.tpApplicationInfo.Controls.Add(this.lblApplicationDate);
            this.tpApplicationInfo.Controls.Add(this.lblLocalDrivingLicenseApplicationID);
            this.tpApplicationInfo.Controls.Add(this.guna2HtmlLabel2);
            this.tpApplicationInfo.Controls.Add(this.guna2HtmlLabel10);
            this.tpApplicationInfo.Controls.Add(this.guna2HtmlLabel1);
            this.tpApplicationInfo.Controls.Add(this.lblPassword);
            this.tpApplicationInfo.Controls.Add(this.pictureBox3);
            this.tpApplicationInfo.Controls.Add(this.pictureBox2);
            this.tpApplicationInfo.Controls.Add(this.pictureBox1);
            this.tpApplicationInfo.Controls.Add(this.pictureBox5);
            this.tpApplicationInfo.Location = new System.Drawing.Point(4, 44);
            this.tpApplicationInfo.Name = "tpApplicationInfo";
            this.tpApplicationInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpApplicationInfo.Size = new System.Drawing.Size(699, 497);
            this.tpApplicationInfo.TabIndex = 1;
            this.tpApplicationInfo.Text = "Application Info";
            this.tpApplicationInfo.UseVisualStyleBackColor = true;
            // 
            // cbLicenseClasses
            // 
            this.cbLicenseClasses.BackColor = System.Drawing.Color.Transparent;
            this.cbLicenseClasses.BorderRadius = 15;
            this.cbLicenseClasses.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLicenseClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseClasses.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLicenseClasses.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLicenseClasses.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLicenseClasses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbLicenseClasses.ItemHeight = 30;
            this.cbLicenseClasses.Location = new System.Drawing.Point(240, 111);
            this.cbLicenseClasses.Name = "cbLicenseClasses";
            this.cbLicenseClasses.Size = new System.Drawing.Size(314, 36);
            this.cbLicenseClasses.TabIndex = 84;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD.Properties.Resources.User_32__2;
            this.pictureBox4.Location = new System.Drawing.Point(185, 204);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(22, 24);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 83;
            this.pictureBox4.TabStop = false;
            // 
            // lblCreatedByUser
            // 
            this.lblCreatedByUser.AutoSize = false;
            this.lblCreatedByUser.BackColor = System.Drawing.Color.Transparent;
            this.lblCreatedByUser.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedByUser.ForeColor = System.Drawing.Color.Black;
            this.lblCreatedByUser.Location = new System.Drawing.Point(240, 204);
            this.lblCreatedByUser.Name = "lblCreatedByUser";
            this.lblCreatedByUser.Size = new System.Drawing.Size(140, 31);
            this.lblCreatedByUser.TabIndex = 82;
            this.lblCreatedByUser.Text = "[???]";
            // 
            // guna2HtmlLabel7
            // 
            this.guna2HtmlLabel7.AutoSize = false;
            this.guna2HtmlLabel7.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel7.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel7.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel7.Location = new System.Drawing.Point(68, 204);
            this.guna2HtmlLabel7.Name = "guna2HtmlLabel7";
            this.guna2HtmlLabel7.Size = new System.Drawing.Size(92, 31);
            this.guna2HtmlLabel7.TabIndex = 81;
            this.guna2HtmlLabel7.Text = "Created By";
            // 
            // lblFees
            // 
            this.lblFees.AutoSize = false;
            this.lblFees.BackColor = System.Drawing.Color.Transparent;
            this.lblFees.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFees.ForeColor = System.Drawing.Color.Black;
            this.lblFees.Location = new System.Drawing.Point(240, 165);
            this.lblFees.Name = "lblFees";
            this.lblFees.Size = new System.Drawing.Size(140, 33);
            this.lblFees.TabIndex = 80;
            this.lblFees.Text = "[???]";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = false;
            this.lblApplicationDate.BackColor = System.Drawing.Color.Transparent;
            this.lblApplicationDate.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationDate.ForeColor = System.Drawing.Color.Black;
            this.lblApplicationDate.Location = new System.Drawing.Point(240, 73);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(140, 32);
            this.lblApplicationDate.TabIndex = 78;
            this.lblApplicationDate.Text = "[???]";
            // 
            // lblLocalDrivingLicenseApplicationID
            // 
            this.lblLocalDrivingLicenseApplicationID.AutoSize = false;
            this.lblLocalDrivingLicenseApplicationID.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalDrivingLicenseApplicationID.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalDrivingLicenseApplicationID.ForeColor = System.Drawing.Color.Black;
            this.lblLocalDrivingLicenseApplicationID.Location = new System.Drawing.Point(240, 27);
            this.lblLocalDrivingLicenseApplicationID.Name = "lblLocalDrivingLicenseApplicationID";
            this.lblLocalDrivingLicenseApplicationID.Size = new System.Drawing.Size(140, 40);
            this.lblLocalDrivingLicenseApplicationID.TabIndex = 77;
            this.lblLocalDrivingLicenseApplicationID.Text = "[???]";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.AutoSize = false;
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(8, 27);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(152, 40);
            this.guna2HtmlLabel2.TabIndex = 70;
            this.guna2HtmlLabel2.Text = "D.L.Application ID";
            // 
            // guna2HtmlLabel10
            // 
            this.guna2HtmlLabel10.AutoSize = false;
            this.guna2HtmlLabel10.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel10.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel10.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel10.Location = new System.Drawing.Point(22, 73);
            this.guna2HtmlLabel10.Name = "guna2HtmlLabel10";
            this.guna2HtmlLabel10.Size = new System.Drawing.Size(138, 40);
            this.guna2HtmlLabel10.TabIndex = 69;
            this.guna2HtmlLabel10.Text = "Application Date";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(27, 165);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(152, 33);
            this.guna2HtmlLabel1.TabIndex = 65;
            this.guna2HtmlLabel1.Text = "Application Fees";
            this.guna2HtmlLabel1.Click += new System.EventHandler(this.guna2HtmlLabel1_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = false;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.Black;
            this.lblPassword.Location = new System.Drawing.Point(51, 119);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(109, 40);
            this.lblPassword.TabIndex = 61;
            this.lblPassword.Text = "License Class";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLD.Properties.Resources.Calendar_32;
            this.pictureBox3.Location = new System.Drawing.Point(185, 73);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(22, 24);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 72;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.Number_32;
            this.pictureBox2.Location = new System.Drawing.Point(185, 27);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 71;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.money_321;
            this.pictureBox1.Location = new System.Drawing.Point(185, 165);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 66;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLD.Properties.Resources.License_Type_32;
            this.pictureBox5.Location = new System.Drawing.Point(185, 119);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(22, 24);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 64;
            this.pictureBox5.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BorderRadius = 15;
            this.btnClose.BorderThickness = 1;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(577, 634);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(113, 37);
            this.btnClose.TabIndex = 46;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BorderRadius = 15;
            this.btnSave.BorderThickness = 1;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.Transparent;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(431, 634);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 37);
            this.btnSave.TabIndex = 45;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(322, -72);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 30);
            this.lblTitle.TabIndex = 43;
            this.lblTitle.Text = "Add New User";
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = false;
            this.lblTitle2.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle2.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.ForeColor = System.Drawing.Color.Red;
            this.lblTitle2.Location = new System.Drawing.Point(140, 36);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(485, 30);
            this.lblTitle2.TabIndex = 47;
            this.lblTitle2.Text = "New Local Driving License Application";
            this.lblTitle2.Click += new System.EventHandler(this.guna2HtmlLabel3_Click);
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.FilterEnabled = true;
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(3, 0);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(690, 421);
            this.ctrlPersonCardWithFilter1.TabIndex = 44;
            // 
            // frmAddUpdateLocalDrivingLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 683);
            this.Controls.Add(this.lblTitle2);
            this.Controls.Add(this.tcApplicationInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddUpdateLocalDrivingLicenseApplication";
            this.Text = "Add Update Local Driving License Application";
            this.Load += new System.EventHandler(this.frmAddUpdateLocalDrivingLicenseApplication_Load);
            this.tcApplicationInfo.ResumeLayout(false);
            this.tpPersonalInfo.ResumeLayout(false);
            this.tpApplicationInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TabControl tcApplicationInfo;
        private System.Windows.Forms.TabPage tpPersonalInfo;
        private people.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private Guna.UI2.WinForms.Guna2Button btnNext;
        private System.Windows.Forms.TabPage tpApplicationInfo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLocalDrivingLicenseApplicationID;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPassword;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCreatedByUser;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel7;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFees;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblApplicationDate;
        private System.Windows.Forms.PictureBox pictureBox4;
        private Guna.UI2.WinForms.Guna2ComboBox cbLicenseClasses;
    }
}