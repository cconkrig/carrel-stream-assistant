using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Drawing;
using FluentFTP;
using System.Windows.Forms;

namespace Carrel_Stream_Assistant
{
    public static class FTPOperations
    {
        // Encryption Key
        private static readonly byte[] AesKey = new byte[]
        {
            0x2B, 0x7E, 0x15, 0x16, 0x28, 0xAE, 0xD2, 0xA6,
            0xAB, 0xF7, 0x97, 0x46, 0x59, 0x77, 0xF0, 0xE1
        };
        public static event EventHandler<TerminalUpdateEventArgs> ScreenUpdated;

        public static string GetNewSaltString()
        {
            byte[] salt = GenerateSalt();
            // Convert salt to hex string
            return ByteArrayToHexString(salt);
        }

        public static string EncryptAndSaltPassword(string password, string salt)
        {
            // Encrypt the password
            string encryptedPassword = EncryptPassword(password);

            // Combine the encrypted password and salt
            string combinedPassword = CombinePasswordAndSalt(encryptedPassword, salt);

            return combinedPassword;
        }

        public static string DecryptAndDesaltPassword(string combinedPassword, string salt)
        {
            // Separate the encrypted password and salt
            string encryptedPassword = ExtractEncryptedPassword(combinedPassword, salt);

            // Decrypt the password
            string decryptedPassword = DecryptPassword(encryptedPassword);

            return decryptedPassword;
        }

        private static string EncryptPassword(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = AesKey;
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];
                aesAlg.Mode = CipherMode.CFB; // Change this to a secure mode
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes = null;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(password);
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }

                return ByteArrayToHexString(encryptedBytes);
            }
        }

        private static string CombinePasswordAndSalt(string encryptedPassword, string salt)
        {
            return encryptedPassword + salt;
        }

        private static string ExtractEncryptedPassword(string combinedPassword, string salt)
        {
            return combinedPassword.Substring(0, combinedPassword.Length - salt.Length);
        }

        private static string DecryptPassword(string encryptedPassword)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = AesKey;
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];
                aesAlg.Mode = CipherMode.CFB; // Change this to match encryption mode
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes = HexStringToByteArray(encryptedPassword);
                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        private static string ByteArrayToHexString(byte[] byteArray)
        {
            return BitConverter.ToString(byteArray).Replace("-", "");
        }

        public static byte[] HexStringToByteArray(string hexString)
        {
            int length = hexString.Length;
            byte[] byteArray = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return byteArray;
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static dynamic ConnectToFTP(FTPServerItem ftpServer)
        {
            try
            {
                if (ftpServer.SecurityMode == 0) // FTP
                {
                    
                    FtpClient client = new FtpClient();
                    string input = ftpServer.HostName;
                    string[] parts = input.Split(':');
                    string hostname = parts[0]; // Default hostname
                    if (parts.Length == 2)
                    {
                        string portStr = parts[1];
                        client.Port = int.Parse(portStr); // Convert the port string to an integer
                                                          // Use the port as needed
                    }
                    client.Host = hostname;
                    client.Credentials = new NetworkCredential(ftpServer.Username, ftpServer.Password);
                    client.Config.EncryptionMode = FtpEncryptionMode.None;
                    // Set the transfer mode based on the ftpServer.TransferMode setting
                    if (ftpServer.TransferMode == 0)
                    {
                        UpdateTerminal($"> Connecting to {hostname} via FTP using PASSIVE mode...");
                        client.Config.DataConnectionType = FtpDataConnectionType.AutoPassive;
                    }
                    else if (ftpServer.TransferMode == 1)
                    {
                        UpdateTerminal($"> Connecting to {hostname} via FTP using ACTIVE mode...");
                        client.Config.DataConnectionType = FtpDataConnectionType.AutoActive;
                    }
                    client.Connect();
                    UpdateTerminal($"> Connected to {hostname} via FTP using Username: {ftpServer.Username}", Color.Green);
                    return client;
                }
                else if (ftpServer.SecurityMode == 1) // FTPS
                {
                    FtpClient client = new FtpClient();
                    string input = ftpServer.HostName;
                    string[] parts = input.Split(':');
                    string hostname = parts[0]; // Default hostname
                    if (parts.Length == 2)
                    {
                        string portStr = parts[1];
                        client.Port = int.Parse(portStr); // Convert the port string to an integer
                                                          // Use the port as needed
                    }
                    client.Host = hostname;
                    client.Credentials = new NetworkCredential(ftpServer.Username, ftpServer.Password);
                    client.Config.EncryptionMode = FtpEncryptionMode.Explicit;

                    client.ValidateCertificate += (sender, e) => { e.Accept = true; };
                    UpdateTerminal($"> Connecting to {hostname} via FTPS...");
                    client.Connect();
                    UpdateTerminal($"> Connected to {hostname} via FTPS using Username: {ftpServer.Username}", Color.Green);
                    return client;
                }
                return null;
            }
            catch (Exception)
            {
                UpdateTerminal($"> CONNECTION FAILED!");
                return null;
            }
        }

        public static bool TestFTPConnection(FTPServerItem ftpServer)
        {
            dynamic ftpClient = ConnectToFTP(ftpServer);
            string testFilename = null;
            if (ftpClient != null)
            {
                try
                {
                    // Perform FTP/FTPS operations using the dynamic object
                    using (ftpClient)
                    {
                        string guid = Path.GetRandomFileName().Replace(".", "");
                        testFilename = $"CarrelStreamAssistant_{guid}_SafeToDelete.txt";
                        UpdateTerminal($"> Attempting to write {testFilename} to /");

                        using (var stream = ftpClient.OpenWrite(testFilename))
                        {
                            byte[] content = Encoding.UTF8.GetBytes("Test upload! This is an automated test, please delete this file.");
                            stream.Write(content, 0, content.Length);
                            // Check if the stream is writable
                            if (stream.CanWrite)
                            {
                                UpdateTerminal($"> File write operation successful.", Color.Green);
                            }
                            else
                            {
                                UpdateTerminal($"> File write operation failed.", Color.Red);
                            }
                            stream.Close(); // Close the file stream after writing
                        }

                        UpdateTerminal($"> Removing {testFilename} from /");

                        try
                        {
                            // Attempt to delete the file
                            ftpClient.SetWorkingDirectory("/");
                            ftpClient.DeleteFile(testFilename);
                            UpdateTerminal($"> Removed {testFilename}.", Color.Green);
                            return true;
                        }
                        catch (Exception e)
                        {
                            UpdateTerminal($"> Failed to remove {testFilename}. This could be lack of permissions, please delete the file manually.", Color.Red);
                            UpdateTerminal($"> {e.Message}", Color.Red);
                            return false;
                        }
                    }
                }
                catch (Exception e)
                {
                    UpdateTerminal($"> Failed to remove the file {testFilename}. This could be lack of permissions, please delete the file manually.", Color.Red);
                    UpdateTerminal($"> {e.Message}", Color.Red);
                    return false;
                }
            }

            return false;
        }

        public static bool UploadFile(FTPServerItem ftpServer, ReelItem reelItem, string filename_to_upload = null, dynamic parent = null)
        {
            dynamic ftpClient = ConnectToFTP(ftpServer);
            if (ftpClient != null)
            {
                try
                {
                    // Perform FTP/FTPS operations using the dynamic object
                    using (ftpClient)
                    {
                        UpdateTerminal($"> Setting working directory: {reelItem.FTPPath}");
                        ftpClient.SetWorkingDirectory($"{reelItem.FTPPath}");
                        parent.progressUpload.Visible = true;
                        UpdateTerminal($"> Uploading {filename_to_upload}...");

                        // Extract the filename from the full path
                        string filename = Path.GetFileName(filename_to_upload);
                        using (var stream = ftpClient.OpenWrite(filename))
                        {
                            using (var fileStream = File.OpenRead(filename_to_upload))
                            {
                                byte[] buffer = new byte[4096]; // Buffer size for reading the file
                                int bytesRead;
                                long totalBytes = fileStream.Length;
                                long uploadedBytes = 0;

                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    stream.Write(buffer, 0, bytesRead);
                                    uploadedBytes += bytesRead;

                                    // Update the progress bar
                                    int progressPercentage = (int)((float)uploadedBytes / totalBytes * 100);
                                    parent.progressUpload.Value = progressPercentage;
                                }
                            }
                        }
                    }
                    UpdateTerminal($"> Upload Finished!");
                    return true;
                }
                catch (Exception e)
                {
                    UpdateTerminal($"> UPLOAD FAILED.", Color.Red);
                    UpdateTerminal($"> {e.Message}", Color.Red);
                    return false;
                }
            }
            return false;
        }


        public static void UpdateTerminal(string terminalLine, Color forecolor = default(Color))
        {
            if (forecolor == default(Color))
            {
                forecolor = Color.White;
            }
            ScreenUpdated?.Invoke(null, new TerminalUpdateEventArgs(terminalLine, forecolor));
        }



    }
}
