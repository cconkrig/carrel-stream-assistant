using System;

namespace Carrel_Stream_Assistant
{
    // Custom class to hold ListBox item information
    public class ReelItem
    {
        public int Id { get; set; }
        public int Format { get; set; }
        public string Filename { get; set; }
        public string StartCommand { get; set; }
        public string StopCommand { get; set; }
        public int MaxLengthSecs { get; set; }
        public int FTPServerId { get; set; }
        public string FTPPath { get; set; }

        // Override ToString to provide custom display text
        public override string ToString()
        {
            return Filename; // Return the display text
        }
    }
}
