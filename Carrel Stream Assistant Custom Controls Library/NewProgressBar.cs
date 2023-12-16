using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Carrel_Stream_Assistant
{
    [System.ComponentModel.ToolboxItem(true)]
    public class NewProgressBar : ProgressBar
    {
        public NewProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // None... Helps control the flicker.
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            const int inset = 0; // A single inset value to control the sizing of the inner rect.
            using (var offscreenImage = new Bitmap(Width, Height))
            using (var offscreen = Graphics.FromImage(offscreenImage))
            {
                var rect = new Rectangle(0, 0, Width, Height);
                if (ProgressBarRenderer.IsSupported)
                    ProgressBarRenderer.DrawHorizontalBar(offscreen, rect);
                rect.Inflate(new Size(-inset, -inset)); // Deflate inner rect.
                rect.Width = (int)(rect.Width * ((double)Value / Maximum));
                if (rect.Width == 0)
                {
                    rect.Width = 1; // Can't draw rec with width of 0.
                    using (var brush = new SolidBrush(ForeColor)) // Use a SolidBrush with the desired color
                    {
                        offscreen.FillRectangle(brush, inset, inset, rect.Width, rect.Height);
                    }
                }
                else
                {
                    using (var brush = new SolidBrush(ForeColor)) // Use a SolidBrush with the desired color
                    {
                        offscreen.FillRectangle(brush, inset, inset, rect.Width, rect.Height);
                    }
                }
                e.Graphics.DrawImage(offscreenImage, 0, 0);
            }
        }
    }
}
