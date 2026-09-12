namespace DVLD.Applications
{
    partial class ctrlDriverLicenses
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbDriverLicenses = new Guna.UI2.WinForms.Guna2GroupBox();
            this.tcDriverLicenses = new Guna.UI2.WinForms.Guna2TabControl();
            this.tpLocalLicenses = new System.Windows.Forms.TabPage();
            this.lblLocalLicensesRecords = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dgvLocalLicensesHistory = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblRecordsCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tpInternationalLicenses = new System.Windows.Forms.TabPage();
            this.lblInternationalLicensesRecords = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dgvInternationalLicensesHistory = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.gbDriverLicenses.SuspendLayout();
            this.tcDriverLicenses.SuspendLayout();
            this.tpLocalLicenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesHistory)).BeginInit();
            this.tpInternationalLicenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDriverLicenses
            // 
            this.gbDriverLicenses.Controls.Add(this.tcDriverLicenses);
            this.gbDriverLicenses.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbDriverLicenses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbDriverLicenses.Location = new System.Drawing.Point(3, 3);
            this.gbDriverLicenses.Name = "gbDriverLicenses";
            this.gbDriverLicenses.Size = new System.Drawing.Size(874, 339);
            this.gbDriverLicenses.TabIndex = 15;
            this.gbDriverLicenses.Text = "Driver Licenses";
            this.gbDriverLicenses.Click += new System.EventHandler(this.gbDriverLicenses_Click);
            // 
            // tcDriverLicenses
            // 
            this.tcDriverLicenses.Controls.Add(this.tpLocalLicenses);
            this.tcDriverLicenses.Controls.Add(this.tpInternationalLicenses);
            this.tcDriverLicenses.ItemSize = new System.Drawing.Size(180, 40);
            this.tcDriverLicenses.Location = new System.Drawing.Point(0, 40);
            this.tcDriverLicenses.Name = "tcDriverLicenses";
            this.tcDriverLicenses.SelectedIndex = 0;
            this.tcDriverLicenses.Size = new System.Drawing.Size(869, 299);
            this.tcDriverLicenses.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tcDriverLicenses.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tcDriverLicenses.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcDriverLicenses.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tcDriverLicenses.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tcDriverLicenses.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tcDriverLicenses.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tcDriverLicenses.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcDriverLicenses.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.tcDriverLicenses.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tcDriverLicenses.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tcDriverLicenses.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tcDriverLicenses.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcDriverLicenses.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tcDriverLicenses.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tcDriverLicenses.TabButtonSize = new System.Drawing.Size(180, 40);
            this.tcDriverLicenses.TabIndex = 0;
            this.tcDriverLicenses.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tcDriverLicenses.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            // 
            // tpLocalLicenses
            // 
            this.tpLocalLicenses.Controls.Add(this.lblLocalLicensesRecords);
            this.tpLocalLicenses.Controls.Add(this.dgvLocalLicensesHistory);
            this.tpLocalLicenses.Controls.Add(this.guna2HtmlLabel3);
            this.tpLocalLicenses.Controls.Add(this.lblRecordsCount);
            this.tpLocalLicenses.Location = new System.Drawing.Point(4, 44);
            this.tpLocalLicenses.Name = "tpLocalLicenses";
            this.tpLocalLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocalLicenses.Size = new System.Drawing.Size(861, 251);
            this.tpLocalLicenses.TabIndex = 0;
            this.tpLocalLicenses.Text = "Local";
            this.tpLocalLicenses.UseVisualStyleBackColor = true;
            // 
            // lblLocalLicensesRecords
            // 
            this.lblLocalLicensesRecords.AutoSize = false;
            this.lblLocalLicensesRecords.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalLicensesRecords.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalLicensesRecords.ForeColor = System.Drawing.Color.Black;
            this.lblLocalLicensesRecords.Location = new System.Drawing.Point(148, 200);
            this.lblLocalLicensesRecords.Name = "lblLocalLicensesRecords";
            this.lblLocalLicensesRecords.Size = new System.Drawing.Size(36, 23);
            this.lblLocalLicensesRecords.TabIndex = 22;
            this.lblLocalLicensesRecords.Text = "0";
            this.lblLocalLicensesRecords.Click += new System.EventHandler(this.lblLocalLicensesRecords_Click);
            // 
            // dgvLocalLicensesHistory
            // 
            this.dgvLocalLicensesHistory.AllowUserToAddRows = false;
            this.dgvLocalLicensesHistory.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvLocalLicensesHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLocalLicensesHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocalLicensesHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLocalLicensesHistory.ColumnHeadersHeight = 4;
            this.dgvLocalLicensesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLocalLicensesHistory.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLocalLicensesHistory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLocalLicensesHistory.Location = new System.Drawing.Point(6, 56);
            this.dgvLocalLicensesHistory.Name = "dgvLocalLicensesHistory";
            this.dgvLocalLicensesHistory.ReadOnly = true;
            this.dgvLocalLicensesHistory.RowHeadersVisible = false;
            this.dgvLocalLicensesHistory.RowHeadersWidth = 51;
            this.dgvLocalLicensesHistory.RowTemplate.Height = 26;
            this.dgvLocalLicensesHistory.Size = new System.Drawing.Size(849, 115);
            this.dgvLocalLicensesHistory.TabIndex = 72;
            this.dgvLocalLicensesHistory.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvLocalLicensesHistory.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvLocalLicensesHistory.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvLocalLicensesHistory.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvLocalLicensesHistory.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvLocalLicensesHistory.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvLocalLicensesHistory.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLocalLicensesHistory.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvLocalLicensesHistory.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvLocalLicensesHistory.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dgvLocalLicensesHistory.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvLocalLicensesHistory.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvLocalLicensesHistory.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvLocalLicensesHistory.ThemeStyle.ReadOnly = true;
            this.dgvLocalLicensesHistory.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvLocalLicensesHistory.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLocalLicensesHistory.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dgvLocalLicensesHistory.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvLocalLicensesHistory.ThemeStyle.RowsStyle.Height = 26;
            this.dgvLocalLicensesHistory.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLocalLicensesHistory.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.AutoSize = false;
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(6, 6);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(248, 36);
            this.guna2HtmlLabel3.TabIndex = 71;
            this.guna2HtmlLabel3.Text = "Local Licenses History:";
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = false;
            this.lblRecordsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRecordsCount.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.ForeColor = System.Drawing.Color.Black;
            this.lblRecordsCount.Location = new System.Drawing.Point(19, 200);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(109, 23);
            this.lblRecordsCount.TabIndex = 20;
            this.lblRecordsCount.Text = "# Records:";
            // 
            // tpInternationalLicenses
            // 
            this.tpInternationalLicenses.Controls.Add(this.lblInternationalLicensesRecords);
            this.tpInternationalLicenses.Controls.Add(this.dgvInternationalLicensesHistory);
            this.tpInternationalLicenses.Controls.Add(this.guna2HtmlLabel2);
            this.tpInternationalLicenses.Controls.Add(this.guna2HtmlLabel4);
            this.tpInternationalLicenses.Location = new System.Drawing.Point(4, 44);
            this.tpInternationalLicenses.Name = "tpInternationalLicenses";
            this.tpInternationalLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternationalLicenses.Size = new System.Drawing.Size(861, 251);
            this.tpInternationalLicenses.TabIndex = 1;
            this.tpInternationalLicenses.Text = "International";
            this.tpInternationalLicenses.UseVisualStyleBackColor = true;
            // 
            // lblInternationalLicensesRecords
            // 
            this.lblInternationalLicensesRecords.AutoSize = false;
            this.lblInternationalLicensesRecords.BackColor = System.Drawing.Color.Transparent;
            this.lblInternationalLicensesRecords.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternationalLicensesRecords.ForeColor = System.Drawing.Color.Black;
            this.lblInternationalLicensesRecords.Location = new System.Drawing.Point(178, 182);
            this.lblInternationalLicensesRecords.Name = "lblInternationalLicensesRecords";
            this.lblInternationalLicensesRecords.Size = new System.Drawing.Size(34, 23);
            this.lblInternationalLicensesRecords.TabIndex = 74;
            this.lblInternationalLicensesRecords.Text = "0";
            // 
            // dgvInternationalLicensesHistory
            // 
            this.dgvInternationalLicensesHistory.AllowUserToAddRows = false;
            this.dgvInternationalLicensesHistory.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvInternationalLicensesHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvInternationalLicensesHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInternationalLicensesHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvInternationalLicensesHistory.ColumnHeadersHeight = 4;
            this.dgvInternationalLicensesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInternationalLicensesHistory.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvInternationalLicensesHistory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvInternationalLicensesHistory.Location = new System.Drawing.Point(4, 36);
            this.dgvInternationalLicensesHistory.Name = "dgvInternationalLicensesHistory";
            this.dgvInternationalLicensesHistory.ReadOnly = true;
            this.dgvInternationalLicensesHistory.RowHeadersVisible = false;
            this.dgvInternationalLicensesHistory.RowHeadersWidth = 51;
            this.dgvInternationalLicensesHistory.RowTemplate.Height = 26;
            this.dgvInternationalLicensesHistory.Size = new System.Drawing.Size(853, 140);
            this.dgvInternationalLicensesHistory.TabIndex = 76;
            this.dgvInternationalLicensesHistory.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvInternationalLicensesHistory.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvInternationalLicensesHistory.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvInternationalLicensesHistory.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvInternationalLicensesHistory.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvInternationalLicensesHistory.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvInternationalLicensesHistory.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvInternationalLicensesHistory.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvInternationalLicensesHistory.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvInternationalLicensesHistory.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dgvInternationalLicensesHistory.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvInternationalLicensesHistory.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvInternationalLicensesHistory.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvInternationalLicensesHistory.ThemeStyle.ReadOnly = true;
            this.dgvInternationalLicensesHistory.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvInternationalLicensesHistory.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvInternationalLicensesHistory.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dgvInternationalLicensesHistory.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvInternationalLicensesHistory.ThemeStyle.RowsStyle.Height = 26;
            this.dgvInternationalLicensesHistory.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvInternationalLicensesHistory.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.AutoSize = false;
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(19, 6);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(193, 24);
            this.guna2HtmlLabel2.TabIndex = 75;
            this.guna2HtmlLabel2.Text = "Local Licenses History:";
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.AutoSize = false;
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(6, 182);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(128, 23);
            this.guna2HtmlLabel4.TabIndex = 73;
            this.guna2HtmlLabel4.Text = "# Records:";
            // 
            // ctrlDriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbDriverLicenses);
            this.Name = "ctrlDriverLicenses";
            this.Size = new System.Drawing.Size(875, 340);
            this.gbDriverLicenses.ResumeLayout(false);
            this.tcDriverLicenses.ResumeLayout(false);
            this.tpLocalLicenses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesHistory)).EndInit();
            this.tpInternationalLicenses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GroupBox gbDriverLicenses;
        private Guna.UI2.WinForms.Guna2TabControl tcDriverLicenses;
        private System.Windows.Forms.TabPage tpLocalLicenses;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLocalLicensesRecords;
        private Guna.UI2.WinForms.Guna2DataGridView dgvLocalLicensesHistory;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRecordsCount;
        private System.Windows.Forms.TabPage tpInternationalLicenses;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInternationalLicensesRecords;
        private Guna.UI2.WinForms.Guna2DataGridView dgvInternationalLicensesHistory;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
    }
}
