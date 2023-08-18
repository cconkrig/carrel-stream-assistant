using System;
using System.IO;
using System.Windows.Forms;

namespace Carrel_Stream_Assistant
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Subsribe to the unhandled exception event
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                // Log the uncaught exception
                string logFileName = $"crashlog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFileName);
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine($"Date and Time: {DateTime.Now}");
                    writer.WriteLine($"Exception Type: {exception.GetType()}");
                    writer.WriteLine($"Message: {exception.Message}");
                    writer.WriteLine($"Stack Trace:\n{exception.StackTrace}");
                    writer.WriteLine(new string('-', 40));
                }
            }
        }
    }
}
