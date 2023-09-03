using System;
using System.Drawing;
using System.Windows.Forms;

namespace Carrel_Stream_Assistant
{
    public partial class FormFTPOutput : Form
    {
        // Reference the Parent Form
        private readonly Form parentForm;
        private int countdownSeconds = 30;
        private Timer countdownTimer;
        private int countdownLineIndex;
        EventHandler loadEventHandler = null; // Declare the event handler variable

        public FormFTPOutput(dynamic parent, string mode, FTPServerItem ftpServer = null, ReelItem reelItem = null, string filename_to_upload = null)
        {
            InitializeComponent();
            // Hookup reference to parent
            parentForm = parent;

            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; // 1 second
            countdownTimer.Tick += CountdownTimer_Tick;

            switch (mode)
            {
                case "testftp":
                    Load += loadEventHandler = (sender, e) => // Assign the event handler to the variable
                    {
                        BeginInvoke((Action)(() =>
                        {
                            this.Invalidate();
                            if (FTPOperations.TestFTPConnection(ftpServer))
                            {
                                TerminalUpdateEventArgs termArgs = new TerminalUpdateEventArgs(
                                    "> TEST WAS SUCCESSFUL!",
                                    Color.Green
                                );
                                UpdateScreen(this, termArgs);
                            }
                            else
                            {
                                TerminalUpdateEventArgs termArgs = new TerminalUpdateEventArgs(
                                    "> TEST FAILED!",
                                    Color.Red
                                );
                                UpdateScreen(this, termArgs);
                            }
                        }));
                    };
                    Closing += (sender, e) =>
                    {
                        Load -= loadEventHandler; // Unsubscribe from the Load event
                    };
                    break;

                case "upload":
                    this.Invalidate();
                    Load += loadEventHandler = (sender, e) => // Assign the event handler to the variable
                    {
                        BeginInvoke((Action)(() =>
                        {
                            progressUpload.Visible = true;
                            progressUpload.Value = 0;
                            if (FTPOperations.UploadFile(ftpServer, reelItem, filename_to_upload, this))
                            {
                                TerminalUpdateEventArgs termArgs = new TerminalUpdateEventArgs(
                                    "> File uploaded successful!",
                                    Color.Green
                                );
                                parent.Invoke((Action)(() =>
                                {
                                    parent.AddLog("Uploaded file to FTP successfully.", Color.Green);
                                }));

                                UpdateScreen(this, termArgs);
                                ftpTerminal.AppendText(Environment.NewLine);
                                countdownLineIndex = ftpTerminal.Lines.Length - 1;
                                countdownTimer.Start();
                            }
                            else
                            {
                                TerminalUpdateEventArgs termArgs = new TerminalUpdateEventArgs(
                                    "> File uploaded failed!",
                                    Color.Red
                                );
                                parent.Invoke((Action)(() =>
                                {
                                    parent.AddLog("Failed to upload file to FTP!", Color.Red);
                                }));
                                UpdateScreen(this, termArgs);
                                ftpTerminal.AppendText(Environment.NewLine);
                                countdownLineIndex = ftpTerminal.Lines.Length - 1;
                                countdownTimer.Start();
                            }
                        }));
                    };
                    Closing += (sender, e) =>
                    {
                        Load -= loadEventHandler; // Unsubscribe from the Load event
                    };
                    break;
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (countdownSeconds > 1)
            {
                countdownSeconds--;
                string updatedLine = $"Closing Window in {countdownSeconds} seconds...";
                int currentSelectionStart = ftpTerminal.SelectionStart;
                ftpTerminal.Select(ftpTerminal.GetFirstCharIndexFromLine(countdownLineIndex), ftpTerminal.Lines[countdownLineIndex].Length);
                ftpTerminal.SelectedText = updatedLine;
                ftpTerminal.Select(currentSelectionStart, 0);
                ftpTerminal.ScrollToCaret();
                countdownLineIndex = ftpTerminal.Lines.Length - 1;
            }
            else
            {
                countdownTimer.Stop();
                Close();
            }
        }
        private void FormFTPOutput_Load(object sender, EventArgs e)
        {
            FTPOperations.ScreenUpdated += UpdateScreen;
        }

        private void UpdateScreen(object sender, TerminalUpdateEventArgs e)
        {
            if (IsDisposed) // Check if the form is disposed
            {
                return; // Exit the method if the form is disposed
            }
            if (e.TerminalLine.StartsWith("> TEST"))
            {
                ftpTerminal.AppendText(Environment.NewLine); // Add a new line
                ftpTerminal.AppendText(Environment.NewLine); // Add a new line
                ftpTerminal.AppendText(Environment.NewLine); // Add a new line
            }
            ftpTerminal.SelectionColor = e.Forecolor;
            ftpTerminal.AppendText(e.TerminalLine);
            ftpTerminal.AppendText(Environment.NewLine); // Add a new line
            if (e.TerminalLine.StartsWith("> TEST"))
            {
                ftpTerminal.AppendText("> You may close this window.");
            }
            ftpTerminal.SelectionStart = ftpTerminal.Text.Length;
            ftpTerminal.ScrollToCaret();
        }

        private void FormFTPOutput_FormClosing(object sender, FormClosingEventArgs e)
        {
            countdownTimer.Enabled = false;
        }
    }
}
