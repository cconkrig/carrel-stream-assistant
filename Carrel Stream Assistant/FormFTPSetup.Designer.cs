
namespace Carrel_Stream_Assistant
{
    partial class FormFTPSetup
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
            this.label1 = new System.Windows.Forms.Label();
            this.lstFTPServers = new System.Windows.Forms.ListBox();
            this.btnAddFTPServer = new System.Windows.Forms.Button();
            this.btnEditFTPServer = new System.Windows.Forms.Button();
            this.btnDeleteFTPServer = new System.Windows.Forms.Button();
            this.grpFTPServerDetails = new System.Windows.Forms.GroupBox();
            this.btnTestFTP = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.cboSecurity = new System.Windows.Forms.ComboBox();
            this.cboTransferMode = new System.Windows.Forms.ComboBox();
            this.lblSecurityMode = new System.Windows.Forms.Label();
            this.lblFTPTransferMode = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblHostname = new System.Windows.Forms.Label();
            this.grpFTPServerDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "FTP Servers";
            // 
            // lstFTPServers
            // 
            this.lstFTPServers.FormattingEnabled = true;
            this.lstFTPServers.Location = new System.Drawing.Point(15, 37);
            this.lstFTPServers.Name = "lstFTPServers";
            this.lstFTPServers.Size = new System.Drawing.Size(259, 368);
            this.lstFTPServers.TabIndex = 1;
            this.lstFTPServers.SelectedIndexChanged += new System.EventHandler(this.LstFTPServers_SelectedIndexChanged);
            // 
            // btnAddFTPServer
            // 
            this.btnAddFTPServer.Location = new System.Drawing.Point(15, 411);
            this.btnAddFTPServer.Name = "btnAddFTPServer";
            this.btnAddFTPServer.Size = new System.Drawing.Size(82, 37);
            this.btnAddFTPServer.TabIndex = 2;
            this.btnAddFTPServer.Text = "Add";
            this.btnAddFTPServer.UseVisualStyleBackColor = true;
            this.btnAddFTPServer.Click += new System.EventHandler(this.BtnAddFTPServer_Click);
            // 
            // btnEditFTPServer
            // 
            this.btnEditFTPServer.Enabled = false;
            this.btnEditFTPServer.Location = new System.Drawing.Point(104, 411);
            this.btnEditFTPServer.Name = "btnEditFTPServer";
            this.btnEditFTPServer.Size = new System.Drawing.Size(82, 37);
            this.btnEditFTPServer.TabIndex = 3;
            this.btnEditFTPServer.Text = "Edit";
            this.btnEditFTPServer.UseVisualStyleBackColor = true;
            this.btnEditFTPServer.Click += new System.EventHandler(this.BtnEditFTPServer_Click);
            // 
            // btnDeleteFTPServer
            // 
            this.btnDeleteFTPServer.Enabled = false;
            this.btnDeleteFTPServer.Location = new System.Drawing.Point(192, 411);
            this.btnDeleteFTPServer.Name = "btnDeleteFTPServer";
            this.btnDeleteFTPServer.Size = new System.Drawing.Size(82, 37);
            this.btnDeleteFTPServer.TabIndex = 4;
            this.btnDeleteFTPServer.Text = "Delete";
            this.btnDeleteFTPServer.UseVisualStyleBackColor = true;
            this.btnDeleteFTPServer.Click += new System.EventHandler(this.BtnDeleteFTPServer_Click);
            // 
            // grpFTPServerDetails
            // 
            this.grpFTPServerDetails.Controls.Add(this.btnTestFTP);
            this.grpFTPServerDetails.Controls.Add(this.btnSave);
            this.grpFTPServerDetails.Controls.Add(this.btnCancel);
            this.grpFTPServerDetails.Controls.Add(this.txtPassword);
            this.grpFTPServerDetails.Controls.Add(this.txtUsername);
            this.grpFTPServerDetails.Controls.Add(this.txtHostname);
            this.grpFTPServerDetails.Controls.Add(this.cboSecurity);
            this.grpFTPServerDetails.Controls.Add(this.cboTransferMode);
            this.grpFTPServerDetails.Controls.Add(this.lblSecurityMode);
            this.grpFTPServerDetails.Controls.Add(this.lblFTPTransferMode);
            this.grpFTPServerDetails.Controls.Add(this.lblPassword);
            this.grpFTPServerDetails.Controls.Add(this.lblUsername);
            this.grpFTPServerDetails.Controls.Add(this.lblHostname);
            this.grpFTPServerDetails.Enabled = false;
            this.grpFTPServerDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFTPServerDetails.Location = new System.Drawing.Point(300, 30);
            this.grpFTPServerDetails.Name = "grpFTPServerDetails";
            this.grpFTPServerDetails.Size = new System.Drawing.Size(531, 375);
            this.grpFTPServerDetails.TabIndex = 5;
            this.grpFTPServerDetails.TabStop = false;
            this.grpFTPServerDetails.Text = "FTP Server Details";
            // 
            // btnTestFTP
            // 
            this.btnTestFTP.Enabled = false;
            this.btnTestFTP.Location = new System.Drawing.Point(12, 321);
            this.btnTestFTP.Name = "btnTestFTP";
            this.btnTestFTP.Size = new System.Drawing.Size(176, 37);
            this.btnTestFTP.TabIndex = 12;
            this.btnTestFTP.Text = "Test Connection";
            this.btnTestFTP.UseVisualStyleBackColor = true;
            this.btnTestFTP.Click += new System.EventHandler(this.BtnTestFTP_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(317, 321);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 37);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(425, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 37);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(137, 144);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(370, 22);
            this.txtPassword.TabIndex = 9;
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(137, 101);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(370, 22);
            this.txtUsername.TabIndex = 8;
            // 
            // txtHostname
            // 
            this.txtHostname.Enabled = false;
            this.txtHostname.Location = new System.Drawing.Point(137, 58);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(370, 22);
            this.txtHostname.TabIndex = 7;
            // 
            // cboSecurity
            // 
            this.cboSecurity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSecurity.Enabled = false;
            this.cboSecurity.FormattingEnabled = true;
            this.cboSecurity.Items.AddRange(new object[] {
            "No Security (Clear Text - Stand FTP - Default Setting)",
            "FTPS (FTP Secure)"});
            this.cboSecurity.Location = new System.Drawing.Point(140, 208);
            this.cboSecurity.Name = "cboSecurity";
            this.cboSecurity.Size = new System.Drawing.Size(367, 24);
            this.cboSecurity.TabIndex = 6;
            // 
            // cboTransferMode
            // 
            this.cboTransferMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTransferMode.Enabled = false;
            this.cboTransferMode.FormattingEnabled = true;
            this.cboTransferMode.Items.AddRange(new object[] {
            "Passive (Recommended)",
            "Active"});
            this.cboTransferMode.Location = new System.Drawing.Point(140, 248);
            this.cboTransferMode.Name = "cboTransferMode";
            this.cboTransferMode.Size = new System.Drawing.Size(367, 24);
            this.cboTransferMode.TabIndex = 5;
            // 
            // lblSecurityMode
            // 
            this.lblSecurityMode.AutoSize = true;
            this.lblSecurityMode.Enabled = false;
            this.lblSecurityMode.Location = new System.Drawing.Point(9, 211);
            this.lblSecurityMode.Name = "lblSecurityMode";
            this.lblSecurityMode.Size = new System.Drawing.Size(126, 16);
            this.lblSecurityMode.TabIndex = 4;
            this.lblSecurityMode.Text = "FTP Security Mode:";
            // 
            // lblFTPTransferMode
            // 
            this.lblFTPTransferMode.AutoSize = true;
            this.lblFTPTransferMode.Enabled = false;
            this.lblFTPTransferMode.Location = new System.Drawing.Point(9, 251);
            this.lblFTPTransferMode.Name = "lblFTPTransferMode";
            this.lblFTPTransferMode.Size = new System.Drawing.Size(128, 16);
            this.lblFTPTransferMode.TabIndex = 3;
            this.lblFTPTransferMode.Text = "FTP Transfer Mode:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Enabled = false;
            this.lblPassword.Location = new System.Drawing.Point(6, 147);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(71, 16);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Enabled = false;
            this.lblUsername.Location = new System.Drawing.Point(6, 104);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(74, 16);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username:";
            // 
            // lblHostname
            // 
            this.lblHostname.AutoSize = true;
            this.lblHostname.Enabled = false;
            this.lblHostname.Location = new System.Drawing.Point(6, 61);
            this.lblHostname.Name = "lblHostname";
            this.lblHostname.Size = new System.Drawing.Size(73, 16);
            this.lblHostname.TabIndex = 0;
            this.lblHostname.Text = "Hostname:";
            // 
            // FormFTPSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 457);
            this.Controls.Add(this.grpFTPServerDetails);
            this.Controls.Add(this.btnDeleteFTPServer);
            this.Controls.Add(this.btnEditFTPServer);
            this.Controls.Add(this.btnAddFTPServer);
            this.Controls.Add(this.lstFTPServers);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFTPSetup";
            this.Text = "FTP Setup";
            this.grpFTPServerDetails.ResumeLayout(false);
            this.grpFTPServerDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstFTPServers;
        private System.Windows.Forms.Button btnAddFTPServer;
        private System.Windows.Forms.Button btnEditFTPServer;
        private System.Windows.Forms.Button btnDeleteFTPServer;
        private System.Windows.Forms.GroupBox grpFTPServerDetails;
        private System.Windows.Forms.Label lblHostname;
        private System.Windows.Forms.ComboBox cboSecurity;
        private System.Windows.Forms.ComboBox cboTransferMode;
        private System.Windows.Forms.Label lblSecurityMode;
        private System.Windows.Forms.Label lblFTPTransferMode;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtHostname;
        private System.Windows.Forms.Button btnTestFTP;
    }
}