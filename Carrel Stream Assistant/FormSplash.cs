using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Carrel_Stream_Assistant
{
    public partial class FormSplash : Form
    {
        private BackgroundWorker worker;

        public event EventHandler InitializationComplete;

        public FormSplash()
        {
            InitializeComponent();
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {
            // Initialize the BackgroundWorker
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            // Start the background worker
            worker.RunWorkerAsync();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblStatus.Text = e.UserState.ToString();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int totalSteps = 100;

            for (int currentStep = 0; currentStep < totalSteps; currentStep++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                worker.ReportProgress(currentStep, GetStatusText(currentStep));
                Thread.Sleep(100);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                OnInitializationComplete();
            }
        }
        public void Initialize()
        {
            // You can add initialization logic here
        }
        private string GetStatusText(int currentStep)
        {
            if (currentStep <= 20)
            {
                return "Checking database indices...";
            }
            else if (currentStep > 20 && currentStep <= 90)
            {
                return "Working...";
            }
            else
            {
                InitializationComplete?.Invoke(this, EventArgs.Empty);
                return "Loading system settings...";
            }
        }

        public void OnInitializationComplete()
        {
            // Raise the InitializationComplete event
            InitializationComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}
