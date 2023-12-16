using System;

namespace Carrel_Stream_Assistant
{
    // Custom class to hold ListBox item information
    public class RecQueueItem
    {
        public int Id { get; set; }
        public ReelItem ReelItem { get; set; }
        public string OutputFilename { get; set; }
        public string Action { get; set; }
        public string ActionText { get; set; }
        
    }
}
