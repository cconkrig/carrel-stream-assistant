namespace Carrel_Stream_Assistant
{
    // Custom class to hold ListBox item information
    public class FTPServerItem
    {
        public int Id { get; set; }
        public string HostName { get; set; }
        public int SecurityMode { get; set; }
        public int TransferMode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        // Override ToString to provide custom display text
        public override string ToString()
        {
            return Username + "@" + HostName; // Return the display text
        }
    }
}
