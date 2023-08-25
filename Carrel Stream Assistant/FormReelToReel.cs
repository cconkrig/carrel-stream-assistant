using System;
using System.IO;
using System.Text.RegularExpressions;
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
    public partial class FormReelToReel : Form
    {
        //Reference the Parent Form
        private readonly FormSetup parentForm;
        private string selectedFormat = ".m4a";

        public FormReelToReel(FormSetup parent, string mode)
        {
            InitializeComponent();

            // Hookup reference to parent
            parentForm = parent;

            // Switch the record format drop menu based on mode (Add or Edit)
            switch (mode)
            {
                case "add":
                    cboRecFormat.SelectedIndex = 0;
                    break;

                case "edit":
                    break;
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
            }
            if(txtFilename.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a Path with Filename. An example has been added for you...");
                txtFilename.Text = "C:\\myaudio.m4a";
            }
        }
    }
}
