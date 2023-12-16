using System.Drawing;

namespace Carrel_Stream_Assistant
{
    // Custom class to hold log message and colors
    public class ColoredListBoxItem
    {
        public string Text { get; }
        public Color ForeColor { get; }
        public Color BackColor { get; }

        public ColoredListBoxItem(string text, Color foreColor, Color backColor)
        {
            Text = text;
            ForeColor = foreColor;
            BackColor = backColor;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
