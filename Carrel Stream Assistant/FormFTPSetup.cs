using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carrel_Stream_Assistant
{
    public partial class FormFTPSetup : Form
    {

        public FormFTPSetup()
        {
            InitializeComponent();
            LoadAllFTPServers();
        }

        private void BtnAddFTPServer_Click(object sender, EventArgs e)
        {
            EnableAndClearFTPDetails();
            btnTestFTP.Enabled = false;
            txtPassword.PasswordChar = '\0'; //enable clear text mode for new servers
            lstFTPServers.SelectedIndex = -1;
        }

        private void BtnEditFTPServer_Click(object sender, EventArgs e)
        {
            EnableAndClearFTPDetails();
            LoadFTPServerDetails();
            btnEditFTPServer.Enabled = false;
            btnDeleteFTPServer.Enabled = false;
        }

        private void BtnDeleteFTPServer_Click(object sender, EventArgs e)
        {
            if (lstFTPServers.SelectedItem is FTPServerItem selectedFTPServer)
            {
                string confirmationMessage = $"Are you sure you want to delete the FTP server: {selectedFTPServer} ?";
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DatabaseOperations.DeleteFTPServer(selectedFTPServer.Id);
                    LoadAllFTPServers(); // Reload the ListBox after deletion
                    DisableAndClearFTPDetails();
                    btnEditFTPServer.Enabled = false;
                    btnDeleteFTPServer.Enabled = false;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string passwordSalt = FTPOperations.GetNewSaltString();
            string passwordHashed = FTPOperations.EncryptAndSaltPassword(txtPassword.Text.Trim(), passwordSalt);

            int ftpServerId = -1;

            if (lstFTPServers.SelectedIndex >= 0)
            {
                if (lstFTPServers.SelectedItem is FTPServerItem selectedFTPServer)
                {
                    ftpServerId = selectedFTPServer.Id;
                }
            }

            FTPServerItem ftpItem = new FTPServerItem
            {
                Id = ftpServerId,
                HostName = txtHostname.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = passwordHashed,
                Salt = passwordSalt,
                SecurityMode = cboSecurity.SelectedIndex,
                TransferMode = cboTransferMode.SelectedIndex
            };

            DatabaseOperations.SaveFtpServerItem(ftpItem);
            DisableAndClearFTPDetails();
            LoadAllFTPServers();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DisableAndClearFTPDetails();
            LoadFTPServerDetails();
        }

        private void LoadAllFTPServers()
        {
            List<FTPServerItem> ftpServers = DatabaseOperations.GetAllFTPServers();
            lstFTPServers.Items.Clear();

            foreach (FTPServerItem ftpServer in ftpServers)
            {
                lstFTPServers.Items.Add(ftpServer);
            }
        }

        private void LoadFTPServerDetails()
        {
            if (lstFTPServers.SelectedItem is FTPServerItem selectedFTPServer)
            {
                txtHostname.Text = selectedFTPServer.HostName;
                txtUsername.Text = selectedFTPServer.Username;
                txtPassword.PasswordChar = '*';
                txtPassword.Text = selectedFTPServer.Password;
                cboSecurity.SelectedIndex = selectedFTPServer.SecurityMode;
                cboTransferMode.SelectedIndex = selectedFTPServer.TransferMode;

                btnAddFTPServer.Enabled = true;
                btnEditFTPServer.Enabled = true;
                btnDeleteFTPServer.Enabled = true;
            }
        }

        private void EnableAndClearFTPDetails()
        {
            btnAddFTPServer.Enabled = false;
            btnEditFTPServer.Enabled = false;
            btnDeleteFTPServer.Enabled = false;

            grpFTPServerDetails.Enabled = true;

            lblHostname.Enabled = true;
            txtHostname.Enabled = true;
            txtHostname.Text = "";

            lblPassword.Enabled = true;
            txtPassword.Enabled = true;
            txtPassword.Text = "";

            lblUsername.Enabled = true;
            txtUsername.Enabled = true;
            txtUsername.Text = "";

            lblSecurityMode.Enabled = true;
            cboSecurity.Enabled = true;
            cboSecurity.SelectedIndex = 0;

            lblFTPTransferMode.Enabled = true;
            cboTransferMode.Enabled = true;
            cboTransferMode.SelectedIndex = 0;

            btnTestFTP.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            txtHostname.Focus();
        }

        private void DisableAndClearFTPDetails()
        {
            btnAddFTPServer.Enabled = true;
            btnEditFTPServer.Enabled = false;
            btnDeleteFTPServer.Enabled = false;

            grpFTPServerDetails.Enabled = false;

            lblHostname.Enabled = false;
            txtHostname.Enabled = false;
            txtHostname.Text = "";

            lblPassword.Enabled = false;
            txtPassword.Enabled = false;
            txtPassword.Text = "";

            lblUsername.Enabled = false;
            txtUsername.Enabled = false;
            txtUsername.Text = "";

            lblSecurityMode.Enabled = false;
            cboSecurity.Enabled = false;
            cboSecurity.SelectedIndex = 0;

            lblFTPTransferMode.Enabled = false;
            cboTransferMode.Enabled = false;
            cboTransferMode.SelectedIndex = 0;

            btnTestFTP.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void LstFTPServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableAndClearFTPDetails();
            LoadFTPServerDetails();
            grpFTPServerDetails.Enabled = true;
            btnTestFTP.Enabled = true;
        }

        private void BtnTestFTP_Click(object sender, EventArgs e)
        {
            FTPServerItem ftpServer = (FTPServerItem)lstFTPServers.SelectedItem;
            FormFTPOutput FormFTPOutput = new FormFTPOutput(this, "testftp", ftpServer);
            FormFTPOutput.ShowDialog();
        }
    }
}
