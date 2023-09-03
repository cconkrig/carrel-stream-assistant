
namespace Carrel_Stream_Assistant
{
    partial class FormFTPOutput
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
            this.ftpTerminal = new System.Windows.Forms.RichTextBox();
            this.progressUpload = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // ftpTerminal
            // 
            this.ftpTerminal.BackColor = System.Drawing.Color.Black;
            this.ftpTerminal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ftpTerminal.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ftpTerminal.ForeColor = System.Drawing.Color.White;
            this.ftpTerminal.Location = new System.Drawing.Point(17, 30);
            this.ftpTerminal.Name = "ftpTerminal";
            this.ftpTerminal.ReadOnly = true;
            this.ftpTerminal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ftpTerminal.Size = new System.Drawing.Size(652, 290);
            this.ftpTerminal.TabIndex = 0;
            this.ftpTerminal.Text = "";
            // 
            // progressUpload
            // 
            this.progressUpload.Location = new System.Drawing.Point(17, 12);
            this.progressUpload.Name = "progressUpload";
            this.progressUpload.Size = new System.Drawing.Size(652, 12);
            this.progressUpload.TabIndex = 1;
            this.progressUpload.Visible = false;
            // 
            // FormFTPOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(686, 332);
            this.Controls.Add(this.progressUpload);
            this.Controls.Add(this.ftpTerminal);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFTPOutput";
            this.Text = "FTP Terminal Output";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFTPOutput_FormClosing);
            this.Load += new System.EventHandler(this.FormFTPOutput_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox ftpTerminal;
        internal System.Windows.Forms.ProgressBar progressUpload;
    }
}