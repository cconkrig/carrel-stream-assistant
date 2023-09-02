using NAudio.Wave;
using NAudio.CoreAudioApi;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Carrel_Stream_Assistant
{
    public static class ReelToReelOperations
    {
        private static CancellationTokenSource recordingCancellation;

        public static async Task StartReel(ReelItem reelItem, MainForm mainForm, string receivedText)
        {
            recordingCancellation = new CancellationTokenSource();
            await RecordWavAsync(reelItem, recordingCancellation.Token, mainForm, receivedText);
        }

        public static string GetAudioFileNameFromReel(ReelItem reelItem, string receivedText)
        {
            string filename;
            string[] startcommand_parts = receivedText.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
            string filename_in_processing = reelItem.Filename;

            //process dynamic filename
            if (startcommand_parts.Length > 1) {
                filename_in_processing = filename_in_processing.Replace("{cartname}", startcommand_parts[1]);
            }

            //get now so we can process the date and time replacements
            DateTime date = DateTime.Now;

            //process date in filename
            string formattedDate = date.ToString("yyyyMMdd");
            filename_in_processing = filename_in_processing.Replace("{date}", formattedDate);

            //process time in filename
            string formattedTime = date.ToString("HHmm");
            filename_in_processing = filename_in_processing.Replace("{time}", formattedTime);

            switch (reelItem.Format)
            {
                case 1:
                    filename_in_processing = filename_in_processing + ".mp3";
                    break;
                case 2:
                    filename_in_processing = filename_in_processing + ".wav";
                    break;
                default:
                    filename_in_processing = filename_in_processing + ".m4a";
                    break;
            }
            //clean up the filename to remove any bad/invalid filenames
            string directoryPath = Path.GetDirectoryName(filename_in_processing);
            string raw_filename = Path.GetFileName(filename_in_processing);
            char[] invalidChars = Path.GetInvalidFileNameChars();
            string cleanedFileName = new string(raw_filename.Where(c => !invalidChars.Contains(c)).ToArray());
            filename = Path.Combine(directoryPath, cleanedFileName);
            return filename;
        }

        public static void StopReel(MainForm mainForm, string stopType = "Normal")
        {
            if (stopType == "Emergency")
            {
                mainForm.AddLog("EMERGENCY STOP RECORD PRESSED BY USER!", Color.Red);
                mainForm.btnEmergencyStopRec.Visible = false;
            }
            recordingCancellation?.Cancel();
            TurnRecordingStatusOff(mainForm);
        }

        private static void TurnRecordingStatusOn(MainForm mainForm, int maxLengthSecs)
        {
            mainForm.Invoke((MethodInvoker)(() =>
            {
                mainForm.startTime = DateTime.Now;
                mainForm.lblR2RStatus.Text = "Recording...";
                mainForm.lblR2RStatus.ForeColor = SystemColors.AppWorkspace;
                mainForm.recBlinkTimer.Start(); // Start blinking
                mainForm.recTimer.Start(); // Start counting
                mainForm.lblR2RTimeElapsed.ForeColor = Color.Black;
                mainForm.lblR2RMaxTime.ForeColor = Color.Black;
                mainForm.btnEmergencyStopRec.Visible = true;

                mainForm.recTimer.Tick += (sender, e) =>
                {
                    TimeSpan elapsed = DateTime.Now - mainForm.startTime;
                    mainForm.lblR2RTimeElapsed.Text = elapsed.ToString("hh\\:mm\\:ss");

                    TimeSpan remaining = TimeSpan.FromSeconds(maxLengthSecs + 1) - elapsed;
                    mainForm.lblR2RMaxTime.Text = remaining.ToString("hh\\:mm\\:ss");
                    if(remaining.TotalSeconds < 5)
                    {
                        mainForm.lblR2RMaxTime.ForeColor = Color.Red;
                    }
                };
                mainForm.pbReelToReel.Visible = true;
                mainForm.vuRecLeft.Visible = true;
                mainForm.vuRecRight.Visible = true;


            }));
        }

        private static void TurnRecordingStatusOff(MainForm mainForm)
        {
            mainForm.Invoke((MethodInvoker)(() =>
            {
                mainForm.lblR2RStatus.Text = "Idle.";
                mainForm.lblR2RStatus.ForeColor = Color.DimGray;
                mainForm.recBlinkTimer.Stop(); // Stop blinking
                mainForm.lblR2RStatus.Visible = true; //just to make sure it's visibile
                mainForm.recTimer.Stop(); // Start counting
                mainForm.lblR2RTimeElapsed.ForeColor = SystemColors.ScrollBar;
                mainForm.lblR2RTimeElapsed.Text = "00:00:00";
                mainForm.lblR2RMaxTime.ForeColor = SystemColors.ScrollBar;
                mainForm.lblR2RMaxTime.Text = "00:00:00";
                mainForm.lblR2RFileName.Text = "";
                mainForm.vuRecLeft.Visible = false;
                mainForm.vuRecRight.Visible = false;
                mainForm.pbReelToReel.Visible = false;
                mainForm.btnEmergencyStopRec.Visible = false;
            }));
        }

        private static async Task RecordWavAsync(ReelItem reelItem, CancellationToken cancellationToken, MainForm mainForm, string receivedText)
        {
            try
            {
                WaveFormat waveFormat = new WaveFormat(44100, 16, 2);
                string desiredDeviceName = ""; // Assume desiredDeviceName is a string

                string audioInputDeviceText = DatabaseOperations.GetSelectedAudioInputDeviceIdFromDatabase();
                var enumerator = new MMDeviceEnumerator();

                if (audioInputDeviceText != null && audioInputDeviceText != "")
                {
                    foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
                    {
                        if (device.FriendlyName == audioInputDeviceText)
                        {
                            desiredDeviceName = device.FriendlyName;
                            break; // Exit loop since we found the desired device
                        }
                    }
                }

                using (WaveInEvent waveInEvent = new WaveInEvent())
                {
                    int deviceNumber = -1; // Initialize deviceNumber as -1

                    // Truncate the desiredDeviceName to 31 characters
                    string truncatedDesiredDeviceName = desiredDeviceName.Length > 30
                        ? desiredDeviceName.Substring(0, 31).Trim()
                        : desiredDeviceName;

                    for (int i = 0; i < WaveInEvent.DeviceCount; i++)
                    {
                        if (WaveInEvent.GetCapabilities(i).ProductName.Trim() == truncatedDesiredDeviceName)
                        {
                            deviceNumber = i;
                            mainForm.currentReelToReelFilename = GetAudioFileNameFromReel(reelItem, receivedText);
                            mainForm.AddLog($"Starting Reel-to-Reel for {mainForm.currentReelToReelFilename} on Command: {reelItem.StartCommand} from Input: {WaveInEvent.GetCapabilities(i).ProductName}");
                            break; // Exit loop since we found the desired device
                        }
                    }
                    if (deviceNumber != -1)
                    {
                        waveInEvent.DeviceNumber = deviceNumber;
                        waveInEvent.WaveFormat = waveFormat;

                        string reelFileName = mainForm.currentReelToReelFilename + ".tmp";
                        mainForm.lblR2RFileName.Text = mainForm.currentReelToReelFilename;

                        using (WaveFileWriter waveFileWriter = new WaveFileWriter(reelFileName, waveFormat))
                        {
                            waveInEvent.DataAvailable += (sender, args) =>
                            {
                                waveFileWriter.Write(args.Buffer, 0, args.BytesRecorded);
                                float leftPeakValue = 0f;
                                float rightPeakValue = 0f;

                            // Process the audio data and calculate the peak values for left and right channels
                            for (int i = 0; i < args.BytesRecorded; i += 4) // Assuming 16-bit stereo audio
                            {
                                    short leftSample = (short)((args.Buffer[i + 1] << 8) | args.Buffer[i]);
                                    float leftSampleValue = leftSample / 32768f; // Normalize the sample value to the range [-1, 1]
                                leftPeakValue = Math.Max(leftPeakValue, Math.Abs(leftSampleValue));

                                    short rightSample = (short)((args.Buffer[i + 3] << 8) | args.Buffer[i + 2]);
                                    float rightSampleValue = rightSample / 32768f; // Normalize the sample value to the range [-1, 1]
                                rightPeakValue = Math.Max(rightPeakValue, Math.Abs(rightSampleValue));
                                }

                            // Update the VU meters in the recording section
                            mainForm.Invoke((MethodInvoker)(() => { mainForm.UpdateRecVUMeters(leftPeakValue, rightPeakValue); }));
                            };

                            waveInEvent.RecordingStopped += (sender, e) =>
                            {
                                waveFileWriter?.Dispose(); // Dispose of the writer to flush data and release the file
                                waveInEvent.Dispose();
                            };

                            Stopwatch stopwatch = Stopwatch.StartNew(); // Start the stopwatch
                            TurnRecordingStatusOn(mainForm, reelItem.MaxLengthSecs);
                            waveInEvent.StartRecording();
                            try
                            {
                                while (!cancellationToken.IsCancellationRequested)
                                {
                                    if (reelItem.MaxLengthSecs > 0 && stopwatch.Elapsed.TotalSeconds >= (reelItem.MaxLengthSecs + 1))
                                    {
                                        mainForm.AddLog($"Maximum record time ({reelItem.MaxLengthSecs.ToString()}s) reached for {mainForm.currentReelToReelFilename}.");
                                        break; // Stop recording after reaching max length
                                    }
                                    else if (stopwatch.Elapsed.TotalSeconds == reelItem.MaxLengthSecs)
                                    {
                                        TurnRecordingStatusOff(mainForm);
                                    }

                                    // Optionally add a small delay to reduce CPU load
                                    await Task.Delay(100); // Adjust the delay value as needed
                                }
                            }
                            finally
                            {
                                mainForm.AddLog($"Recording stopped.");
                                stopwatch.Stop(); // Stop the stopwatch
                                waveInEvent.StopRecording();
                                waveFileWriter?.Dispose(); // Dispose of the writer to flush data and release the file
                                waveInEvent.Dispose();
                                TurnRecordingStatusOff(mainForm);
                                #pragma warning disable CS4014 //disable warning about awaiting a task. We don't care, let it process
                                Task.Run(() => ConvertFileFormat(reelItem, mainForm));
                                #pragma warning restore CS4014 //renable warning about awaiting a task.
                            }

                        }
                    } else
                    {
                        mainForm.AddLog($"Requested Audio Input Card NOT FOUND: {desiredDeviceName}.", Color.Red);

                    }
                }
            }
            catch (Exception)
            {
                mainForm.AddLog("Error! Could not start recording.");
            }
        }

        private static void ConvertFileFormat(ReelItem reelItem, MainForm mainForm)
        {
            string outputFilePath;
            string reelFileName = mainForm.currentReelToReelFilename + ".tmp";
            mainForm.Invoke((Action)(() =>
            {
                mainForm.AddLog("Recording Complete. Waiting for file to finalize.");
            }));
            // Wait for the file to be released
            bool fileReady = false;
            while (!fileReady)
            {
                try
                {
                    using (var testDispose = new FileStream(reelFileName, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        fileReady = true; // The file is accessible, no exception was thrown
                        mainForm.Invoke((Action)(() =>
                        {
                            mainForm.AddLog("File has finalized. Beginning conversion...");
                        }));
                    }
                }
                catch (IOException e)
                {
                    // File is locked, wait and try again
                    Thread.Sleep(100); // Adjust the sleep interval as needed
                    Debug.WriteLine($"{e.Message}");
                }
            }

            string reelTempFile = mainForm.currentReelToReelFilename + ".tmp";
            string ffmpegPath = Path.Combine(AppContext.BaseDirectory, "ffmpeg.exe"); // Path to ffmpeg executable

            switch (reelItem.Format)
            {
                case 0:
                    try
                    {
                        mainForm.Invoke((Action)(() =>
                        {
                            mainForm.AddLog("Converting to AAC format...");
                        }));
                        string outputAACFile = mainForm.currentReelToReelFilename;
                        string ffmpegArguments = $"-y -i \"{reelTempFile}\" -c:a aac -b:a 128k \"{outputAACFile}\"";
                        ProcessStartInfo processStartInfo = new ProcessStartInfo
                        {
                            FileName = ffmpegPath,
                            Arguments = ffmpegArguments,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                        using (Process process = new Process())
                        {
                            process.StartInfo = processStartInfo;

                            process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                            process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);

                            process.Start();
                            process.BeginOutputReadLine();
                            process.BeginErrorReadLine();

                            process.WaitForExit(); // Pauses here until the process completes
                        }
                        File.Delete(reelTempFile);
                        mainForm.Invoke((Action)(() =>
                        {
                            mainForm.AddLog($"Conversion to M4A (AAC) completed: {outputAACFile}");
                            mainForm.currentReelToReelFilename = null;
                            UploadFTP(reelItem, mainForm, outputAACFile);
                        }));
                        
                    }
                    catch (Exception e)
                    {
                        mainForm.Invoke((Action)(() =>
                        {
                            mainForm.AddLog($"Error converting recorded file to AAC format: {reelTempFile}", Color.Red);
                            mainForm.AddLog($"{e.Message}", Color.Red);
                            Debug.WriteLine($"{e.Message}");
                            mainForm.currentReelToReelFilename = null;
                        }));
                    }
                    break;

                case 1:
                    outputFilePath = mainForm.currentReelToReelFilename;
                    try
                    {
                        try
                        {
                            mainForm.Invoke((Action)(() =>
                            {
                                mainForm.AddLog("Converting to MP3 format...");
                            }));
                            string outputAACFile = mainForm.currentReelToReelFilename;
                            string ffmpegArguments = $"-y -i \"{reelTempFile}\" -c:a mp3 -b:a 128k \"{outputFilePath}\"";
                            ProcessStartInfo processStartInfo = new ProcessStartInfo
                            {
                                FileName = ffmpegPath,
                                Arguments = ffmpegArguments,
                                RedirectStandardOutput = true,
                                RedirectStandardError = true,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };
                            using (Process process = new Process())
                            {
                                process.StartInfo = processStartInfo;

                                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                                process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);

                                process.Start();
                                process.BeginOutputReadLine();
                                process.BeginErrorReadLine();

                                process.WaitForExit(); // Pauses here until the process completes
                            }
                            File.Delete(reelTempFile);
                            mainForm.Invoke((Action)(() =>
                            {
                                mainForm.AddLog($"Conversion to MP3 completed: {outputAACFile}");
                                mainForm.currentReelToReelFilename = null;
                                UploadFTP(reelItem, mainForm, outputAACFile);
                            }));

                        }
                        catch (Exception e)
                        {
                            mainForm.Invoke((Action)(() =>
                            {
                                mainForm.AddLog($"Error converting recorded file to MP3 format: {reelTempFile}", Color.Red);
                                mainForm.AddLog($"{e.Message}", Color.Red);
                                Debug.WriteLine($"{e.Message}");
                                mainForm.currentReelToReelFilename = null;
                            }));
                        }

                    }
                    catch (Exception e)
                    {
                        mainForm.Invoke((Action)(() =>
                        {
                            mainForm.AddLog($"Error converting recorded file to MP3 format: {outputFilePath}", Color.Red);
                            mainForm.AddLog($"{e.Message}", Color.Red);
                            Debug.WriteLine($"{e.Message}");
                            mainForm.currentReelToReelFilename = null;
                        }));
                    }
                    break;

                case 3:
                    // standard old .wav file, just rename
                    outputFilePath = mainForm.currentReelToReelFilename;
                    try
                    {
                        File.Move(reelFileName, outputFilePath);
                        UploadFTP(reelItem, mainForm, outputFilePath);
                    }
                    catch (Exception)
                    {
                        mainForm.Invoke((Action)(() =>
                        {
                            mainForm.AddLog($"Error converting recorded file to wav format: {outputFilePath}");
                            mainForm.currentReelToReelFilename = null;
                        }));
                    }
                    break;
            }
        }

        internal static void UploadFTP(ReelItem reelItem, MainForm mainForm, string outputFilePath)
        {
            if (reelItem.FTPServerId == 0)
            {
                //No FTP Upload
                mainForm.Invoke((Action)(() =>
                {
                    mainForm.AddLog($"FTP Upload DISABLED for Reel-To-Reel: {outputFilePath}");
                }));

            }
            else
            {
                mainForm.Invoke((Action)(() =>
                {
                    mainForm.AddLog($"FTP Upload ENABLED for Reel-To-Reel: {outputFilePath}");
                }));
                FTPServerItem ftpServer = DatabaseOperations.GetFTPServerById(reelItem.FTPServerId);
                FormFTPOutput FormFTPOutput = new FormFTPOutput(mainForm, "upload", ftpServer, reelItem, outputFilePath);
                FormFTPOutput.Show();
            }
        }

    }
}