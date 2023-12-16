using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SQLite;


namespace Carrel_Stream_Assistant
{
    public static class DatabaseOperations
    {
        // Define the variable in the class scope
        public readonly static string databaseName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Cyber-Comp Technologies, LLC", "Carrel Stream Assistant", "stream_assist.db");
        public readonly static string connectionString = $"Data Source={databaseName};Version=3;";

        public static void CheckDatabase()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Cyber-Comp Technologies, LLC", "Carrel Stream Assistant");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Get the current schema version
                int currentVersion = SchemaUpdater.GetSchemaVersion(connection);

                // Apply schema updates based on current version
                if (currentVersion < 1) { SchemaUpdater.UpdateToVersion1(connection); }
                if (currentVersion < 2) { SchemaUpdater.UpdateToVersion2(connection); }
                if (currentVersion < 3) { SchemaUpdater.UpdateToVersion3(connection); }
                if (currentVersion < 4) { SchemaUpdater.UpdateToVersion4(connection); }
                if (currentVersion < 5) { SchemaUpdater.UpdateToVersion5(connection); }
            }
        }

        public static GeneralSettings GetGeneralSettingsFromDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT NetCuePort, NetCueProcessingMode, NetCueStartCommand, NetCueStopCommand, AudioFeedVolume, InputVolumeControl FROM Settings WHERE Id = @Id";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Id", 1); // Assuming 'Id' value for the settings row
                    GeneralSettings settings;
                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            settings = new GeneralSettings
                            {
                                NetCuePort = Convert.ToInt32(reader["NetCuePort"]),
                                NetCueProcessingMode = reader["NetCueProcessingMode"].ToString(),
                                NetCueStartCommand = reader["NetCueStartCommand"].ToString(),
                                NetCueStopCommand = reader["NetCueStopCommand"].ToString(),
                                AudioFeedVolume = reader["AudioFeedVolume"].ToString(),
                                InputVolumeControl = Convert.ToInt32(reader["InputVolumeControl"])
                            };

                            return settings;
                        }
                        settings = new GeneralSettings
                        {
                            NetCuePort = 9963,
                            NetCueProcessingMode = "Always Process Incoming NetCues",
                            NetCueStartCommand = "",
                            NetCueStopCommand = "",
                            AudioFeedVolume = "1.0",
                            InputVolumeControl = 0
                        };
                        return settings;
                    }
                }
            }
        }

        public static Dictionary<string, object> GetNetCue(string NetCueCode)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = @"
                    SELECT r.*, n.Muted, n.FriendlyName
                    FROM Rotations r
                    INNER JOIN NetCues n ON r.NetCueID = n.Id
                    WHERE n.NetCue = @NetCue
                        AND (r.StartDate IS NULL OR r.StartDate = '' OR strftime('%Y-%m-%d %H:%M', datetime('now', 'localtime')) >= r.StartDate)
                        AND (r.EndDate IS NULL OR r.EndDate = '' OR strftime('%Y-%m-%d %H:%M', datetime('now', 'localtime')) <= r.EndDate)
                        AND (r.Marker = 1)
                    ORDER BY r.Marker DESC, r.SortOrder ASC
                    LIMIT 1";

                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@NetCue", NetCueCode);

                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Dictionary<string, object> rowData = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowData[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            return rowData;
                        }
                    }
                }

                //lookup where the marker is... If we find it here, let's assume it's not in season and skip it, moving the Marker down.
                selectQuery = @"
                    SELECT r.*, n.Muted, n.FriendlyName
                    FROM Rotations r
                    INNER JOIN NetCues n ON r.NetCueID = n.Id
                    WHERE n.NetCue = @NetCue
                    AND r.Marker = 1
                    ORDER BY r.Marker DESC, r.SortOrder ASC
                    LIMIT 1";

                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@NetCue", NetCueCode);

                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Dictionary<string, object> rowData = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowData[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }

                            // Update the Marker for the next suitable row
                            string updateMarkerQuery = @"
                                UPDATE Rotations
                                SET Marker = 0
                                WHERE NetCueID = @NetCueID
                                AND SortOrder = @SortOrder
                                ORDER BY SortOrder ASC
                                LIMIT 1";
                            using (SQLiteCommand updateMarkerCommand = new SQLiteCommand(updateMarkerQuery, connection))
                            {
                                updateMarkerCommand.Parameters.AddWithValue("@NetCueID", rowData["NetCueID"]);
                                updateMarkerCommand.Parameters.AddWithValue("@SortOrder", rowData["SortOrder"]);
                                updateMarkerCommand.ExecuteNonQuery();
                            }

                            // Update the Marker for the next suitable row
                            updateMarkerQuery = @"
                                UPDATE Rotations
                                SET Marker = 1
                                WHERE NetCueID = @NetCueID
                                AND SortOrder > @SortOrder
                                ORDER BY SortOrder ASC
                                LIMIT 1";
                            using (SQLiteCommand updateMarkerCommand = new SQLiteCommand(updateMarkerQuery, connection))
                            {
                                updateMarkerCommand.Parameters.AddWithValue("@NetCueID", rowData["NetCueID"]);
                                updateMarkerCommand.Parameters.AddWithValue("@SortOrder", rowData["SortOrder"]);
                                updateMarkerCommand.ExecuteNonQuery();
                            }

                            // Retrieve the updated row information
                            string updatedRowQuery = @"
                                SELECT r.*, n.Muted, n.FriendlyName
                                FROM Rotations r
                                WHERE r.NetCueID = @NetCueID
                                AND r.SortOrder = (SELECT MIN(SortOrder) FROM Rotations WHERE NetCueID = @NetCueID AND SortOrder > @SortOrder)
                            ";
                            using (SQLiteCommand updatedRowCommand = new SQLiteCommand(updatedRowQuery, connection))
                            {
                                updatedRowCommand.Parameters.AddWithValue("@NetCueID", rowData["NetCueID"]);
                                updatedRowCommand.Parameters.AddWithValue("@SortOrder", rowData["SortOrder"]);


                                using (SQLiteDataReader updatedRowReader = updatedRowCommand.ExecuteReader())
                                {
                                    if (updatedRowReader.Read())
                                    {
                                        Dictionary<string, object> updatedRowData = new Dictionary<string, object>();
                                        for (int i = 0; i < updatedRowReader.FieldCount; i++)
                                        {
                                            updatedRowData[updatedRowReader.GetName(i)] = updatedRowReader.IsDBNull(i) ? null : updatedRowReader.GetValue(i);
                                        }
                                        return updatedRowData;
                                    }
                                }
                            }
                        }
                    }
                }

                // If no row with Marker = 1 is found, try to get the first row by SortOrder and Id
                string fallbackQuery = @"
                    SELECT r.*, n.Muted, n.FriendlyName
                    FROM Rotations r
                    INNER JOIN NetCues n ON r.NetCueID = n.Id
                    WHERE n.NetCue = @NetCue
                        AND(r.StartDate IS NULL OR r.StartDate = '' OR strftime('%Y-%m-%d %H:%M', datetime('now', 'localtime')) >= r.StartDate)
                        AND(r.EndDate IS NULL OR r.EndDate = '' OR strftime('%Y-%m-%d %H:%M', datetime('now', 'localtime')) <= r.EndDate)
                    ORDER BY r.SortOrder ASC, r.Id ASC
                    LIMIT 1";

                using (SQLiteCommand fallbackCommand = new SQLiteCommand(fallbackQuery, connection))
                {
                    fallbackCommand.Parameters.AddWithValue("@NetCue", NetCueCode);

                    using (SQLiteDataReader reader = fallbackCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Dictionary<string, object> rowData = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowData[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            return rowData;
                        }
                    }
                }

                return null; // No suitable rows found
            }
        }

        public static string GetSelectedAudioOutputDeviceIdFromDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT AudioOutputDevice FROM Settings WHERE Id = @Id";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Id", 1); // Assuming 'Id' value for the settings row
                    object selectedDeviceId = selectCommand.ExecuteScalar();
                    return selectedDeviceId?.ToString();
                }
            }
        }
        public static string GetSelectedAudioInputDeviceIdFromDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT AudioFeedDevice FROM Settings WHERE Id = @Id";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Id", 1); // Assuming 'Id' value for the settings row
                    object selectedDeviceId = selectCommand.ExecuteScalar();
                    return selectedDeviceId?.ToString();
                }
            }
        }

        public static bool IsNetCueValid(string NetCueCode)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT id FROM NetCues WHERE NetCue = @NetCue";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@NetCue", NetCueCode);
                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        return reader.HasRows; // Return true if rows are found, false otherwise
                    }
                }
            }
        }

        public static void SaveReel(ReelItem reelItem, Action cacheRefreshCallback)
        {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    if (reelItem.Id == 0)
                    {
                        // Insert a new record
                        string insertQuery = "INSERT INTO ReelToReel (Format, Filename, StartCommand, StopCommand, MaxLengthSecs, FTPServerId, FTPPath) " +
                                             "VALUES (@Format, @Filename, @StartCommand, @StopCommand, @MaxLengthSecs, @FTPServerId, @FTPPath)";

                        using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Format", reelItem.Format);
                            command.Parameters.AddWithValue("@Filename", reelItem.Filename);
                            command.Parameters.AddWithValue("@StartCommand", reelItem.StartCommand);
                            command.Parameters.AddWithValue("@StopCommand", reelItem.StopCommand);
                            command.Parameters.AddWithValue("@MaxLengthSecs", reelItem.MaxLengthSecs);
                            command.Parameters.AddWithValue("@FTPServerId", reelItem.FTPServerId);
                            command.Parameters.AddWithValue("@FTPPath", reelItem.FTPPath);
                            command.ExecuteNonQuery(); // Execute the INSERT query
                        }
                    }
                    else
                    {
                        // Update an existing record
                        string updateQuery = "UPDATE ReelToReel SET Format = @Format, Filename = @Filename, StartCommand = @StartCommand, " +
                                             "StopCommand = @StopCommand, MaxLengthSecs = @MaxLengthSecs, FTPServerId = @FTPServerId, FTPPath = @FTPPath WHERE Id = @Id";

                        using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Format", reelItem.Format);
                            command.Parameters.AddWithValue("@Filename", reelItem.Filename);
                            command.Parameters.AddWithValue("@StartCommand", reelItem.StartCommand);
                            command.Parameters.AddWithValue("@StopCommand", reelItem.StopCommand);
                            command.Parameters.AddWithValue("@MaxLengthSecs", reelItem.MaxLengthSecs);
                            command.Parameters.AddWithValue("@FTPServerId", reelItem.FTPServerId);
                            command.Parameters.AddWithValue("@FTPPath", reelItem.FTPPath);

                            command.Parameters.AddWithValue("@Id", reelItem.Id);

                            command.ExecuteNonQuery(); // Execute the UPDATE query
                        }
                    }
                // Invoke the cacheRefreshCallback after save operation
                cacheRefreshCallback?.Invoke();
            }
        }

        public static void SaveFtpServerItem(FTPServerItem ftpItem)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                if (ftpItem.Id == -1)
                {
                    // Insert new record
                    InsertFTPServer(connection, ftpItem);
                }
                else
                {
                    // Update existing record
                    UpdateFTPServer(connection, ftpItem);
                }
            }
        }

        private static void InsertFTPServer(SQLiteConnection connection, FTPServerItem ftpItem)
        {
            string sql = @"INSERT INTO FTPServers (Hostname, Username, Password, Salt, SecurityMode, TransferMode) 
                      VALUES (@Hostname, @Username, @Password, @Salt, @SecurityMode, @TransferMode)";

            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Hostname", ftpItem.HostName);
                command.Parameters.AddWithValue("@Username", ftpItem.Username);
                command.Parameters.AddWithValue("@Password", ftpItem.Password);
                command.Parameters.AddWithValue("@Salt", ftpItem.Salt);
                command.Parameters.AddWithValue("@SecurityMode", ftpItem.SecurityMode);
                command.Parameters.AddWithValue("@TransferMode", ftpItem.TransferMode);

                command.ExecuteNonQuery();
            }
        }

        private static void UpdateFTPServer(SQLiteConnection connection, FTPServerItem ftpItem)
        {
            string sql = @"UPDATE FTPServers 
                      SET Hostname = @Hostname, 
                          Username = @Username, 
                          Password = @Password, 
                          Salt = @Salt, 
                          SecurityMode = @SecurityMode, 
                          TransferMode = @TransferMode 
                      WHERE Id = @Id";

            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", ftpItem.Id);
                command.Parameters.AddWithValue("@Hostname", ftpItem.HostName);
                command.Parameters.AddWithValue("@Username", ftpItem.Username);
                command.Parameters.AddWithValue("@Password", ftpItem.Password);
                command.Parameters.AddWithValue("@Salt", ftpItem.Salt);
                command.Parameters.AddWithValue("@SecurityMode", ftpItem.SecurityMode);
                command.Parameters.AddWithValue("@TransferMode", ftpItem.TransferMode);

                command.ExecuteNonQuery();
            }
        }

        public static FTPServerItem GetFTPServerById(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM FTPServers WHERE Id = @Id";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            FTPServerItem ftpServer = new FTPServerItem
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                HostName = Convert.ToString(reader["Hostname"]),
                                Username = Convert.ToString(reader["Username"]),
                                Password = FTPOperations.DecryptAndDesaltPassword(Convert.ToString(reader["Password"]), Convert.ToString(reader["Salt"])),
                                Salt = Convert.ToString(reader["Salt"]),
                                SecurityMode = Convert.ToInt32(reader["SecurityMode"]),
                                TransferMode = Convert.ToInt32(reader["TransferMode"])
                            };

                            return ftpServer;
                        }
                        else
                        {
                            return null; // No matching record found
                        }
                    }
                }
            }
        }

        public static List<FTPServerItem> GetAllFTPServers()
        {
            List<FTPServerItem> ftpServers = new List<FTPServerItem>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM FTPServers";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FTPServerItem ftpServer = new FTPServerItem
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            HostName = Convert.ToString(reader["Hostname"]),
                            Username = Convert.ToString(reader["Username"]),
                            Password = FTPOperations.DecryptAndDesaltPassword(Convert.ToString(reader["Password"]), Convert.ToString(reader["Salt"])),
                            Salt = Convert.ToString(reader["Salt"]),
                            SecurityMode = Convert.ToInt32(reader["SecurityMode"]),
                            TransferMode = Convert.ToInt32(reader["TransferMode"])
                        };

                        ftpServers.Add(ftpServer);
                    }
                }
            }

            return ftpServers;
        }

        public static void DeleteFTPServer(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM FTPServers WHERE Id = @Id";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void LoadReelToReelTableFromDatabase(List<ReelItem> reelToReelTable, MainForm mainForm)
        {
            // Clear the existing reelToReelTable if it's provided
            mainForm.ReelToReelTable.Clear();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Create a command to select all records from the ReelToReel table
                string query = "SELECT * FROM ReelToReel";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ReelItem reelItem = new ReelItem()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Format = Convert.ToInt32(reader["Format"]),
                            Filename = Convert.ToString(reader["Filename"]),
                            StartCommand = Convert.ToString(reader["StartCommand"]),
                            StopCommand = Convert.ToString(reader["StopCommand"]),
                            MaxLengthSecs = Convert.ToInt32(reader["MaxLengthSecs"]),
                            FTPServerId = Convert.ToInt32(reader["FTPServerId"]),
                            FTPPath = Convert.ToString(reader["FTPPath"])
                        };
                        mainForm.ReelToReelTable.Add(reelItem);
                    }
                }
            }
        }

    }
}
