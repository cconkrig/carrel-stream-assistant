using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Carrel_Stream_Assistant
{
    static class Program
    {
        private static ManualResetEvent initializationComplete = new ManualResetEvent(false);
        private static readonly string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Cyber-Comp Technologies, LLC", "Carrel Stream Assistant", "log");

        [STAThread]
        static void Main()
        {
            // Subscribe to the unhandled exception event
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create and show the splash screen
            //FormSplash formSplash = new FormSplash();
            //formSplash.Show();

            // Subscribe to the FormSplash's initialization completion event
            //formSplash.InitializationComplete += (sender, e) =>
            //{
            // Set the ManualResetEvent to signal that initialization is complete
            //    initializationComplete.Set();

            // Open MainForm when initialization is complete
            //    OpenMainForm(formSplash.progressBar.Value);
            //};

            // Create a separate thread for initialization
            //Thread initializationThread = new Thread(() =>
            //{
            // Perform initialization tasks in FormSplash
            //    formSplash.Initialize();

            // Signal that initialization is complete
            //    formSplash.OnInitializationComplete();
            //});
            //initializationThread.Start();

            // Wait until initialization is complete or a timeout occurs
            //if (initializationComplete.WaitOne(TimeSpan.FromSeconds(10))) // Adjust the timeout as needed
            //{
            // MainForm should open after initialization is complete
            //}
            //else
            //{
            // Handle the case where initialization takes too long
            // You can show an error message or take appropriate action
            //    MessageBox.Show("Initialization took too long.");
            //}
            Application.Run(new MainForm());
        }

        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                // Log the uncaught exception
                string logFileName = $"crashlog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string logFilePath = Path.Combine(logDirectory, logFileName);

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

        static void OpenMainForm(int progressBarValue)
        {
            if (progressBarValue >= 90)
            {
                // Start the main application by opening MainForm
                Application.Run(new MainForm());
            }
        }
    }
}
