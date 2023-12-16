using System;

namespace Carrel_Stream_Assistant
{
    // Custom class to hold ListBox item information
    public class RotationItem
    {
        public int Id { get; set; }
        public int NetCueID { get; set; }
        public int SortOrder { get; set; }
        public int Marker { get; set; }
        public string CartName { get; set; }
        public string Path { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        // Override ToString to provide custom display text
        public override string ToString()
        {
            return CartName; // Return the display text
        }
    }
}
