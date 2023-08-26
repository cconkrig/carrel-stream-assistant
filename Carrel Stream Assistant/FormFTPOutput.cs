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
    public partial class FormFTPOutput : Form
    {
        //Reference the Parent Form
        private readonly FormFTPSetup parentForm;

        public FormFTPOutput(FormFTPSetup parent, string mode, FTPServerItem ftpServer = null)
        {
            InitializeComponent();
            // Hookup reference to parent
            parentForm = parent;

            switch (mode)
            {
                case "testftp":
                    Shown += (sender, e) =>
                    {
                        if (FTPOperations.TestFTPConnection(ftpServer))
                        {
                            TerminalUpdateEventArgs termArgs = new TerminalUpdateEventArgs(
                                "> TEST WAS SUCCESSFUL!",
                                Color.Green
                            );
                            UpdateScreen(this, termArgs);
                        } else
                        {
                            TerminalUpdateEventArgs termArgs = new TerminalUpdateEventArgs(
                                "> TEST FAILED!",
                                Color.Red
                            );
                            UpdateScreen(this, termArgs);
                        }
                    };
                    break;
            }
        }

        private void FormFTPOutput_Load(object sender, EventArgs e)
        {
            FTPOperations.ScreenUpdated += UpdateScreen;
        }

        private void UpdateScreen(object sender, TerminalUpdateEventArgs e)
        {
            if(e.TerminalLine.StartsWith("> TEST"))
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
    }
}
