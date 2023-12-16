
namespace Carrel_Stream_Assistant
{
    partial class FormSetup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.sliderInputVolume = new System.Windows.Forms.TrackBar();
            this.ComboBoxNetCueProcessingMode = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.BtnCancelGeneral = new System.Windows.Forms.Button();
            this.BtnSaveGeneral = new System.Windows.Forms.Button();
            this.TextBoxStopNetCue = new System.Windows.Forms.TextBox();
            this.LabelNetCueStop = new System.Windows.Forms.Label();
            this.TextBoxStartNetCue = new System.Windows.Forms.TextBox();
            this.LabelNetCueStart = new System.Windows.Forms.Label();
            this.TextBoxUDPListener = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabNetCues = new System.Windows.Forms.TabPage();
            this.lblNetCuesDirections = new System.Windows.Forms.Label();
            this.btnNetCueDelete = new System.Windows.Forms.Button();
            this.btnNetCueEdit = new System.Windows.Forms.Button();
            this.btnNetCueAdd = new System.Windows.Forms.Button();
            this.grpBoxNetCue = new System.Windows.Forms.GroupBox();
            this.txtFriendlyNetCueName = new System.Windows.Forms.TextBox();
            this.lblFriendlyName = new System.Windows.Forms.Label();
            this.btnSaveNetCue = new System.Windows.Forms.Button();
            this.btnCancelNetCue = new System.Windows.Forms.Button();
            this.txtNetCue = new System.Windows.Forms.TextBox();
            this.lblNetCue = new System.Windows.Forms.Label();
            this.lstNetcues = new System.Windows.Forms.ListBox();
            this.tabRotations = new System.Windows.Forms.TabPage();
            this.CheckMuteNetCuePlayback = new System.Windows.Forms.CheckBox();
            this.lblRotInstructions1 = new System.Windows.Forms.Label();
            this.dgRots = new System.Windows.Forms.DataGridView();
            this.Marker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CartPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboRotType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRotDelete = new System.Windows.Forms.Button();
            this.btnRotAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRotNetCues = new System.Windows.Forms.Label();
            this.cboRotNetCues = new System.Windows.Forms.ComboBox();
            this.tabReeltoReel = new System.Windows.Forms.TabPage();
            this.btnFTPSetup = new System.Windows.Forms.Button();
            this.dgReelToReel = new System.Windows.Forms.DataGridView();
            this.R2RColRecording = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2RColStartCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2RColStopCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R2RColMaxLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReel2ReelDelete = new System.Windows.Forms.Button();
            this.btnReel2ReelEdit = new System.Windows.Forms.Button();
            this.btnReel2ReelAdd = new System.Windows.Forms.Button();
            this.tabHardware = new System.Windows.Forms.TabPage();
            this.cboAudioOutput = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboAudioFeedInput = new System.Windows.Forms.ComboBox();
            this.ttVolume = new System.Windows.Forms.ToolTip(this.components);
            this.chkEnableVolumeControl = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderInputVolume)).BeginInit();
            this.tabNetCues.SuspendLayout();
            this.grpBoxNetCue.SuspendLayout();
            this.tabRotations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRots)).BeginInit();
            this.tabReeltoReel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReelToReel)).BeginInit();
            this.tabHardware.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabNetCues);
            this.tabControl1.Controls.Add(this.tabRotations);
            this.tabControl1.Controls.Add(this.tabReeltoReel);
            this.tabControl1.Controls.Add(this.tabHardware);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(670, 482);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.chkEnableVolumeControl);
            this.tabGeneral.Controls.Add(this.sliderInputVolume);
            this.tabGeneral.Controls.Add(this.ComboBoxNetCueProcessingMode);
            this.tabGeneral.Controls.Add(this.label11);
            this.tabGeneral.Controls.Add(this.BtnCancelGeneral);
            this.tabGeneral.Controls.Add(this.BtnSaveGeneral);
            this.tabGeneral.Controls.Add(this.TextBoxStopNetCue);
            this.tabGeneral.Controls.Add(this.LabelNetCueStop);
            this.tabGeneral.Controls.Add(this.TextBoxStartNetCue);
            this.tabGeneral.Controls.Add(this.LabelNetCueStart);
            this.tabGeneral.Controls.Add(this.TextBoxUDPListener);
            this.tabGeneral.Controls.Add(this.label8);
            this.tabGeneral.Controls.Add(this.label9);
            this.tabGeneral.Location = new System.Drawing.Point(4, 25);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(662, 453);
            this.tabGeneral.TabIndex = 3;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // sliderInputVolume
            // 
            this.sliderInputVolume.Enabled = false;
            this.sliderInputVolume.Location = new System.Drawing.Point(17, 291);
            this.sliderInputVolume.Maximum = 100;
            this.sliderInputVolume.Name = "sliderInputVolume";
            this.sliderInputVolume.Size = new System.Drawing.Size(610, 45);
            this.sliderInputVolume.SmallChange = 5;
            this.sliderInputVolume.TabIndex = 20;
            this.sliderInputVolume.TickFrequency = 10;
            this.sliderInputVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.sliderInputVolume.Scroll += new System.EventHandler(this.SliderInputVolume_Scroll);
            // 
            // ComboBoxNetCueProcessingMode
            // 
            this.ComboBoxNetCueProcessingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxNetCueProcessingMode.FormattingEnabled = true;
            this.ComboBoxNetCueProcessingMode.Items.AddRange(new object[] {
            "Always Process Incoming NetCues",
            "Start/Stop Processing via Specific NetCues"});
            this.ComboBoxNetCueProcessingMode.Location = new System.Drawing.Point(239, 108);
            this.ComboBoxNetCueProcessingMode.Name = "ComboBoxNetCueProcessingMode";
            this.ComboBoxNetCueProcessingMode.Size = new System.Drawing.Size(391, 24);
            this.ComboBoxNetCueProcessingMode.TabIndex = 9;
            this.ComboBoxNetCueProcessingMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxNetCueProcessingMode_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(17, 111);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(187, 16);
            this.label11.TabIndex = 8;
            this.label11.Text = "NetCue Processing Mode:";
            // 
            // BtnCancelGeneral
            // 
            this.BtnCancelGeneral.Enabled = false;
            this.BtnCancelGeneral.Location = new System.Drawing.Point(531, 410);
            this.BtnCancelGeneral.Name = "BtnCancelGeneral";
            this.BtnCancelGeneral.Size = new System.Drawing.Size(99, 40);
            this.BtnCancelGeneral.TabIndex = 7;
            this.BtnCancelGeneral.Text = "&Cancel";
            this.BtnCancelGeneral.UseVisualStyleBackColor = true;
            this.BtnCancelGeneral.Click += new System.EventHandler(this.BtnCancelGeneral_Click);
            // 
            // BtnSaveGeneral
            // 
            this.BtnSaveGeneral.Enabled = false;
            this.BtnSaveGeneral.Location = new System.Drawing.Point(413, 410);
            this.BtnSaveGeneral.Name = "BtnSaveGeneral";
            this.BtnSaveGeneral.Size = new System.Drawing.Size(99, 40);
            this.BtnSaveGeneral.TabIndex = 6;
            this.BtnSaveGeneral.Text = "S&ave";
            this.BtnSaveGeneral.UseVisualStyleBackColor = true;
            this.BtnSaveGeneral.Click += new System.EventHandler(this.BtnSaveGeneral_Click);
            // 
            // TextBoxStopNetCue
            // 
            this.TextBoxStopNetCue.Location = new System.Drawing.Point(276, 194);
            this.TextBoxStopNetCue.Name = "TextBoxStopNetCue";
            this.TextBoxStopNetCue.Size = new System.Drawing.Size(354, 22);
            this.TextBoxStopNetCue.TabIndex = 5;
            this.TextBoxStopNetCue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxStopNetCue.TextChanged += new System.EventHandler(this.TextBoxStopNetCue_TextChanged);
            // 
            // LabelNetCueStop
            // 
            this.LabelNetCueStop.AutoSize = true;
            this.LabelNetCueStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNetCueStop.Location = new System.Drawing.Point(17, 197);
            this.LabelNetCueStop.Name = "LabelNetCueStop";
            this.LabelNetCueStop.Size = new System.Drawing.Size(252, 16);
            this.LabelNetCueStop.TabIndex = 4;
            this.LabelNetCueStop.Text = "NetCue to Stop NetCue Processing:";
            // 
            // TextBoxStartNetCue
            // 
            this.TextBoxStartNetCue.Location = new System.Drawing.Point(276, 151);
            this.TextBoxStartNetCue.Name = "TextBoxStartNetCue";
            this.TextBoxStartNetCue.Size = new System.Drawing.Size(354, 22);
            this.TextBoxStartNetCue.TabIndex = 3;
            this.TextBoxStartNetCue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxStartNetCue.TextChanged += new System.EventHandler(this.TextBoxStartNetCue_TextChanged);
            // 
            // LabelNetCueStart
            // 
            this.LabelNetCueStart.AutoSize = true;
            this.LabelNetCueStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNetCueStart.Location = new System.Drawing.Point(17, 154);
            this.LabelNetCueStart.Name = "LabelNetCueStart";
            this.LabelNetCueStart.Size = new System.Drawing.Size(252, 16);
            this.LabelNetCueStart.TabIndex = 2;
            this.LabelNetCueStart.Text = "NetCue to Start NetCue Processing:";
            // 
            // TextBoxUDPListener
            // 
            this.TextBoxUDPListener.Location = new System.Drawing.Point(213, 41);
            this.TextBoxUDPListener.Name = "TextBoxUDPListener";
            this.TextBoxUDPListener.Size = new System.Drawing.Size(86, 22);
            this.TextBoxUDPListener.TabIndex = 1;
            this.TextBoxUDPListener.Text = "9963";
            this.TextBoxUDPListener.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxUDPListener.TextChanged += new System.EventHandler(this.TextBoxUDPListener_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(189, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "NetCue UDP Listener Port:";
            // 
            // label9
            // 
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(20, 334);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(607, 68);
            this.label9.TabIndex = 21;
            this.label9.Text = resources.GetString("label9.Text");
            // 
            // tabNetCues
            // 
            this.tabNetCues.Controls.Add(this.lblNetCuesDirections);
            this.tabNetCues.Controls.Add(this.btnNetCueDelete);
            this.tabNetCues.Controls.Add(this.btnNetCueEdit);
            this.tabNetCues.Controls.Add(this.btnNetCueAdd);
            this.tabNetCues.Controls.Add(this.grpBoxNetCue);
            this.tabNetCues.Controls.Add(this.lstNetcues);
            this.tabNetCues.Location = new System.Drawing.Point(4, 25);
            this.tabNetCues.Name = "tabNetCues";
            this.tabNetCues.Padding = new System.Windows.Forms.Padding(3);
            this.tabNetCues.Size = new System.Drawing.Size(662, 453);
            this.tabNetCues.TabIndex = 0;
            this.tabNetCues.Text = "NetCues";
            this.tabNetCues.UseVisualStyleBackColor = true;
            // 
            // lblNetCuesDirections
            // 
            this.lblNetCuesDirections.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetCuesDirections.Location = new System.Drawing.Point(6, 5);
            this.lblNetCuesDirections.Name = "lblNetCuesDirections";
            this.lblNetCuesDirections.Size = new System.Drawing.Size(637, 39);
            this.lblNetCuesDirections.TabIndex = 6;
            this.lblNetCuesDirections.Text = "Carrel Stream Assistant will only listen for NETCUES defined below. If a NETCUE i" +
    "s received without being added, then it will be logged and ignored.";
            // 
            // btnNetCueDelete
            // 
            this.btnNetCueDelete.Location = new System.Drawing.Point(146, 426);
            this.btnNetCueDelete.Name = "btnNetCueDelete";
            this.btnNetCueDelete.Size = new System.Drawing.Size(70, 23);
            this.btnNetCueDelete.TabIndex = 5;
            this.btnNetCueDelete.Text = "Delete";
            this.btnNetCueDelete.UseVisualStyleBackColor = true;
            this.btnNetCueDelete.Click += new System.EventHandler(this.BtnNetCueDelete_Click);
            // 
            // btnNetCueEdit
            // 
            this.btnNetCueEdit.Location = new System.Drawing.Point(76, 426);
            this.btnNetCueEdit.Name = "btnNetCueEdit";
            this.btnNetCueEdit.Size = new System.Drawing.Size(70, 23);
            this.btnNetCueEdit.TabIndex = 4;
            this.btnNetCueEdit.Text = "Edit";
            this.btnNetCueEdit.UseVisualStyleBackColor = true;
            this.btnNetCueEdit.Click += new System.EventHandler(this.BtnNetCueEdit_Click);
            // 
            // btnNetCueAdd
            // 
            this.btnNetCueAdd.Location = new System.Drawing.Point(6, 426);
            this.btnNetCueAdd.Name = "btnNetCueAdd";
            this.btnNetCueAdd.Size = new System.Drawing.Size(70, 23);
            this.btnNetCueAdd.TabIndex = 3;
            this.btnNetCueAdd.Text = "Add";
            this.btnNetCueAdd.UseVisualStyleBackColor = true;
            this.btnNetCueAdd.Click += new System.EventHandler(this.BtnNetCueAdd_Click);
            // 
            // grpBoxNetCue
            // 
            this.grpBoxNetCue.Controls.Add(this.txtFriendlyNetCueName);
            this.grpBoxNetCue.Controls.Add(this.lblFriendlyName);
            this.grpBoxNetCue.Controls.Add(this.btnSaveNetCue);
            this.grpBoxNetCue.Controls.Add(this.btnCancelNetCue);
            this.grpBoxNetCue.Controls.Add(this.txtNetCue);
            this.grpBoxNetCue.Controls.Add(this.lblNetCue);
            this.grpBoxNetCue.Location = new System.Drawing.Point(232, 42);
            this.grpBoxNetCue.Name = "grpBoxNetCue";
            this.grpBoxNetCue.Size = new System.Drawing.Size(411, 384);
            this.grpBoxNetCue.TabIndex = 2;
            this.grpBoxNetCue.TabStop = false;
            this.grpBoxNetCue.Text = "NetCue Details";
            // 
            // txtFriendlyNetCueName
            // 
            this.txtFriendlyNetCueName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFriendlyNetCueName.Location = new System.Drawing.Point(121, 76);
            this.txtFriendlyNetCueName.Name = "txtFriendlyNetCueName";
            this.txtFriendlyNetCueName.Size = new System.Drawing.Size(243, 22);
            this.txtFriendlyNetCueName.TabIndex = 5;
            // 
            // lblFriendlyName
            // 
            this.lblFriendlyName.AutoSize = true;
            this.lblFriendlyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFriendlyName.Location = new System.Drawing.Point(16, 79);
            this.lblFriendlyName.Name = "lblFriendlyName";
            this.lblFriendlyName.Size = new System.Drawing.Size(98, 16);
            this.lblFriendlyName.TabIndex = 4;
            this.lblFriendlyName.Text = "Friendly Name:";
            // 
            // btnSaveNetCue
            // 
            this.btnSaveNetCue.Location = new System.Drawing.Point(208, 330);
            this.btnSaveNetCue.Name = "btnSaveNetCue";
            this.btnSaveNetCue.Size = new System.Drawing.Size(75, 23);
            this.btnSaveNetCue.TabIndex = 3;
            this.btnSaveNetCue.Text = "&Save";
            this.btnSaveNetCue.UseVisualStyleBackColor = true;
            this.btnSaveNetCue.Click += new System.EventHandler(this.BtnSaveNetCue_Click);
            // 
            // btnCancelNetCue
            // 
            this.btnCancelNetCue.Location = new System.Drawing.Point(289, 330);
            this.btnCancelNetCue.Name = "btnCancelNetCue";
            this.btnCancelNetCue.Size = new System.Drawing.Size(75, 23);
            this.btnCancelNetCue.TabIndex = 2;
            this.btnCancelNetCue.Text = "&Cancel";
            this.btnCancelNetCue.UseVisualStyleBackColor = true;
            this.btnCancelNetCue.Click += new System.EventHandler(this.BtnCancelNetCue_Click);
            // 
            // txtNetCue
            // 
            this.txtNetCue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetCue.Location = new System.Drawing.Point(121, 41);
            this.txtNetCue.Name = "txtNetCue";
            this.txtNetCue.Size = new System.Drawing.Size(243, 22);
            this.txtNetCue.TabIndex = 1;
            this.txtNetCue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNetCue_KeyPress);
            // 
            // lblNetCue
            // 
            this.lblNetCue.AutoSize = true;
            this.lblNetCue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetCue.Location = new System.Drawing.Point(23, 44);
            this.lblNetCue.Name = "lblNetCue";
            this.lblNetCue.Size = new System.Drawing.Size(91, 16);
            this.lblNetCue.TabIndex = 0;
            this.lblNetCue.Text = "NetCue Code:";
            // 
            // lstNetcues
            // 
            this.lstNetcues.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstNetcues.FormattingEnabled = true;
            this.lstNetcues.ItemHeight = 16;
            this.lstNetcues.Location = new System.Drawing.Point(6, 45);
            this.lstNetcues.Name = "lstNetcues";
            this.lstNetcues.Size = new System.Drawing.Size(209, 372);
            this.lstNetcues.TabIndex = 1;
            this.lstNetcues.SelectedIndexChanged += new System.EventHandler(this.LstNetcues_SelectedIndexChanged);
            // 
            // tabRotations
            // 
            this.tabRotations.Controls.Add(this.CheckMuteNetCuePlayback);
            this.tabRotations.Controls.Add(this.lblRotInstructions1);
            this.tabRotations.Controls.Add(this.dgRots);
            this.tabRotations.Controls.Add(this.cboRotType);
            this.tabRotations.Controls.Add(this.label2);
            this.tabRotations.Controls.Add(this.btnRotDelete);
            this.tabRotations.Controls.Add(this.btnRotAdd);
            this.tabRotations.Controls.Add(this.label1);
            this.tabRotations.Controls.Add(this.lblRotNetCues);
            this.tabRotations.Controls.Add(this.cboRotNetCues);
            this.tabRotations.Location = new System.Drawing.Point(4, 25);
            this.tabRotations.Name = "tabRotations";
            this.tabRotations.Padding = new System.Windows.Forms.Padding(3);
            this.tabRotations.Size = new System.Drawing.Size(662, 453);
            this.tabRotations.TabIndex = 1;
            this.tabRotations.Text = "Rotations";
            this.tabRotations.UseVisualStyleBackColor = true;
            // 
            // CheckMuteNetCuePlayback
            // 
            this.CheckMuteNetCuePlayback.AutoSize = true;
            this.CheckMuteNetCuePlayback.Enabled = false;
            this.CheckMuteNetCuePlayback.ForeColor = System.Drawing.Color.Black;
            this.CheckMuteNetCuePlayback.Location = new System.Drawing.Point(418, 52);
            this.CheckMuteNetCuePlayback.Name = "CheckMuteNetCuePlayback";
            this.CheckMuteNetCuePlayback.Size = new System.Drawing.Size(228, 20);
            this.CheckMuteNetCuePlayback.TabIndex = 10;
            this.CheckMuteNetCuePlayback.Text = "Mute Audio Feed during Playback";
            this.CheckMuteNetCuePlayback.UseVisualStyleBackColor = true;
            this.CheckMuteNetCuePlayback.CheckedChanged += new System.EventHandler(this.CheckMuteNetCuePlayback_CheckedChanged);
            // 
            // lblRotInstructions1
            // 
            this.lblRotInstructions1.AutoSize = true;
            this.lblRotInstructions1.Location = new System.Drawing.Point(470, 226);
            this.lblRotInstructions1.Name = "lblRotInstructions1";
            this.lblRotInstructions1.Size = new System.Drawing.Size(176, 80);
            this.lblRotInstructions1.TabIndex = 9;
            this.lblRotInstructions1.Text = "ALT + M  =  Set Marker\r\nALT + A   =  Add Line\r\nALT + D   =  Delete Line\r\nALT + ↑ " +
    "   =  Move Line Up\r\nALT + ↓    =  Move Line Down";
            // 
            // dgRots
            // 
            this.dgRots.AllowUserToAddRows = false;
            this.dgRots.AllowUserToResizeColumns = false;
            this.dgRots.AllowUserToResizeRows = false;
            this.dgRots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRots.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Marker,
            this.Cart,
            this.DateStart,
            this.DateEnd,
            this.CartPath});
            this.dgRots.Location = new System.Drawing.Point(6, 128);
            this.dgRots.MultiSelect = false;
            this.dgRots.Name = "dgRots";
            this.dgRots.RowHeadersVisible = false;
            this.dgRots.Size = new System.Drawing.Size(445, 274);
            this.dgRots.TabIndex = 8;
            this.dgRots.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgRots_CellClick);
            this.dgRots.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DgRots_Scroll);
            // 
            // Marker
            // 
            this.Marker.HeaderText = ">";
            this.Marker.MaxInputLength = 1;
            this.Marker.MinimumWidth = 20;
            this.Marker.Name = "Marker";
            this.Marker.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Marker.Width = 20;
            // 
            // Cart
            // 
            this.Cart.HeaderText = "Cart Name";
            this.Cart.Name = "Cart";
            this.Cart.ReadOnly = true;
            // 
            // DateStart
            // 
            dataGridViewCellStyle3.Format = "yyyy-MM-dd HH:mm";
            this.DateStart.DefaultCellStyle = dataGridViewCellStyle3;
            this.DateStart.HeaderText = "Start Date";
            this.DateStart.Name = "DateStart";
            this.DateStart.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DateStart.Width = 150;
            // 
            // DateEnd
            // 
            dataGridViewCellStyle4.Format = "yyyy-MM-dd HH:mm";
            this.DateEnd.DefaultCellStyle = dataGridViewCellStyle4;
            this.DateEnd.HeaderText = "End Date";
            this.DateEnd.Name = "DateEnd";
            this.DateEnd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DateEnd.Width = 150;
            // 
            // CartPath
            // 
            this.CartPath.HeaderText = "Cart Path";
            this.CartPath.Name = "CartPath";
            this.CartPath.Visible = false;
            // 
            // cboRotType
            // 
            this.cboRotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRotType.Enabled = false;
            this.cboRotType.FormattingEnabled = true;
            this.cboRotType.Items.AddRange(new object[] {
            "Simple Audio Rotation"});
            this.cboRotType.Location = new System.Drawing.Point(6, 75);
            this.cboRotType.Name = "cboRotType";
            this.cboRotType.Size = new System.Drawing.Size(373, 24);
            this.cboRotType.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(474, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cart Rotation for NetCue (dates in YYYY-MM-DD HH:mm format, 24 hour format)";
            // 
            // btnRotDelete
            // 
            this.btnRotDelete.Enabled = false;
            this.btnRotDelete.Location = new System.Drawing.Point(326, 408);
            this.btnRotDelete.Name = "btnRotDelete";
            this.btnRotDelete.Size = new System.Drawing.Size(125, 39);
            this.btnRotDelete.TabIndex = 5;
            this.btnRotDelete.Text = "Delete";
            this.btnRotDelete.UseVisualStyleBackColor = true;
            this.btnRotDelete.Click += new System.EventHandler(this.BtnRotDelete_Click);
            // 
            // btnRotAdd
            // 
            this.btnRotAdd.Location = new System.Drawing.Point(184, 408);
            this.btnRotAdd.Name = "btnRotAdd";
            this.btnRotAdd.Size = new System.Drawing.Size(125, 39);
            this.btnRotAdd.TabIndex = 4;
            this.btnRotAdd.Text = "Add";
            this.btnRotAdd.UseVisualStyleBackColor = true;
            this.btnRotAdd.Click += new System.EventHandler(this.BtnRotAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rotation Type";
            // 
            // lblRotNetCues
            // 
            this.lblRotNetCues.AutoSize = true;
            this.lblRotNetCues.Location = new System.Drawing.Point(6, 6);
            this.lblRotNetCues.Name = "lblRotNetCues";
            this.lblRotNetCues.Size = new System.Drawing.Size(52, 16);
            this.lblRotNetCues.TabIndex = 1;
            this.lblRotNetCues.Text = "NetCue";
            // 
            // cboRotNetCues
            // 
            this.cboRotNetCues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRotNetCues.FormattingEnabled = true;
            this.cboRotNetCues.Location = new System.Drawing.Point(6, 25);
            this.cboRotNetCues.Name = "cboRotNetCues";
            this.cboRotNetCues.Size = new System.Drawing.Size(373, 24);
            this.cboRotNetCues.TabIndex = 0;
            this.cboRotNetCues.SelectedIndexChanged += new System.EventHandler(this.CboRotNetCues_SelectedIndexChanged);
            // 
            // tabReeltoReel
            // 
            this.tabReeltoReel.Controls.Add(this.btnFTPSetup);
            this.tabReeltoReel.Controls.Add(this.dgReelToReel);
            this.tabReeltoReel.Controls.Add(this.btnReel2ReelDelete);
            this.tabReeltoReel.Controls.Add(this.btnReel2ReelEdit);
            this.tabReeltoReel.Controls.Add(this.btnReel2ReelAdd);
            this.tabReeltoReel.Location = new System.Drawing.Point(4, 25);
            this.tabReeltoReel.Name = "tabReeltoReel";
            this.tabReeltoReel.Size = new System.Drawing.Size(662, 453);
            this.tabReeltoReel.TabIndex = 4;
            this.tabReeltoReel.Text = "Reel-to-Reel";
            this.tabReeltoReel.UseVisualStyleBackColor = true;
            // 
            // btnFTPSetup
            // 
            this.btnFTPSetup.Location = new System.Drawing.Point(18, 405);
            this.btnFTPSetup.Name = "btnFTPSetup";
            this.btnFTPSetup.Size = new System.Drawing.Size(211, 32);
            this.btnFTPSetup.TabIndex = 6;
            this.btnFTPSetup.Text = "FTP Setup";
            this.btnFTPSetup.UseVisualStyleBackColor = true;
            this.btnFTPSetup.Click += new System.EventHandler(this.BtnFTPSetup_Click);
            // 
            // dgReelToReel
            // 
            this.dgReelToReel.AllowUserToAddRows = false;
            this.dgReelToReel.AllowUserToDeleteRows = false;
            this.dgReelToReel.AllowUserToResizeRows = false;
            this.dgReelToReel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReelToReel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.R2RColRecording,
            this.R2RColStartCommand,
            this.R2RColStopCommand,
            this.R2RColMaxLength});
            this.dgReelToReel.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgReelToReel.Location = new System.Drawing.Point(18, 20);
            this.dgReelToReel.Name = "dgReelToReel";
            this.dgReelToReel.ReadOnly = true;
            this.dgReelToReel.RowHeadersVisible = false;
            this.dgReelToReel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgReelToReel.Size = new System.Drawing.Size(628, 379);
            this.dgReelToReel.TabIndex = 5;
            // 
            // R2RColRecording
            // 
            this.R2RColRecording.HeaderText = "Recording";
            this.R2RColRecording.Name = "R2RColRecording";
            this.R2RColRecording.ReadOnly = true;
            this.R2RColRecording.Width = 275;
            // 
            // R2RColStartCommand
            // 
            this.R2RColStartCommand.HeaderText = "Start Command";
            this.R2RColStartCommand.Name = "R2RColStartCommand";
            this.R2RColStartCommand.ReadOnly = true;
            this.R2RColStartCommand.Width = 140;
            // 
            // R2RColStopCommand
            // 
            this.R2RColStopCommand.HeaderText = "Stop Command";
            this.R2RColStopCommand.Name = "R2RColStopCommand";
            this.R2RColStopCommand.ReadOnly = true;
            this.R2RColStopCommand.Width = 140;
            // 
            // R2RColMaxLength
            // 
            this.R2RColMaxLength.HeaderText = "Max Length";
            this.R2RColMaxLength.Name = "R2RColMaxLength";
            this.R2RColMaxLength.ReadOnly = true;
            this.R2RColMaxLength.Width = 50;
            // 
            // btnReel2ReelDelete
            // 
            this.btnReel2ReelDelete.Enabled = false;
            this.btnReel2ReelDelete.Location = new System.Drawing.Point(568, 405);
            this.btnReel2ReelDelete.Name = "btnReel2ReelDelete";
            this.btnReel2ReelDelete.Size = new System.Drawing.Size(78, 32);
            this.btnReel2ReelDelete.TabIndex = 4;
            this.btnReel2ReelDelete.Text = "Delete";
            this.btnReel2ReelDelete.UseVisualStyleBackColor = true;
            // 
            // btnReel2ReelEdit
            // 
            this.btnReel2ReelEdit.Enabled = false;
            this.btnReel2ReelEdit.Location = new System.Drawing.Point(484, 405);
            this.btnReel2ReelEdit.Name = "btnReel2ReelEdit";
            this.btnReel2ReelEdit.Size = new System.Drawing.Size(78, 32);
            this.btnReel2ReelEdit.TabIndex = 3;
            this.btnReel2ReelEdit.Text = "Edit";
            this.btnReel2ReelEdit.UseVisualStyleBackColor = true;
            this.btnReel2ReelEdit.Click += new System.EventHandler(this.BtnReel2ReelEdit_Click);
            // 
            // btnReel2ReelAdd
            // 
            this.btnReel2ReelAdd.Location = new System.Drawing.Point(400, 405);
            this.btnReel2ReelAdd.Name = "btnReel2ReelAdd";
            this.btnReel2ReelAdd.Size = new System.Drawing.Size(78, 32);
            this.btnReel2ReelAdd.TabIndex = 2;
            this.btnReel2ReelAdd.Text = "Add";
            this.btnReel2ReelAdd.UseVisualStyleBackColor = true;
            this.btnReel2ReelAdd.Click += new System.EventHandler(this.BtnReel2ReelAdd_Click);
            // 
            // tabHardware
            // 
            this.tabHardware.Controls.Add(this.cboAudioOutput);
            this.tabHardware.Controls.Add(this.label5);
            this.tabHardware.Controls.Add(this.label6);
            this.tabHardware.Controls.Add(this.label4);
            this.tabHardware.Controls.Add(this.label3);
            this.tabHardware.Controls.Add(this.cboAudioFeedInput);
            this.tabHardware.Location = new System.Drawing.Point(4, 25);
            this.tabHardware.Name = "tabHardware";
            this.tabHardware.Size = new System.Drawing.Size(662, 453);
            this.tabHardware.TabIndex = 2;
            this.tabHardware.Text = "Hardware";
            this.tabHardware.UseVisualStyleBackColor = true;
            // 
            // cboAudioOutput
            // 
            this.cboAudioOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAudioOutput.FormattingEnabled = true;
            this.cboAudioOutput.Location = new System.Drawing.Point(139, 228);
            this.cboAudioOutput.Name = "cboAudioOutput";
            this.cboAudioOutput.Size = new System.Drawing.Size(493, 24);
            this.cboAudioOutput.TabIndex = 3;
            this.cboAudioOutput.SelectedIndexChanged += new System.EventHandler(this.CboAudioOutput_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(602, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "The audio output is the audio card or virtual audio card that Carrel Stream Assis" +
    "tant will play audio on.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Audio Output:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(620, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "The audio feed input is the input that will be muted if a NetCue is configured to" +
    " be muted during playback.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Audio Feed Input:";
            // 
            // cboAudioFeedInput
            // 
            this.cboAudioFeedInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAudioFeedInput.FormattingEnabled = true;
            this.cboAudioFeedInput.Location = new System.Drawing.Point(139, 93);
            this.cboAudioFeedInput.Name = "cboAudioFeedInput";
            this.cboAudioFeedInput.Size = new System.Drawing.Size(493, 24);
            this.cboAudioFeedInput.TabIndex = 0;
            this.cboAudioFeedInput.SelectedIndexChanged += new System.EventHandler(this.CboAudioFeedInput_SelectedIndexChanged);
            // 
            // chkEnableVolumeControl
            // 
            this.chkEnableVolumeControl.AutoSize = true;
            this.chkEnableVolumeControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableVolumeControl.Location = new System.Drawing.Point(20, 265);
            this.chkEnableVolumeControl.Name = "chkEnableVolumeControl";
            this.chkEnableVolumeControl.Size = new System.Drawing.Size(468, 20);
            this.chkEnableVolumeControl.TabIndex = 22;
            this.chkEnableVolumeControl.Text = "Enable Audio Input Volume Control When Processing is Enabled";
            this.chkEnableVolumeControl.UseVisualStyleBackColor = true;
            this.chkEnableVolumeControl.CheckedChanged += new System.EventHandler(this.chkEnableVolumeControl_CheckedChanged);
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 497);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetup";
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.FormSetup_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderInputVolume)).EndInit();
            this.tabNetCues.ResumeLayout(false);
            this.grpBoxNetCue.ResumeLayout(false);
            this.grpBoxNetCue.PerformLayout();
            this.tabRotations.ResumeLayout(false);
            this.tabRotations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRots)).EndInit();
            this.tabReeltoReel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgReelToReel)).EndInit();
            this.tabHardware.ResumeLayout(false);
            this.tabHardware.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabNetCues;
        private System.Windows.Forms.Button btnNetCueDelete;
        private System.Windows.Forms.Button btnNetCueEdit;
        private System.Windows.Forms.Button btnNetCueAdd;
        private System.Windows.Forms.GroupBox grpBoxNetCue;
        private System.Windows.Forms.ListBox lstNetcues;
        private System.Windows.Forms.TabPage tabRotations;
        private System.Windows.Forms.Label lblNetCuesDirections;
        private System.Windows.Forms.TabPage tabHardware;
        private System.Windows.Forms.Button btnSaveNetCue;
        private System.Windows.Forms.Button btnCancelNetCue;
        private System.Windows.Forms.TextBox txtNetCue;
        private System.Windows.Forms.Label lblNetCue;
        private System.Windows.Forms.Label lblRotNetCues;
        private System.Windows.Forms.ComboBox cboRotNetCues;
        private System.Windows.Forms.ComboBox cboRotType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRotDelete;
        private System.Windows.Forms.Button btnRotAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboAudioFeedInput;
        private System.Windows.Forms.ComboBox cboAudioOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgRots;
        private System.Windows.Forms.Label lblRotInstructions1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marker;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cart;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn CartPath;
        private System.Windows.Forms.CheckBox CheckMuteNetCuePlayback;
        private System.Windows.Forms.TextBox txtFriendlyNetCueName;
        private System.Windows.Forms.Label lblFriendlyName;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.Button BtnCancelGeneral;
        private System.Windows.Forms.Button BtnSaveGeneral;
        private System.Windows.Forms.TextBox TextBoxStopNetCue;
        private System.Windows.Forms.Label LabelNetCueStop;
        private System.Windows.Forms.TextBox TextBoxStartNetCue;
        private System.Windows.Forms.Label LabelNetCueStart;
        private System.Windows.Forms.TextBox TextBoxUDPListener;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ComboBoxNetCueProcessingMode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar sliderInputVolume;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolTip ttVolume;
        private System.Windows.Forms.TabPage tabReeltoReel;
        private System.Windows.Forms.Button btnReel2ReelDelete;
        private System.Windows.Forms.Button btnReel2ReelEdit;
        private System.Windows.Forms.Button btnReel2ReelAdd;
        private System.Windows.Forms.DataGridView dgReelToReel;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2RColRecording;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2RColStartCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2RColStopCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn R2RColMaxLength;
        private System.Windows.Forms.Button btnFTPSetup;
        private System.Windows.Forms.CheckBox chkEnableVolumeControl;
    }
}