
namespace Carrel_Stream_Assistant
{
    partial class FormReelToReel
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSaveReel = new System.Windows.Forms.Button();
            this.btnReelCancel = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.txtStartCommand = new System.Windows.Forms.TextBox();
            this.txtStopCommand = new System.Windows.Forms.TextBox();
            this.txtMaxLengthSecs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboRecFormat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path w/Filename:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Start Command:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Stop Command:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Max Length (Seconds):";
            // 
            // btnSaveReel
            // 
            this.btnSaveReel.Location = new System.Drawing.Point(243, 302);
            this.btnSaveReel.Name = "btnSaveReel";
            this.btnSaveReel.Size = new System.Drawing.Size(79, 34);
            this.btnSaveReel.TabIndex = 4;
            this.btnSaveReel.Text = "Save";
            this.btnSaveReel.UseVisualStyleBackColor = true;
            this.btnSaveReel.Click += new System.EventHandler(this.BtnSaveReel_Click);
            // 
            // btnReelCancel
            // 
            this.btnReelCancel.Location = new System.Drawing.Point(342, 302);
            this.btnReelCancel.Name = "btnReelCancel";
            this.btnReelCancel.Size = new System.Drawing.Size(79, 34);
            this.btnReelCancel.TabIndex = 5;
            this.btnReelCancel.Text = "Cancel";
            this.btnReelCancel.UseVisualStyleBackColor = true;
            this.btnReelCancel.Click += new System.EventHandler(this.BtnReelCancel_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilename.Location = new System.Drawing.Point(166, 80);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(255, 22);
            this.txtFilename.TabIndex = 6;
            this.txtFilename.Text = "C:\\";
            // 
            // txtStartCommand
            // 
            this.txtStartCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartCommand.Location = new System.Drawing.Point(166, 185);
            this.txtStartCommand.Name = "txtStartCommand";
            this.txtStartCommand.Size = new System.Drawing.Size(255, 22);
            this.txtStartCommand.TabIndex = 7;
            // 
            // txtStopCommand
            // 
            this.txtStopCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStopCommand.Location = new System.Drawing.Point(166, 221);
            this.txtStopCommand.Name = "txtStopCommand";
            this.txtStopCommand.Size = new System.Drawing.Size(255, 22);
            this.txtStopCommand.TabIndex = 8;
            // 
            // txtMaxLengthSecs
            // 
            this.txtMaxLengthSecs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxLengthSecs.Location = new System.Drawing.Point(271, 255);
            this.txtMaxLengthSecs.Name = "txtMaxLengthSecs";
            this.txtMaxLengthSecs.Size = new System.Drawing.Size(150, 22);
            this.txtMaxLengthSecs.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(404, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "{cartname} = Dynamic cart name, follows StartCommand + ** (eg. startrec**ballgame" +
    ")";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Filename Macros:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "{date} = yyyy-mm-dd";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "{time} = HH:mm:ss";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(334, 102);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "*no file extension";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(17, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "Record Format:";
            // 
            // cboRecFormat
            // 
            this.cboRecFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRecFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRecFormat.FormattingEnabled = true;
            this.cboRecFormat.Items.AddRange(new object[] {
            "HE-AAC, 44100Hz, Stereo, 30Kbps (DEFAULT)",
            "HE-AAC v2, 44100Hz, Stereo, 30Kbps",
            "MP3, 44100Hz, Stereo, 128Kbps",
            "WAV, 44100Hz, Stereo, 16-bit"});
            this.cboRecFormat.Location = new System.Drawing.Point(118, 32);
            this.cboRecFormat.Name = "cboRecFormat";
            this.cboRecFormat.Size = new System.Drawing.Size(303, 24);
            this.cboRecFormat.TabIndex = 16;
            // 
            // FormReelToReel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 352);
            this.Controls.Add(this.cboRecFormat);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMaxLengthSecs);
            this.Controls.Add(this.txtStopCommand);
            this.Controls.Add(this.txtStartCommand);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.btnReelCancel);
            this.Controls.Add(this.btnSaveReel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReelToReel";
            this.Text = "Recording Setup";
            this.Load += new System.EventHandler(this.FormReelToReel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSaveReel;
        private System.Windows.Forms.Button btnReelCancel;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.TextBox txtStartCommand;
        private System.Windows.Forms.TextBox txtStopCommand;
        private System.Windows.Forms.TextBox txtMaxLengthSecs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboRecFormat;
    }
}