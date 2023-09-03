using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Diagnostics;

namespace Carrel_Stream_Assistant
{
    public partial class MainForm : Form
    {
        public readonly static string databaseName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Cyber-Comp Technologies, LLC", "Carrel Stream Assistant", "stream_assist.db");
        private readonly static string connectionString = $"Data Source={databaseName};Version=3;";
        private readonly string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Cyber-Comp Technologies, LLC", "Carrel Stream Assistant", "log");
        public static MainForm Instance { get; private set; }
        private readonly UdpClient udpClient;
        #pragma warning disable IDE0044 // Disable "Make field readonly" suggestion
        private IPAddress serverIpAddress;
        private static IPEndPoint senderEndPoint;
        private readonly int maxLogEntries = 2000; // Maximum number of log entries in gui
        private WaveOutEvent waveOutDevice1;
        private WaveOutEvent waveOutDevice2;
        private AudioFileReader audioFileReader1;
        private AudioFileReader audioFileReader2;
        private CancellationTokenSource cancellationTokenSource1;
        private CancellationTokenSource cancellationTokenSource2;
        private SampleChannel sampleChannel1;
        private SampleChannel sampleChannel2;
        private bool isSampleChannel1Subscribed = false;
        private bool isSampleChannel2Subscribed = false;
        private System.Threading.Timer updateTimer;
        private System.Threading.Timer updateTimer2;
        private bool NetCueProcessingModeState = false;
        private MMDevice inputAudioDevice;
        private float audioFeedVolumeAtStartup = 0.0f;
        internal System.Windows.Forms.Timer recBlinkTimer;
        internal System.Windows.Forms.Timer recTimer;
        internal DateTime startTime; // Store the start time of the recording
        internal string currentReelToReelFilename;
        // Define a List to store the ReelToReel table records
        public List<ReelItem> ReelToReelTable { get; private set; } = new List<ReelItem>();
        private int fadeCounter = 0;
        private int fadeSteps = 10; // Number of steps for the fade

        public MainForm()
        {
            InitializeComponent();
            // Assign the instance to the static variable
            Instance = this;
            DatabaseOperations.CheckDatabase();
            PbPlaybackProgress.Maximum = 1000; // Set the maximum value (for percentage)
            PbPlaybackProgress2.Maximum = 1000; // Set the maximum value (for percentage)
            PbPlaybackProgress.Value = 0; // Set the initial value
            PbPlaybackProgress2.Value = 0; // Set the initial value
            PbPlaybackProgress.Style = ProgressBarStyle.Blocks;
            PbPlaybackProgress2.Style = ProgressBarStyle.Blocks;
            PbPlaybackProgress.MarqueeAnimationSpeed = 0;
            PbPlaybackProgress2.MarqueeAnimationSpeed = 0;
            PbPlaybackProgress.ForeColor = Color.MediumPurple; // Adjust the color as needed
            PbPlaybackProgress2.ForeColor = Color.MediumPurple; // Adjust the color as needed
            // Set the DrawMode of the ListBox to OwnerDrawFixed
            lstLog.DrawMode = DrawMode.OwnerDrawFixed;
            lstLog.DrawItem += LstLog_DrawItem;
            GeneralSettings settings = DatabaseOperations.GetGeneralSettingsFromDatabase();
            // Load the ReelToReel table from the database into memory
            DatabaseOperations.LoadReelToReelTableFromDatabase(ReelToReelTable, this);
            int netCuePort = settings.NetCuePort;
            string NetCueMode = settings.NetCueProcessingMode;
            serverIpAddress = GetLocalIPAddress();
            udpClient = new UdpClient(netCuePort);
            udpClient.BeginReceive(ReceiveCallback, null);
            AddLog($"Carrel Stream Assistant Started. Listening for NetCues on {serverIpAddress} UDP Port {netCuePort}...");
            // Get startup volume level so we know what to put it back to
            audioFeedVolumeAtStartup = GetAudioFeedVolume();
            float dB = ConvertLinearToDB(audioFeedVolumeAtStartup);
            AddLog($"Current Audio Feed Input Device volume is: {dB:F2} dB");
            TslblStatus.Text = $"Server: {serverIpAddress}:{netCuePort}";
            UpdateMode($"Mode: {NetCueMode}"); //make sure this comes last as we need the start-up line first.
            recBlinkTimer = new System.Windows.Forms.Timer();
            recBlinkTimer.Interval = 150; // 150 milliseconds blink frequency
            recBlinkTimer.Tick += RecBlinkTimer_Tick;
            recTimer = new System.Windows.Forms.Timer();
            recTimer.Interval = 100; // 1000 milliseconds update frequency
            recTimer.Tick += RecTimer_Tick;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Show();
        }

        private void RecBlinkTimer_Tick(object sender, EventArgs e)
        {
            fadeCounter++;
            if (fadeCounter <= fadeSteps)
            {
                Color intermediateColor = CalculateIntermediateColor(SystemColors.AppWorkspace, Color.Red, fadeCounter, fadeSteps);
                lblR2RStatus.ForeColor = intermediateColor;
            }
            else if (fadeCounter <= fadeSteps * 2) // Fading back to the default color
            {
                int reverseStep = fadeCounter - fadeSteps;
                Color intermediateColor = CalculateIntermediateColor(Color.Red, SystemColors.AppWorkspace, reverseStep, fadeSteps);
                lblR2RStatus.ForeColor = intermediateColor;
            }
            else
            {
                fadeCounter = 0; // Reset the fade counter for the next cycle
            }

        }

        private Color CalculateIntermediateColor(Color startColor, Color endColor, int step, int totalSteps)
        {
            int r = (int)((endColor.R - startColor.R) * ((float)step / totalSteps) + startColor.R);
            int g = (int)((endColor.G - startColor.G) * ((float)step / totalSteps) + startColor.G);
            int b = (int)((endColor.B - startColor.B) * ((float)step / totalSteps) + startColor.B);

            return Color.FromArgb(r, g, b);
        }

        private void RecTimer_Tick(object sender, EventArgs e)
        {
            // we inject the code from ReelToReelOperations
            // need to keep this blank
        }

        private float GetAudioFeedVolume()
        {
            string audioInputDeviceText = DatabaseOperations.GetSelectedAudioInputDeviceIdFromDatabase();
            var enumerator = new MMDeviceEnumerator();
            float currentVolume = 0.0f; // Default value if volume retrieval fails

            if (audioInputDeviceText != null && audioInputDeviceText != "")
            {
                foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
                {
                    if (device.FriendlyName == audioInputDeviceText)
                    {
                        string deviceID = device.ID;
                        var inputAudioDevice = enumerator.GetDevice(deviceID);
                        currentVolume = inputAudioDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
                        break; // Exit loop since we found the desired device
                    }
                }
            }
            else
            {
                // get the default system input
                MMDevice defaultInputDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
                currentVolume = defaultInputDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
            }

            return currentVolume;
        }

        // Expose a public method to update the ToolStripMode
        public void UpdateMode(string newText)
        {
            TslblMode.Text = newText;
            if(newText == "Mode: Always Process Incoming NetCues")
            {
                TslblStartStop.Visible = false;
                // Immediately set the requested Audio Feed Volume
                GeneralSettings settings = DatabaseOperations.GetGeneralSettingsFromDatabase();
                if (settings != null)
                {
                    if (settings.InputVolumeControl == 1)
                    {
                        float volume = float.Parse(settings.AudioFeedVolume);
                        float dB = ConvertLinearToDB(volume);
                        SetAudioFeedVolume(volume, dB);
                    } else
                    {
                        // Immediately set the requested Audio Feed Volume to startup value
                        float dB = ConvertLinearToDB(audioFeedVolumeAtStartup);
                        SetAudioFeedVolume(audioFeedVolumeAtStartup, dB); // return to normal volume when the software was started.
                    }
                }
            } else
            {
                TslblStartStop.Visible = true;
                TslblStartStop.Text = "Processing Disabled";
                TslblStartStop.ForeColor = Color.Red;
                NetCueProcessingModeState = false;

                // Immediately set the requested Audio Feed Volume to startup value
                float dB = ConvertLinearToDB(audioFeedVolumeAtStartup);
                SetAudioFeedVolume(audioFeedVolumeAtStartup, dB); // return to normal volume when the software was started.
            }
        }

        private float ConvertLinearToDB(float linearValue)
        {
            //To change a linear value into dB levels, we need to use:
            // dB = 20 * log10(linearValue)
            if (linearValue == 0)
                return 0f;
            if (linearValue < 0)
                return -60.0f; // Minimum possible dB value (e.g., muted)

            return (float)(20 * Math.Log10(linearValue));
        }

        private void SetAudioFeedVolume(float volume, float dB)
        {
            try
            {
                string audioInputDeviceText = DatabaseOperations.GetSelectedAudioInputDeviceIdFromDatabase();
                var enumerator = new MMDeviceEnumerator();
                if (audioInputDeviceText != null && audioInputDeviceText != "")
                {
                    foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
                    {
                        if (device.FriendlyName == audioInputDeviceText)
                        {
                            string deviceID = device.ID;
                            inputAudioDevice = enumerator.GetDevice(deviceID);
                            break; // Exit loop since we found the desired device
                        }
                    }
                }
                else
                {
                    // get the default system input
                    MMDevice defaultInputDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
                    audioInputDeviceText = defaultInputDevice.ID;
                    inputAudioDevice = enumerator.GetDevice(audioInputDeviceText);
                }
                inputAudioDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
                AddLog($"Set Audio Feed Input Volume to: {dB:F2} dB");
            }catch (Exception)
            {
                AddLog("Could not set volume, input device is missing. Please reset the input audio feed device in setup.");
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            senderEndPoint = new IPEndPoint(IPAddress.Any, 9963);
            byte[] receivedBytes = udpClient.EndReceive(ar, ref senderEndPoint);
            string receivedText = Encoding.ASCII.GetString(receivedBytes);

            // Update the listbox with the received text on the UI thread
            Invoke(new Action(() =>
            {
                string raw_command = receivedText;
                string[] startcommand_parts = receivedText.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
                if (startcommand_parts.Length > 1)
                {
                    receivedText = startcommand_parts[0]; //trim the filename off the dynamic cart so we can match on the command itself or the netcue.
                }

                // ============================================================================================
                // Check for Record Start Command - we process these regardless of what mode we are in.
                // Check if the receivedText matches StartCommand or StopCommand from the reelToReelTable Cache
                // ============================================================================================
                ReelItem matchingItem = ReelToReelTable.FirstOrDefault(item =>
                    item.StartCommand.Equals(receivedText, StringComparison.OrdinalIgnoreCase) ||
                    item.StopCommand.Equals(receivedText, StringComparison.OrdinalIgnoreCase));
                if (matchingItem != null)
                {
                    if (matchingItem.StartCommand == receivedText)
                    {
                        if (recTimer.Enabled)
                        {
                            AddLog($"Reel-to-Reel Already Recording... Cannot start another record. Received: {receivedText}", Color.DeepPink);
                            return;
                        }
                        else
                        {
                            // Call the function for start recording
                            // dispatch as a task so the code doesn't get blocked.
                            this.Invoke((Action)(async () => await ReelToReelOperations.StartReel(matchingItem, this, raw_command)));
                            return;
                        }
                    }
                    else if (matchingItem.StopCommand == receivedText)
                    {
                        if (!recTimer.Enabled)
                        {
                            AddLog($"Reel-to-Reel is not recording... Received: {receivedText}", Color.DeepPink);
                            return;
                        }
                        else
                        {
                            // Call the function for stop recording
                            AddLog($"Stopping Reel-to-Reel for: {currentReelToReelFilename} on Command: {matchingItem.StopCommand}");
                            ReelToReelOperations.StopReel(this);
                            return;
                        }
                    }
                }
                // ============================================================================================

                // Check if we are in Always Processing Mode or Switched Mode
                GeneralSettings settings = DatabaseOperations.GetGeneralSettingsFromDatabase();
                string NetCueMode = settings.NetCueProcessingMode;
                string StartNetCue = settings.NetCueStartCommand;
                string StopNetCue = settings.NetCueStopCommand;

                // Check for a NetCue and the processing mode we are in.
                switch (NetCueMode)
                {
                    case "Always Process Incoming NetCues":
                        ProcessNetCue(receivedText);
                        break;

                    case "Start/Stop Processing via Specific NetCues":
                        if (receivedText == StartNetCue)
                        {
                            NetCueProcessingModeState = true;
                            AddLog($"Received NetCue Start Command: \"{receivedText}\" from {senderEndPoint.Address}");
                            AddLog("NetCue processing is enabled! Listening for valid NetCues.");
                            TslblStartStop.Visible = true;
                            TslblStartStop.Text = "Processing Enabled";
                            TslblStartStop.ForeColor = Color.Green;
                            // Immediately set the requested Audio Feed Volume
                            settings = DatabaseOperations.GetGeneralSettingsFromDatabase();
                            if (settings != null)
                            {
                                float volume = float.Parse(settings.AudioFeedVolume);
                                float dB = ConvertLinearToDB(volume);
                                SetAudioFeedVolume(volume, dB);
                            }
                        }
                        else if (receivedText == StopNetCue)
                        {
                            NetCueProcessingModeState = false;
                            AddLog($"Received NetCue Stop Command: \"{receivedText}\" from {senderEndPoint.Address}");
                            AddLog("NetCue processing is disabled! No longer listening for valid NetCues.");
                            TslblStartStop.Visible = true;
                            TslblStartStop.Text = "Processing Disabled";
                            TslblStartStop.ForeColor = Color.Red;
                            // Immediately set the requested Audio Feed Volume
                            float dB = ConvertLinearToDB(audioFeedVolumeAtStartup);
                            SetAudioFeedVolume(audioFeedVolumeAtStartup, dB); // return to normal volume when the software was started.
                        }
                        else
                        {
                            if (NetCueProcessingModeState)
                            {
                                ProcessNetCue(receivedText);
                            }
                            else
                            {
                                AddLog($"*** Received NetCue: \"{receivedText}\" | Ignoring, processing is disabled.", Color.Gray, Color.White);
                            }
                        }
                        break;

                    default:
                        AddLog($"CANNOT DETERMINE NETCUE PROCESSING MODE!!! Fatal Error!", Color.Red, Color.White);
                        AddLog($"*** Received NetCue: {receivedText}", Color.Red);
                        AddLog($"CANNOT PROCESS NETCUE!!!", Color.Red);
                        break;
                }
            }));
            udpClient.BeginReceive(ReceiveCallback, null);
        }

        private void ProcessNetCue(string NetCueCode)
        {
            if (DatabaseOperations.IsNetCueValid(NetCueCode))
            {
                AddLog($"*** Received NetCue: {NetCueCode} from {senderEndPoint.Address}");
                // Call GetNetCue method to retrieve row data
                Dictionary<string, object> rowData = DatabaseOperations.GetNetCue(NetCueCode);

                if (rowData != null)
                {
                    // Extract specific values from the dictionary
                    int Id = Convert.ToInt32(rowData["Id"]);
                    string CartPath = Convert.ToString(rowData["CartPath"]);
                    int Muted = Convert.ToInt32(rowData["Muted"]);
                    string FriendlyName = Convert.ToString(rowData["FriendlyName"]);
                    if(FriendlyName != null && FriendlyName != "")
                    {
                        NetCueCode = FriendlyName + $" ({NetCueCode})";
                    }
                    Handle_NewPlayBack(CartPath, Id, NetCueCode, Muted);
                }
                else
                {
                    AddLog($"   !! No Carts in Rotation for NetCueCode \"{NetCueCode}\" !!", Color.Red, Color.White);
                }
            } else
            {
                AddLog($"*** Received NetCue: {NetCueCode} from {senderEndPoint.Address} - NetCue Not on File", Color.Gray, Color.White);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            udpClient.Close();
        }

        private IPAddress GetLocalIPAddress()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address;
            }
        }

        public void AddLog(string message, Color? foreColor = null, Color? backColor = null)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} :: {message}";
            if (lstLog.Items.Count >= maxLogEntries)
            {
                lstLog.Items.RemoveAt(0); // Remove the oldest item
            }
            lstLog.Items.Add(new ColoredListBoxItem(logEntry, foreColor ?? SystemColors.ControlText, backColor ?? SystemColors.Window));
            lstLog.TopIndex = lstLog.Items.Count - 1;
            ManageLogFiles(); // Call the method to manage log files
            string logFileName = $"log_{DateTime.Now:yyyyMMdd}.txt";
            string logFilePath = Path.Combine(logDirectory, logFileName);
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine(logEntry);
            }
        }

        private void ManageLogFiles()
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            string[] logFiles = Directory.GetFiles(logDirectory, "*.txt");
            DateTime now = DateTime.Now;
            DateTime currentDate = now.Date;
            DateTime oldestAllowedDate = currentDate.AddDays(-7);
            string currentlogFileName = $"log_{currentDate:yyyyMMdd}.txt";
            string currentlogFilePath = Path.Combine(logDirectory, currentlogFileName);

            foreach (string logFile in logFiles)
            {
                DateTime fileDate = File.GetCreationTime(logFile);
                if (fileDate < oldestAllowedDate)
                {
                    try
                    {
                        File.Delete(logFile);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        // Log the specific error message or exception details
                        string errorMessage = $"Failed to delete file: {logFile}. Error: {ex.Message}";
                        using (StreamWriter writer = File.AppendText(currentlogFilePath))
                        {
                            writer.WriteLine(errorMessage);
                        }
                    }
                }
            }

            if (!File.Exists(currentlogFilePath)) // Check if the file exists
            {
                using (StreamWriter writer = File.AppendText(currentlogFilePath))
                {
                    writer.WriteLine($"{now:yyyy-MM-dd HH:mm:ss} :: Log file created.");
                }
            }
        }

        private void ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup FormSetup = new FormSetup(this);
            FormSetup.ShowDialog();
        }

        private void Handle_NewPlayBack(string filename, int id, string netcuecode, int muted)
        {
            // check if player 1 is playing, if not, start it!
            if(waveOutDevice1 == null || waveOutDevice1.PlaybackState != PlaybackState.Playing)
            {
                if (File.Exists(filename))
                {
                    // Play the file
                    Player1_Play(filename, muted);
                    AddLog($"Aired \"{Path.GetFileName(filename)}\" via Audio Player 1 on NetCue: {netcuecode}", Color.Green, Color.White);
                }
                else
                {
                    AddLog($"!! CART NOT FOUND - FILE: {filename}  !!", Color.Red);
                }
            } 
            //check if player 2 is playing
            else if(waveOutDevice2 == null || waveOutDevice2.PlaybackState != PlaybackState.Playing)
            {
                if (File.Exists(filename))
                {
                    // Play the file
                    Player2_Play(filename, muted);
                    AddLog($"Aired \"{Path.GetFileName(filename)}\" via Audio Player 2 on NetCue: {netcuecode}", Color.Green, Color.White);
                }
                else
                {
                    AddLog($"!! CART NOT FOUND - FILE: {filename}  !!", Color.Red, Color.White);
                }
            } else
            {
                PlaybackQueueItem item = new PlaybackQueueItem
                {
                    FileName = Path.GetFileName(filename),
                    FullFileName = filename,
                    SourceNetCueID = netcuecode,
                    RotationID = id,
                    Muted = muted
                };
                lstQueue.Items.Add(item);
            }
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Update the Marker column for the current id (assuming it's the "id" column of the Rotations table) to 0
                string updateQuery = @"
                    UPDATE Rotations
                    SET Marker = 0
                    WHERE Id = @Id";
                using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Id", id);
                    updateCommand.ExecuteNonQuery();
                }
                // Get the next record's "id" for the same NetCueID
                int nextId;
                string nextIdQuery = @"
                    SELECT Id
                    FROM Rotations
                    WHERE NetCueID = (SELECT NetCueID FROM Rotations WHERE Id = @Id)
                    AND Id > @Id
                    ORDER BY Id ASC
                    LIMIT 1";
                using (SQLiteCommand nextIdCommand = new SQLiteCommand(nextIdQuery, connection))
                {
                    nextIdCommand.Parameters.AddWithValue("@Id", id);
                    object nextIdObj = nextIdCommand.ExecuteScalar();
                    nextId = nextIdObj != null ? Convert.ToInt32(nextIdObj) : -1;
                }
                // If no next "id" found, get the minimum "id" for the same NetCueID
                if (nextId == -1)
                {
                    string minIdQuery = @"
                        SELECT MIN(Id)
                        FROM Rotations
                        WHERE NetCueID = (SELECT NetCueID FROM Rotations WHERE Id = @Id)";
                    using (SQLiteCommand minIdCommand = new SQLiteCommand(minIdQuery, connection))
                    {
                        minIdCommand.Parameters.AddWithValue("@Id", id);
                        object minIdObj = minIdCommand.ExecuteScalar();
                        if (minIdObj != DBNull.Value) // Check if the value is not DBNull
                        {
                            nextId = Convert.ToInt32(minIdObj);
                        }
                        else
                        {
                            nextId = -1; // Handle the case where the value is DBNull
                        }
                    }
                }
                // Update the Marker column for the next "id" to 1
                string setNextIdQuery = @"
                    UPDATE Rotations
                    SET Marker = 1
                    WHERE Id = @NextId";
                using (SQLiteCommand setNextIdCommand = new SQLiteCommand(setNextIdQuery, connection))
                {
                    setNextIdCommand.Parameters.AddWithValue("@NextId", nextId);
                    setNextIdCommand.ExecuteNonQuery();
                }
            }
        }

        private void Player1_Play(string filename, int muted)
        {
            waveOutDevice1 = new WaveOutEvent();
            string audioInputDeviceText = DatabaseOperations.GetSelectedAudioInputDeviceIdFromDatabase();
            var enumerator = new MMDeviceEnumerator();
            if(audioInputDeviceText != null && audioInputDeviceText != "")
            {
                foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
                {
                    if (device.FriendlyName == audioInputDeviceText)
                    {
                        string deviceID = device.ID;
                        inputAudioDevice = enumerator.GetDevice(deviceID);
                        break; // Exit loop since we found the desired device
                    }
                }
            } else
            {
                // get the default system input
                MMDevice defaultInputDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
                audioInputDeviceText = defaultInputDevice.ID;
                inputAudioDevice = enumerator.GetDevice(audioInputDeviceText);
            }

            string audioFilePath = filename;
            LabelPlayer1Filename.Text = Path.GetFileName(audioFilePath);
            if (muted == 1)
            {
                LabelPlayer1Muted.Text = "YES";
                LabelPlayer1Muted.ForeColor = Color.Red;
                if (inputAudioDevice != null)
                {
                    inputAudioDevice.AudioEndpointVolume.Mute = true;
                }
            } else
            {
                LabelPlayer1Muted.Text = "NO";
                LabelPlayer1Muted.ForeColor = Color.Green;
                if (inputAudioDevice != null)
                {
                    inputAudioDevice.AudioEndpointVolume.Mute = false;
                }
            }

            if (waveOutDevice1 != null)
            {
                waveOutDevice1.Stop();
                waveOutDevice1.Dispose();
                waveOutDevice1 = null;
            }
            if (audioFileReader1 != null)
            {
                audioFileReader1.Dispose();
            }
            var waveOutDevices1 = WaveOut.DeviceCount;
            string desiredDeviceName = DatabaseOperations.GetSelectedAudioOutputDeviceIdFromDatabase(); // Replace with the desired device name
            int desiredDeviceNumber = -1;
            for (int deviceNumber = 0; deviceNumber < waveOutDevices1; deviceNumber++)
            {
                var capabilities = WaveOut.GetCapabilities(deviceNumber);
                if (capabilities.ProductName.IndexOf(desiredDeviceName, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    desiredDeviceNumber = deviceNumber;
                    break;
                }
            }
            if (desiredDeviceNumber != -1)
            {
                using (var waveOut = new WaveOutEvent())
                {
                    waveOutDevice1 = new WaveOutEvent
                    {
                        DeviceNumber = desiredDeviceNumber
                    };
                }
            }
            else
            {
                MessageBox.Show("Desired audio device not found.");
                return;
            }
            audioFileReader1 = new AudioFileReader(audioFilePath);
            // Create a SampleChannel to wrap the AudioFileReader
            sampleChannel1 = new SampleChannel(audioFileReader1);
            sampleChannel1.PreVolumeMeter += SampleChannel_PreVolumeMeter1;
            isSampleChannel1Subscribed = true;
            waveOutDevice1.PlaybackStopped += WaveOut_PlaybackStopped;
            waveOutDevice1.Init(sampleChannel1);
            waveOutDevice1.Play();
            cancellationTokenSource1 = new CancellationTokenSource();
            // Set up the timer to update the label every 100ms
            updateTimer = new System.Threading.Timer(UpdatePlaybackTime, null, 0, 100);
        }

        private void Player2_Play(string filename, int muted)
        {
            waveOutDevice2 = new WaveOutEvent();
            string audioInputDeviceText = DatabaseOperations.GetSelectedAudioInputDeviceIdFromDatabase();
            var enumerator = new MMDeviceEnumerator();
            if (audioInputDeviceText != null && audioInputDeviceText != "")
            {
                foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
                {
                    if (device.FriendlyName == audioInputDeviceText)
                    {
                        string deviceID = device.ID;
                        inputAudioDevice = enumerator.GetDevice(deviceID);
                        break; // Exit loop since we found the desired device
                    }
                }
            }
            else
            {
                // get the default system input
                MMDevice defaultInputDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
                audioInputDeviceText = defaultInputDevice.ID;
                inputAudioDevice = enumerator.GetDevice(audioInputDeviceText);
            }
            string audioFilePath = filename;
            LabelPlayer2Filename.Text = Path.GetFileName(audioFilePath);
            if (muted == 1)
            {
                LabelPlayer2Muted.Text = "YES";
                LabelPlayer2Muted.ForeColor = Color.Red;
                if (inputAudioDevice != null)
                {
                    inputAudioDevice.AudioEndpointVolume.Mute = true;
                }
            }
            else
            {
                LabelPlayer2Muted.Text = "NO";
                LabelPlayer2Muted.ForeColor = Color.Green;
                if (inputAudioDevice != null)
                {
                    inputAudioDevice.AudioEndpointVolume.Mute = false;
                }
            }
            if (waveOutDevice2 != null)
            {
                waveOutDevice2.Stop();
                waveOutDevice2.Dispose();
                waveOutDevice2 = null;
            }
            if (audioFileReader2 != null)
            {
                audioFileReader2.Dispose();
            }
            var waveOutDevices2 = WaveOut.DeviceCount;
            string desiredDeviceName = DatabaseOperations.GetSelectedAudioOutputDeviceIdFromDatabase(); // Replace with the desired device name
            int desiredDeviceNumber = -1;
            for (int deviceNumber = 0; deviceNumber < waveOutDevices2; deviceNumber++)
            {
                var capabilities = WaveOut.GetCapabilities(deviceNumber);
                if (capabilities.ProductName.IndexOf(desiredDeviceName, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    desiredDeviceNumber = deviceNumber;
                    break;
                }
            }
            if (desiredDeviceNumber != -1)
            {
                using (var waveOut = new WaveOutEvent())
                {
                    waveOutDevice2 = new WaveOutEvent
                    {
                        DeviceNumber = desiredDeviceNumber
                    };
                }
            }
            else
            {
                MessageBox.Show("Desired audio device not found.");
                return;
            }
            audioFileReader2 = new AudioFileReader(audioFilePath);
            // Create a SampleChannel to wrap the AudioFileReader
            sampleChannel2 = new SampleChannel(audioFileReader2);
            sampleChannel2.PreVolumeMeter += SampleChannel_PreVolumeMeter2;
            isSampleChannel2Subscribed = true;
            waveOutDevice2.PlaybackStopped += WaveOut_PlaybackStopped2;
            waveOutDevice2.Init(sampleChannel2);
            waveOutDevice2.Play();
            cancellationTokenSource2 = new CancellationTokenSource();
            // Set up the timer to update the label every 100ms
            updateTimer2 = new System.Threading.Timer(UpdatePlaybackTime2, null, 0, 100);
        }

        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // Handle playback stopped event here
            if (e.Exception != null)
            {
                // If there's an exception, handle it
                // e.Exception contains the exception that caused the stop
            }
            else
            {
                if(LabelPlayer1Muted.Text == "YES")
                {
                    inputAudioDevice.AudioEndpointVolume.Mute = false;
                }
                PbPlaybackProgress.Value = 0;
                lblPlaybackTimeLeft.Text = "-00:00:00.0";
                LabelPlayer1Filename.Text = "";
                LabelPlayer1Muted.Text = "";
                UpdatePlayer1VUMeters(0, 0);
                CheckQueue();
            }
        }

        private void WaveOut_PlaybackStopped2(object sender, StoppedEventArgs e)
        {
            // Handle playback stopped event here
            if (e.Exception != null)
            {
                // If there's an exception, handle it
                // e.Exception contains the exception that caused the stop
            }
            else
            {
                if (LabelPlayer2Muted.Text == "YES")
                {
                    inputAudioDevice.AudioEndpointVolume.Mute = false;
                }
                PbPlaybackProgress2.Value = 0;
                lblPlaybackTimeLeft2.Text = "-00:00:00.0";
                LabelPlayer2Filename.Text = "";
                LabelPlayer2Muted.Text = "";
                UpdatePlayer2VUMeters(0, 0); 
                CheckQueue();
            }
        }

        private void CheckQueue()
        {
            // Check if we have items queued for playback
            if(lstQueue.Items.Count > 0)
            {
                // Pick the first cart in the queue list.
                lstQueue.SelectedIndex = 0;
                // Log the message
                AddLog($"Started queued playback of {Path.GetFileName(lstQueue.SelectedItem.ToString())}");
                // Cast the item back to a PlaybackQueueItem
                PlaybackQueueItem selectedItem = (PlaybackQueueItem)lstQueue.SelectedItem;
                string fullFileName = selectedItem.FullFileName;
                string sourceNetCue = selectedItem.SourceNetCueID;
                int rotationId = selectedItem.RotationID;
                int muted = selectedItem.Muted;
                // Play the file
                Handle_NewPlayBack(fullFileName, rotationId, sourceNetCue, muted);
                // Remove the item from the queue
                if (lstQueue.SelectedItem != null)
                {
                    lstQueue.Items.Remove(lstQueue.SelectedItem);
                }
            }
        }

        private void SampleChannel_PreVolumeMeter1(object sender, StreamVolumeEventArgs e)
        {
            // Get the peak volume values for the left and right channels
            float leftVolume = e.MaxSampleValues[0];
            float rightVolume;
            if (e.MaxSampleValues.Length > 1)
            {
                rightVolume = e.MaxSampleValues[1];
            } else
            {
                //mono cart
                rightVolume = leftVolume;
            }
            // Update your UI or perform other logic with the volume values
            UpdatePlayer1VUMeters(leftVolume, rightVolume);
        }

        private void SampleChannel_PreVolumeMeter2(object sender, StreamVolumeEventArgs e)
        {
            // Get the peak volume values for the left and right channels
            float leftVolume = e.MaxSampleValues[0];
            float rightVolume;
            if (e.MaxSampleValues.Length > 1)
            {
                rightVolume  = e.MaxSampleValues[1];
            }
            else
            {
                //mono cart
                rightVolume  = leftVolume;
            }

            // Update your UI or perform other logic with the volume values
            UpdatePlayer2VUMeters(leftVolume, rightVolume);
        }

        private void UpdatePlayer1VUMeters(float leftVolume, float rightVolume)
        {
            // Calculate the interpolation values based on the left and right volumes
            float leftInterpolationValue = leftVolume;
            float rightInterpolationValue = rightVolume;
            // Define the start and end colors for the gradient
            Color startColor = Color.Green;
            Color middleColor = Color.Yellow;
            Color endColor = Color.Red;
            // Create a new LinearGradientBrush for left volume meter
            LinearGradientBrush leftBrush = new LinearGradientBrush(
                new Point(0, PbVuLeft.Height),
                new Point(0, 0),
                startColor,
                endColor
            );
            // Create a new LinearGradientBrush for right volume meter
            LinearGradientBrush rightBrush = new LinearGradientBrush(
                new Point(0, PbVuRight.Height),
                new Point(0, 0),
                startColor,
                endColor
            );
            // Set the PictureBox BackgroundImage property with the LinearGradientBrush
            PbVuLeft.BackgroundImage = CSAGraphics.CreateGradientImage(leftBrush, leftInterpolationValue, PbVuLeft.Width, PbVuLeft.Height, middleColor);
            PbVuLeft.Invalidate();
            PbVuRight.BackgroundImage = CSAGraphics.CreateGradientImage(rightBrush, rightInterpolationValue, PbVuRight.Width, PbVuRight.Height, middleColor);
            PbVuRight.Invalidate();
        }


        private void UpdatePlayer2VUMeters(float leftVolume, float rightVolume)
        {
            // Calculate the interpolation values based on the left and right volumes
            float leftInterpolationValue = leftVolume;
            float rightInterpolationValue = rightVolume;
            // Define the start and end colors for the gradient
            Color startColor = Color.Green;
            Color middleColor = Color.Yellow;
            Color endColor = Color.Red;
            // Create a new LinearGradientBrush for left volume meter
            LinearGradientBrush leftBrush = new LinearGradientBrush(
                new Point(0, PbVuLeft2.Height),
                new Point(0, 0),
                startColor,
                endColor
            );
            // Create a new LinearGradientBrush for right volume meter
            LinearGradientBrush rightBrush = new LinearGradientBrush(
                new Point(0, PbVuRight2.Height),
                new Point(0, 0),
                startColor,
                endColor
            );
            // Set the PictureBox BackgroundImage property with the LinearGradientBrush
            PbVuLeft2.BackgroundImage = CSAGraphics.CreateGradientImage(leftBrush, leftInterpolationValue, PbVuLeft2.Width, PbVuLeft2.Height, middleColor);
            PbVuLeft2.Invalidate();
            PbVuRight2.BackgroundImage = CSAGraphics.CreateGradientImage(rightBrush, rightInterpolationValue, PbVuRight2.Width, PbVuRight2.Height, middleColor);
            PbVuRight2.Invalidate();
        }

        public void UpdateRecVUMeters(float leftVolume, float rightVolume)
        {
            // Calculate the interpolation values based on the left and right volumes
            float leftInterpolationValue = leftVolume;
            float rightInterpolationValue = rightVolume;
            // Define the start and end colors for the gradient
            Color startColor = Color.Green;
            Color middleColor = Color.Yellow;
            Color endColor = Color.Red;
            // Create a new LinearGradientBrush for left volume meter
            LinearGradientBrush leftBrush = new LinearGradientBrush(
                new Point(0, vuRecLeft.Height),
                new Point(0, 0),
                startColor,
                endColor
            );
            // Create a new LinearGradientBrush for right volume meter
            LinearGradientBrush rightBrush = new LinearGradientBrush(
                new Point(0, vuRecRight.Height),
                new Point(0, 0),
                startColor,
                endColor
            );

            // Set the PictureBox BackgroundImage property with the LinearGradientBrush
            vuRecLeft.BackgroundImage = CSAGraphics.CreateGradientImage(leftBrush, leftInterpolationValue, vuRecLeft.Width, vuRecLeft.Height, middleColor);
            vuRecLeft.Invalidate();
            vuRecRight.BackgroundImage = CSAGraphics.CreateGradientImage(rightBrush, rightInterpolationValue, vuRecRight.Width, vuRecRight.Height, middleColor);
            vuRecRight.Invalidate();
        }

        private void UpdatePlaybackTime(object state)
        {
            try
            {
                if (waveOutDevice1 == null || waveOutDevice1.PlaybackState != PlaybackState.Playing)
                {
                    // Stop the timer when playback is stopped or waveOutDevice1 is null/disposed
                    if (updateTimer != null)
                    {
                        updateTimer.Dispose();
                    }
                    PbPlaybackProgress.Value = 0;
                    return;
                }
                TimeSpan currentTime = audioFileReader1.CurrentTime;
                TimeSpan totalTime = audioFileReader1.TotalTime;
                TimeSpan timeLeft = totalTime - currentTime;
                // Update the UI using Invoke
                this.Invoke((MethodInvoker)(() =>
                {
                    if (audioFileReader1 != null)
                    {
                        currentTime = audioFileReader1.CurrentTime;
                        totalTime = audioFileReader1.TotalTime;
                        timeLeft = totalTime - currentTime;
                        double progressPercentage = (currentTime.TotalMilliseconds / totalTime.TotalMilliseconds) * 1000;
                        PbPlaybackProgress.Value = (int)progressPercentage;
                        lblPlaybackTimeLeft.Text = $"-{timeLeft:hh\\:mm\\:ss\\.f}";
                        if (waveOutDevice1 == null || waveOutDevice1.PlaybackState != PlaybackState.Playing)
                        {
                            PbPlaybackProgress.Value = 0;
                        }
                    } else
                    {
                        PbPlaybackProgress.Value = 0;
                    }
                }));
            }
            catch (ObjectDisposedException)
            {
                // The form has been disposed, exit gracefully
                Application.Exit();
            }
        }
        private void UpdatePlaybackTime2(object state)
        {
            try
            {
                if (waveOutDevice2 == null || waveOutDevice2.PlaybackState != PlaybackState.Playing)
                {
                    // Stop the timer when playback is stopped or waveOutDevice1 is null/disposed
                    if (updateTimer2 != null)
                    {
                        updateTimer2.Dispose();
                    }
                    PbPlaybackProgress2.Value = 0;
                    return;
                }
                TimeSpan currentTime = audioFileReader2.CurrentTime;
                TimeSpan totalTime = audioFileReader2.TotalTime;
                TimeSpan timeLeft = totalTime - currentTime;
                // Update the UI using Invoke
                this.Invoke((MethodInvoker)(() =>
                {
                    if (audioFileReader1 != null)
                    {
                        currentTime = audioFileReader2.CurrentTime;
                        totalTime = audioFileReader2.TotalTime;
                        timeLeft = totalTime - currentTime;
                        double progressPercentage = (currentTime.TotalMilliseconds / totalTime.TotalMilliseconds) * 1000;
                        PbPlaybackProgress2.Value = (int)progressPercentage;
                        lblPlaybackTimeLeft2.Text = $"-{timeLeft:hh\\:mm\\:ss\\.f}";
                    } else
                    {
                        PbPlaybackProgress2.Value = 0;
                    }
                }));
            }
            catch (ObjectDisposedException)
            {
                // The form has been disposed, exit gracefully
                Application.Exit();
            }
        }

        private void BtnStopPlayback_Click(object sender, EventArgs e)
        {
            AddLog("STOPPED ALL CURRENT AUDIO PLAYBACK!", Color.DarkRed);
            // Unsubscribing from SampleChannel.PreVolumeMeter
            if (isSampleChannel1Subscribed)
            {
                AddLog($"---- Stopped Player 1 ({LabelPlayer1Filename.Text})", Color.DarkRed);
                sampleChannel1.PreVolumeMeter -= SampleChannel_PreVolumeMeter1;
                isSampleChannel1Subscribed = false;
            }
            if (isSampleChannel2Subscribed)
            {
                AddLog($"---- Stopped Player 2 ({LabelPlayer2Filename.Text})", Color.DarkRed);
                sampleChannel2.PreVolumeMeter -= SampleChannel_PreVolumeMeter2;
                isSampleChannel2Subscribed = false;
            }
            UpdatePlayer1VUMeters(0, 0);
            UpdatePlayer2VUMeters(0, 0);
            lblPlaybackTimeLeft.Text = "-00:00:00.0";
            lblPlaybackTimeLeft2.Text = "-00:00:00.0";
            LabelPlayer1Filename.Text = "";
            LabelPlayer2Filename.Text = "";
            LabelPlayer1Muted.Text = "";
            LabelPlayer2Muted.Text = "";
            PbPlaybackProgress.Value = 0;
            PbPlaybackProgress2.Value = 0;
            if (cancellationTokenSource1 != null)
            {
                cancellationTokenSource1.Cancel();
            }
            if (cancellationTokenSource2 != null)
            {
                cancellationTokenSource2.Cancel();
            }
            if (waveOutDevice1 != null)
            {
                waveOutDevice1.Stop();
                waveOutDevice1.Dispose();
                waveOutDevice1 = null;
            }
            if (waveOutDevice2 != null)
            {
                waveOutDevice2.Stop();
                waveOutDevice2.Dispose();
                waveOutDevice2 = null;
            }
            if (audioFileReader1 != null)
            {
                audioFileReader1.Dispose();
                audioFileReader1 = null;
            }
            if (audioFileReader2 != null)
            {
                audioFileReader2.Dispose();
                audioFileReader2 = null;
            }
            if (updateTimer != null)
            {
                updateTimer.Dispose();
                updateTimer = null;
            }
            if (updateTimer2 != null)
            {
                updateTimer2.Dispose();
                updateTimer2 = null;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Stop and dispose of the timer
            if (updateTimer != null)
            {
                updateTimer.Dispose();
                updateTimer = null;
            }
            // Dispose of audio resources
            if (waveOutDevice1 != null)
            {
                waveOutDevice1.Stop();
                waveOutDevice1.Dispose();
                waveOutDevice1 = null;
            }
            if (audioFileReader1 != null)
            {
                audioFileReader1.Dispose();
                audioFileReader1 = null;
            }
            base.OnFormClosing(e);
        }

        private void BtnClearQueue_Click(object sender, EventArgs e)
        {
            AddLog("Playback Queue Cleared.", Color.PaleVioletRed);
            lstQueue.Items.Clear();
        }

        private void BtnStopandClear_Click(object sender, EventArgs e)
        {
            BtnClearQueue_Click(sender, e);
            BtnStopPlayback_Click(sender, e);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox AboutBox = new AboutBox();
            AboutBox.ShowDialog();
        }

        private void MainForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Cancel the default close behavior
                this.Hide();      // Hide the main form
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); // Hide the main form
            }
        }


        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ExitApplicationStopServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Properly dispose of the NotifyIcon when you're done with it
            //notifyIcon.Dispose();
            Application.Exit();
        }

        private void RestoreWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void LstLog_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Determine the background and foreground colors for the item
            Color bgColor = Color.White;
            Color fgColor = Color.Black;

            // Check if this item has a custom color set
            if (e.Index < lstLog.Items.Count && lstLog.Items[e.Index] is ColoredListBoxItem coloredItem)
            {
                fgColor = coloredItem.ForeColor;
            }

            // Draw the background
            e.Graphics.FillRectangle(new SolidBrush(bgColor), e.Bounds);

            // Draw the item's text with the custom foreground color
            e.Graphics.DrawString(lstLog.Items[e.Index].ToString(), e.Font, new SolidBrush(fgColor), e.Bounds, StringFormat.GenericDefault);

            e.DrawFocusRectangle();
        }

        private void BtnEmergencyStopRec_Click(object sender, EventArgs e)
        {
            ReelToReelOperations.StopReel(this, "Emergency");
        }

    }
}
