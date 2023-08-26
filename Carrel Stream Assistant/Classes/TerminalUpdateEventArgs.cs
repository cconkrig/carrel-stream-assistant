using System;
using System.Drawing;

namespace Carrel_Stream_Assistant
{
    public class TerminalUpdateEventArgs : EventArgs
    {
        public string TerminalLine { get; private set; }
        public Color Forecolor { get; private set; }

        public TerminalUpdateEventArgs(string terminalLine, Color forecolor)
        {
            TerminalLine = terminalLine;
            Forecolor = forecolor;
        }
    }
}
