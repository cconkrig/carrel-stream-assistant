using System.Diagnostics;
using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Carrel_Stream_Assistant
{
    public partial class FormReelToReel : Form
    {
        //Reference the Parent Form
        private readonly FormSetup parentForm;
        #pragma warning disable IDE0044 // Disable the unused member warning
        private int reelItemId = 0;


        public FormReelToReel(FormSetup parent, string mode, ReelItem editReelItem = null)
        {
            InitializeComponent();

            // Hookup reference to parent
            parentForm = parent;
            LoadAllFTPServers();
            cboFTPUpload.SelectedIndex = 0;

            // Switch the record format drop menu based on mode (Add or Edit)
            switch (mode)
            {
                case "add":
                    cboRecFormat.SelectedIndex = 0;
                    break;

                case "edit":
                    //override the 0 (new record) with the actual record id
                    reelItemId = editReelItem.Id;
                    cboRecFormat.SelectedIndex = editReelItem.Format;
                    txtFilename.Text = editReelItem.Filename;
                    txtStartCommand.Text = editReelItem.StartCommand;
                    txtStopCommand.Text = editReelItem.StopCommand;
                    txtMaxLengthSecs.Text = editReelItem.MaxLengthSecs.ToString();
                    // Set selected item based on ftpServerItem.id
                    for (int i = 0; i < cboFTPUpload.Items.Count; i++)
                    {
                        if (cboFTPUpload.Items[i] is FTPServerItem ftpServerItem)
                        {
                            if (ftpServerItem.Id == editReelItem.FTPServerId)
                            {
                                cboFTPUpload.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    if (cboFTPUpload.SelectedIndex > 0)
                    {
                        txtFTPRootPath.Text = editReelItem.FTPPath;
                        txtFTPRootPath.Enabled = true;
                        lblRootPath.Enabled = true;
                    } else
                    {
                        txtFTPRootPath.Text = "";
                        txtFTPRootPath.Enabled = false;
                        lblRootPath.Enabled = false;
                    }
                    break;
            }
        }
        private void LoadAllFTPServers()
        {
            List<FTPServerItem> ftpServers = DatabaseOperations.GetAllFTPServers();
            cboFTPUpload.Items.Clear();
            cboFTPUpload.Items.Add("Do not automatically upload to FTP");
            foreach (FTPServerItem ftpServer in ftpServers)
            {
                cboFTPUpload.Items.Add(ftpServer);
            }
        }
        private void FormReelToReel_Load(object sender, EventArgs e)
        {

        }

        private void BtnReelCancel_Click(object sender, EventArgs e)
        {
            Form currentForm = this;
            currentForm.Close();
        }

        private void BtnSaveReel_Click(object sender, EventArgs e)
        {
            if(cboRecFormat.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Record Format, setting default for you...");
                cboRecFormat.SelectedIndex = 0;
                return;
            }
            if(txtFilename.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a Path with filename, excluding the file extension. An example has been added for you...");
                txtFilename.Text = "C:\\csa-audiofile";
                return;
            }
            if (txtStartCommand.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a Start Command. An example has been added for you...");
                txtStartCommand.Text = "STARTSTREAMRECORD";
                return;
            }
            if (txtStopCommand.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a Stop Command. An example has been added for you...");
                txtStopCommand.Text = "STOPSTREAMRECORD";
                return;
            }
            if (txtMaxLengthSecs.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a Max Length in Seconds. An example of 1 hour in seconds has been added for you...");
                txtMaxLengthSecs.Text = "3600";
                return;
            }

            int? ftpServerId = null;
            if(cboFTPUpload.SelectedIndex > 0)
            {
                FTPServerItem ftpServerItem = cboFTPUpload.Items[cboFTPUpload.SelectedIndex] as FTPServerItem;
                ftpServerId = ftpServerItem.Id;
            }

            // Build the reel item for database insert/update.
            ReelItem reelItem = new ReelItem
            {
                Id = reelItemId,
                Format = cboRecFormat.SelectedIndex,
                Filename = txtFilename.Text.Trim(),
                StartCommand =txtStartCommand.Text.Trim(),
                StopCommand = txtStopCommand.Text.Trim(),
                MaxLengthSecs = Int32.Parse(txtMaxLengthSecs.Text.Trim()),
                FTPServerId = ftpServerId ?? default, // Use null coalescing operator with default value
                FTPPath = txtFTPRootPath.Text
            };
            
            if (ProcessFileName())
            {
                // Save into the db
                DatabaseOperations.SaveReel(reelItem, () =>
                {
                    // Access the MainForm instance using the static variable
                    MainForm mainForm = MainForm.Instance;
                    if (mainForm != null)
                    {
                        DatabaseOperations.LoadReelToReelTableFromDatabase(mainForm.ReelToReelTable, mainForm);
                    }
                });
                parentForm.LoadReelToReelScreen();
                Form currentForm = this;
                currentForm.Close();
            }
        }

        private bool ProcessFileName()
        {
            string fullPath = txtFilename.Text.Trim();
            string directory = Path.GetDirectoryName(fullPath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPath);
            string fileExtension = Path.GetExtension(fullPath);

            if (string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(fileNameWithoutExtension))
            {
                MessageBox.Show("Please provide a valid path and filename without extension.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrEmpty(fileExtension))
            {
                MessageBox.Show("Please enter a filename without an extension.", "Invalid Filename", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Directory.Exists(directory))
            {
                DialogResult result = MessageBox.Show("The directory does not exist. Do you want to create it?", "Directory Missing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(directory);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Could not create the directory: {ex.Message}", "Directory Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            // Check write permission
            if (!HasWritePermission(directory))
            {
                MessageBox.Show("You do not have write permission in the directory.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //everything was ok with the filename
            return true;
        }

        private bool HasWritePermission(string directory)
        {
            try
            {
                string testFilePath = Path.Combine(directory, "test.txt");
                File.WriteAllText(testFilePath, "Test");
                File.Delete(testFilePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void TxtMaxLengthSecs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-numeric characters
            }
        }

        private void TxtMaxLengthSecs_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string numericText = new string(textBox.Text.Where(char.IsDigit).ToArray());

                if (textBox.Text != numericText)
                {
                    int selectionStart = textBox.SelectionStart;
                    textBox.Text = numericText;
                    textBox.SelectionStart = selectionStart;
                }
            }
        }

        private void CboFTPUpload_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboFTPUpload.SelectedIndex > 0)
            {
                lblRootPath.Enabled = true;
                txtFTPRootPath.Enabled = true;
                txtFTPRootPath.Text = "/";
            } else
            {
                txtFTPRootPath.Text = "";
                txtFTPRootPath.Enabled = false;
                lblRootPath.Enabled = false;
            }
        }
    }
}
