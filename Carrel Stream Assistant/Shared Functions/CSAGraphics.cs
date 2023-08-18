using System.Drawing;
using System.Drawing.Drawing2D;


namespace Carrel_Stream_Assistant
{
    class CSAGraphics
    {
        public static Image CreateGradientImage(LinearGradientBrush brush, float interpolationValue, int width, int height, Color middleColor)
        {
            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Set the Graphics object's clipping region to the size of the PictureBox
                graphics.SetClip(new Rectangle(0, 0, width, height));

                // Calculate the height based on the interpolation value
                int gradientHeight = (int)(height * interpolationValue);

                // Calculate the threshold for displaying red
                int redThreshold = (int)(height * 0.9);

                // Create a blend of colors for the gradient
                Color[] colors = { brush.LinearColors[0], middleColor, brush.LinearColors[1] };
                float[] positions = { 0f, (float)redThreshold / height, 1f };

                // Create a new ColorBlend object for the gradient
                ColorBlend colorBlend = new ColorBlend
                {
                    Colors = colors,
                    Positions = positions
                };

                // Set the InterpolationColors property of the brush
                brush.InterpolationColors = colorBlend;

                // Fill the Graphics object with the LinearGradientBrush
                graphics.FillRectangle(brush, new Rectangle(0, height - gradientHeight, width, gradientHeight));
            }

            return bitmap;
        }
    }
}
