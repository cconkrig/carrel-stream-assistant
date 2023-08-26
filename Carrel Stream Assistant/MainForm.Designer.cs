
namespace Carrel_Stream_Assistant
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lstLog = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.TslblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TslblMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.TslblStartStop = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnStopPlayback = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PbPlaybackProgress = new Carrel_Stream_Assistant.NewProgressBar();
            this.LabelPlayer1Muted = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LabelPlayer1Filename = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PbVuRight = new System.Windows.Forms.PictureBox();
            this.PbVuLeft = new System.Windows.Forms.PictureBox();
            this.lblPlaybackTimeLeft = new System.Windows.Forms.Label();
            this.VuMeterTimer = new System.Windows.Forms.Timer(this.components);
            this.lstQueue = new System.Windows.Forms.ListBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.lblQueue = new System.Windows.Forms.Label();
            this.btnClearQueue = new System.Windows.Forms.Button();
            this.BtnStopandClear = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PbPlaybackProgress2 = new Carrel_Stream_Assistant.NewProgressBar();
            this.LabelPlayer2Muted = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LabelPlayer2Filename = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PbVuRight2 = new System.Windows.Forms.PictureBox();
            this.PbVuLeft2 = new System.Windows.Forms.PictureBox();
            this.lblPlaybackTimeLeft2 = new System.Windows.Forms.Label();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyIconContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitApplicationStopServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblR2RStatus = new System.Windows.Forms.Label();
            this.lblR2RTimeElapsed = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblR2RFileName = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblR2RMaxTime = new System.Windows.Forms.Label();
            this.lblR2RUpload = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbVuRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbVuLeft)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbVuRight2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbVuLeft2)).BeginInit();
            this.NotifyIconContext.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.ItemHeight = 16;
            this.lstLog.Location = new System.Drawing.Point(12, 452);
            this.lstLog.Name = "lstLog";
            this.lstLog.ScrollAlwaysVisible = true;
            this.lstLog.Size = new System.Drawing.Size(818, 132);
            this.lstLog.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1171, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.configurationToolStripMenuItem.Text = "Setup";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.ConfigurationToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TslblStatus,
            this.TslblMode,
            this.TslblStartStop});
            this.statusBar.Location = new System.Drawing.Point(0, 587);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1171, 22);
            this.statusBar.TabIndex = 4;
            this.statusBar.Text = "statusStrip1";
            // 
            // TslblStatus
            // 
            this.TslblStatus.Name = "TslblStatus";
            this.TslblStatus.Size = new System.Drawing.Size(43, 17);
            this.TslblStatus.Text = "Offline";
            // 
            // TslblMode
            // 
            this.TslblMode.Name = "TslblMode";
            this.TslblMode.Padding = new System.Windows.Forms.Padding(0, 0, 50, 0);
            this.TslblMode.Size = new System.Drawing.Size(1113, 17);
            this.TslblMode.Spring = true;
            this.TslblMode.Text = "Mode";
            // 
            // TslblStartStop
            // 
            this.TslblStartStop.Name = "TslblStartStop";
            this.TslblStartStop.Size = new System.Drawing.Size(118, 17);
            this.TslblStartStop.Text = "toolStripStatusLabel1";
            this.TslblStartStop.Visible = false;
            // 
            // BtnStopPlayback
            // 
            this.BtnStopPlayback.BackColor = System.Drawing.Color.Navy;
            this.BtnStopPlayback.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStopPlayback.ForeColor = System.Drawing.Color.White;
            this.BtnStopPlayback.Location = new System.Drawing.Point(639, 54);
            this.BtnStopPlayback.Name = "BtnStopPlayback";
            this.BtnStopPlayback.Size = new System.Drawing.Size(191, 75);
            this.BtnStopPlayback.TabIndex = 7;
            this.BtnStopPlayback.Text = "Stop All Playback";
            this.BtnStopPlayback.UseVisualStyleBackColor = false;
            this.BtnStopPlayback.Click += new System.EventHandler(this.BtnStopPlayback_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PbPlaybackProgress);
            this.groupBox1.Controls.Add(this.LabelPlayer1Muted);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.LabelPlayer1Filename);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.PbVuRight);
            this.groupBox1.Controls.Add(this.PbVuLeft);
            this.groupBox1.Controls.Add(this.lblPlaybackTimeLeft);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 121);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio Player 1";
            // 
            // PbPlaybackProgress
            // 
            this.PbPlaybackProgress.Location = new System.Drawing.Point(50, 89);
            this.PbPlaybackProgress.Name = "PbPlaybackProgress";
            this.PbPlaybackProgress.Size = new System.Drawing.Size(521, 22);
            this.PbPlaybackProgress.TabIndex = 9;
            // 
            // LabelPlayer1Muted
            // 
            this.LabelPlayer1Muted.AutoSize = true;
            this.LabelPlayer1Muted.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPlayer1Muted.Location = new System.Drawing.Point(155, 63);
            this.LabelPlayer1Muted.Name = "LabelPlayer1Muted";
            this.LabelPlayer1Muted.Size = new System.Drawing.Size(0, 16);
            this.LabelPlayer1Muted.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Muted Output:";
            // 
            // LabelPlayer1Filename
            // 
            this.LabelPlayer1Filename.AutoSize = true;
            this.LabelPlayer1Filename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPlayer1Filename.Location = new System.Drawing.Point(155, 27);
            this.LabelPlayer1Filename.Name = "LabelPlayer1Filename";
            this.LabelPlayer1Filename.Size = new System.Drawing.Size(0, 16);
            this.LabelPlayer1Filename.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cart Name:";
            // 
            // PbVuRight
            // 
            this.PbVuRight.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.PbVuRight.BackColor = System.Drawing.SystemColors.Control;
            this.PbVuRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PbVuRight.Location = new System.Drawing.Point(25, 27);
            this.PbVuRight.Name = "PbVuRight";
            this.PbVuRight.Size = new System.Drawing.Size(14, 85);
            this.PbVuRight.TabIndex = 1;
            this.PbVuRight.TabStop = false;
            // 
            // PbVuLeft
            // 
            this.PbVuLeft.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.PbVuLeft.BackColor = System.Drawing.SystemColors.Control;
            this.PbVuLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PbVuLeft.Location = new System.Drawing.Point(9, 27);
            this.PbVuLeft.Name = "PbVuLeft";
            this.PbVuLeft.Size = new System.Drawing.Size(14, 85);
            this.PbVuLeft.TabIndex = 0;
            this.PbVuLeft.TabStop = false;
            // 
            // lblPlaybackTimeLeft
            // 
            this.lblPlaybackTimeLeft.AutoSize = true;
            this.lblPlaybackTimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaybackTimeLeft.Location = new System.Drawing.Point(362, 49);
            this.lblPlaybackTimeLeft.Name = "lblPlaybackTimeLeft";
            this.lblPlaybackTimeLeft.Size = new System.Drawing.Size(218, 42);
            this.lblPlaybackTimeLeft.TabIndex = 3;
            this.lblPlaybackTimeLeft.Text = "-00:00:00.0";
            this.lblPlaybackTimeLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstQueue
            // 
            this.lstQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstQueue.FormattingEnabled = true;
            this.lstQueue.ItemHeight = 16;
            this.lstQueue.Location = new System.Drawing.Point(12, 352);
            this.lstQueue.Name = "lstQueue";
            this.lstQueue.Size = new System.Drawing.Size(818, 68);
            this.lstQueue.TabIndex = 9;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.Location = new System.Drawing.Point(12, 434);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(57, 16);
            this.lblLog.TabIndex = 10;
            this.lblLog.Text = "History";
            // 
            // lblQueue
            // 
            this.lblQueue.AutoSize = true;
            this.lblQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQueue.Location = new System.Drawing.Point(12, 333);
            this.lblQueue.Name = "lblQueue";
            this.lblQueue.Size = new System.Drawing.Size(122, 16);
            this.lblQueue.TabIndex = 11;
            this.lblQueue.Text = "Playback Queue";
            // 
            // btnClearQueue
            // 
            this.btnClearQueue.BackColor = System.Drawing.Color.Orange;
            this.btnClearQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearQueue.Location = new System.Drawing.Point(639, 145);
            this.btnClearQueue.Name = "btnClearQueue";
            this.btnClearQueue.Size = new System.Drawing.Size(191, 75);
            this.btnClearQueue.TabIndex = 12;
            this.btnClearQueue.Text = "Clear Playback Queue";
            this.btnClearQueue.UseVisualStyleBackColor = false;
            this.btnClearQueue.Click += new System.EventHandler(this.BtnClearQueue_Click);
            // 
            // BtnStopandClear
            // 
            this.BtnStopandClear.BackColor = System.Drawing.Color.Red;
            this.BtnStopandClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStopandClear.ForeColor = System.Drawing.Color.White;
            this.BtnStopandClear.Location = new System.Drawing.Point(639, 237);
            this.BtnStopandClear.Name = "BtnStopandClear";
            this.BtnStopandClear.Size = new System.Drawing.Size(191, 75);
            this.BtnStopandClear.TabIndex = 13;
            this.BtnStopandClear.Text = "Stop && Clear Playback Queue";
            this.BtnStopandClear.UseVisualStyleBackColor = false;
            this.BtnStopandClear.Click += new System.EventHandler(this.BtnStopandClear_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PbPlaybackProgress2);
            this.groupBox2.Controls.Add(this.LabelPlayer2Muted);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.LabelPlayer2Filename);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.PbVuRight2);
            this.groupBox2.Controls.Add(this.PbVuLeft2);
            this.groupBox2.Controls.Add(this.lblPlaybackTimeLeft2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(15, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(591, 121);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Audio Player 2";
            // 
            // PbPlaybackProgress2
            // 
            this.PbPlaybackProgress2.Location = new System.Drawing.Point(50, 90);
            this.PbPlaybackProgress2.Name = "PbPlaybackProgress2";
            this.PbPlaybackProgress2.Size = new System.Drawing.Size(518, 22);
            this.PbPlaybackProgress2.TabIndex = 11;
            // 
            // LabelPlayer2Muted
            // 
            this.LabelPlayer2Muted.AutoSize = true;
            this.LabelPlayer2Muted.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPlayer2Muted.Location = new System.Drawing.Point(155, 63);
            this.LabelPlayer2Muted.Name = "LabelPlayer2Muted";
            this.LabelPlayer2Muted.Size = new System.Drawing.Size(0, 16);
            this.LabelPlayer2Muted.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Muted Output:";
            // 
            // LabelPlayer2Filename
            // 
            this.LabelPlayer2Filename.AutoSize = true;
            this.LabelPlayer2Filename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPlayer2Filename.Location = new System.Drawing.Point(155, 27);
            this.LabelPlayer2Filename.Name = "LabelPlayer2Filename";
            this.LabelPlayer2Filename.Size = new System.Drawing.Size(0, 16);
            this.LabelPlayer2Filename.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Cart Name:";
            // 
            // PbVuRight2
            // 
            this.PbVuRight2.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.PbVuRight2.BackColor = System.Drawing.SystemColors.Control;
            this.PbVuRight2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PbVuRight2.Location = new System.Drawing.Point(25, 27);
            this.PbVuRight2.Name = "PbVuRight2";
            this.PbVuRight2.Size = new System.Drawing.Size(14, 85);
            this.PbVuRight2.TabIndex = 1;
            this.PbVuRight2.TabStop = false;
            // 
            // PbVuLeft2
            // 
            this.PbVuLeft2.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.PbVuLeft2.BackColor = System.Drawing.SystemColors.Control;
            this.PbVuLeft2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PbVuLeft2.Location = new System.Drawing.Point(9, 27);
            this.PbVuLeft2.Name = "PbVuLeft2";
            this.PbVuLeft2.Size = new System.Drawing.Size(14, 85);
            this.PbVuLeft2.TabIndex = 0;
            this.PbVuLeft2.TabStop = false;
            // 
            // lblPlaybackTimeLeft2
            // 
            this.lblPlaybackTimeLeft2.AutoSize = true;
            this.lblPlaybackTimeLeft2.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaybackTimeLeft2.Location = new System.Drawing.Point(362, 49);
            this.lblPlaybackTimeLeft2.Name = "lblPlaybackTimeLeft2";
            this.lblPlaybackTimeLeft2.Size = new System.Drawing.Size(218, 42);
            this.lblPlaybackTimeLeft2.TabIndex = 3;
            this.lblPlaybackTimeLeft2.Text = "-00:00:00.0";
            this.lblPlaybackTimeLeft2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.NotifyIconContext;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "Carrel Stream Assistant";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.Click += new System.EventHandler(this.NotifyIcon_Click);
            // 
            // NotifyIconContext
            // 
            this.NotifyIconContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreWindowToolStripMenuItem,
            this.exitApplicationStopServiceToolStripMenuItem});
            this.NotifyIconContext.Name = "NotifyIconContext";
            this.NotifyIconContext.Size = new System.Drawing.Size(161, 48);
            // 
            // restoreWindowToolStripMenuItem
            // 
            this.restoreWindowToolStripMenuItem.Name = "restoreWindowToolStripMenuItem";
            this.restoreWindowToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.restoreWindowToolStripMenuItem.Text = "Restore Window";
            this.restoreWindowToolStripMenuItem.Click += new System.EventHandler(this.RestoreWindowToolStripMenuItem_Click);
            // 
            // exitApplicationStopServiceToolStripMenuItem
            // 
            this.exitApplicationStopServiceToolStripMenuItem.Name = "exitApplicationStopServiceToolStripMenuItem";
            this.exitApplicationStopServiceToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitApplicationStopServiceToolStripMenuItem.Text = "Exit Application";
            this.exitApplicationStopServiceToolStripMenuItem.Click += new System.EventHandler(this.ExitApplicationStopServiceToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.lblR2RUpload);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.lblR2RMaxTime);
            this.groupBox3.Controls.Add(this.lblR2RFileName);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lblR2RTimeElapsed);
            this.groupBox3.Controls.Add(this.lblR2RStatus);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(870, 51);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(289, 533);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Reel-to-Reel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Status:";
            // 
            // lblR2RStatus
            // 
            this.lblR2RStatus.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblR2RStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblR2RStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblR2RStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblR2RStatus.Location = new System.Drawing.Point(17, 69);
            this.lblR2RStatus.Name = "lblR2RStatus";
            this.lblR2RStatus.Size = new System.Drawing.Size(206, 63);
            this.lblR2RStatus.TabIndex = 1;
            this.lblR2RStatus.Text = "Idle";
            this.lblR2RStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblR2RTimeElapsed
            // 
            this.lblR2RTimeElapsed.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblR2RTimeElapsed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblR2RTimeElapsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblR2RTimeElapsed.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lblR2RTimeElapsed.Location = new System.Drawing.Point(17, 201);
            this.lblR2RTimeElapsed.Name = "lblR2RTimeElapsed";
            this.lblR2RTimeElapsed.Size = new System.Drawing.Size(207, 58);
            this.lblR2RTimeElapsed.TabIndex = 2;
            this.lblR2RTimeElapsed.Text = "00:00:00";
            this.lblR2RTimeElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(185, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Recording Time Elapsed:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 401);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Output Filename:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // lblR2RFileName
            // 
            this.lblR2RFileName.AutoEllipsis = true;
            this.lblR2RFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblR2RFileName.ForeColor = System.Drawing.Color.DarkViolet;
            this.lblR2RFileName.Location = new System.Drawing.Point(14, 424);
            this.lblR2RFileName.Name = "lblR2RFileName";
            this.lblR2RFileName.Size = new System.Drawing.Size(203, 23);
            this.lblR2RFileName.TabIndex = 5;
            this.lblR2RFileName.Click += new System.EventHandler(this.label9_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(14, 290);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(193, 16);
            this.label10.TabIndex = 7;
            this.label10.Text = "Maximum Time Remaining:";
            // 
            // lblR2RMaxTime
            // 
            this.lblR2RMaxTime.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblR2RMaxTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblR2RMaxTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblR2RMaxTime.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lblR2RMaxTime.Location = new System.Drawing.Point(17, 311);
            this.lblR2RMaxTime.Name = "lblR2RMaxTime";
            this.lblR2RMaxTime.Size = new System.Drawing.Size(207, 58);
            this.lblR2RMaxTime.TabIndex = 6;
            this.lblR2RMaxTime.Text = "00:00:00";
            this.lblR2RMaxTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblR2RUpload
            // 
            this.lblR2RUpload.AutoEllipsis = true;
            this.lblR2RUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblR2RUpload.ForeColor = System.Drawing.Color.Black;
            this.lblR2RUpload.Location = new System.Drawing.Point(14, 491);
            this.lblR2RUpload.Name = "lblR2RUpload";
            this.lblR2RUpload.Size = new System.Drawing.Size(203, 20);
            this.lblR2RUpload.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(14, 468);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(150, 16);
            this.label13.TabIndex = 8;
            this.label13.Text = "Upload Reel to FTP:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(258, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(14, 482);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(242, 45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(14, 482);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(17, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 30);
            this.button1.TabIndex = 12;
            this.button1.Text = "Stop Recording";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 609);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BtnStopandClear);
            this.Controls.Add(this.btnClearQueue);
            this.Controls.Add(this.lblQueue);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.lstQueue);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnStopPlayback);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Carrel Stream Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing_1);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbVuRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbVuLeft)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbVuRight2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbVuLeft2)).EndInit();
            this.NotifyIconContext.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel TslblStatus;
        private System.Windows.Forms.Button BtnStopPlayback;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer VuMeterTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPlaybackTimeLeft;
        private System.Windows.Forms.Label LabelPlayer1Muted;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LabelPlayer1Filename;
        private System.Windows.Forms.PictureBox PbVuRight;
        private System.Windows.Forms.PictureBox PbVuLeft;
        private System.Windows.Forms.ListBox lstQueue;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Label lblQueue;
        private System.Windows.Forms.Button btnClearQueue;
        private System.Windows.Forms.Button BtnStopandClear;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label LabelPlayer2Muted;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LabelPlayer2Filename;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox PbVuRight2;
        private System.Windows.Forms.PictureBox PbVuLeft2;
        private System.Windows.Forms.Label lblPlaybackTimeLeft2;
        private System.Windows.Forms.ToolStripStatusLabel TslblMode;
        private System.Windows.Forms.ToolStripStatusLabel TslblStartStop;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip NotifyIconContext;
        private System.Windows.Forms.ToolStripMenuItem restoreWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitApplicationStopServiceToolStripMenuItem;
        private NewProgressBar PbPlaybackProgress;
        private NewProgressBar PbPlaybackProgress2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblR2RUpload;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblR2RMaxTime;
        private System.Windows.Forms.Label lblR2RFileName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblR2RTimeElapsed;
        private System.Windows.Forms.Label lblR2RStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
    }
}

