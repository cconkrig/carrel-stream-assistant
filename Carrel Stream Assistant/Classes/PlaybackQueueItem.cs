using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrel_Stream_Assistant
{
    class PlaybackQueueItem
    {
        public string FileName { get; set; }
        public string FullFileName { get; set; }
        public string SourceNetCueID { get; set; }
        public int RotationID { get; set; }
        public int Muted { get; set; }

        // Override ToString to provide custom display text
        public override string ToString()
        {
            return FileName; // Return the display text
        }
    }
}
