using System;

namespace Carrel_Stream_Assistant
{
    // Custom class to hold ListBox item information
    public class NetCueItem
    {
        public string Text { get; set; }
        public string NetCue { get; set; }
        public string FriendlyName { get; set; }
        public int Id { get; set; }

        // Override ToString to provide custom display text
        public override string ToString()
        {
            return Text; // Return the display text
        }
    }
}
