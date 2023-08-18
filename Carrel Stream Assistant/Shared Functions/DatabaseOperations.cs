﻿using System;
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

        public static GeneralSettings GetGeneralSettingsFromDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT NetCuePort, NetCueProcessingMode, NetCueStartCommand, NetCueStopCommand, AudioFeedVolume FROM Settings WHERE Id = @Id";
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
                                AudioFeedVolume = reader["AudioFeedVolume"].ToString()
                            };

                            return settings;
                        }
                        settings = new GeneralSettings
                        {
                            NetCuePort = 9963,
                            NetCueProcessingMode = "Always Process Incoming NetCues",
                            NetCueStartCommand = "",
                            NetCueStopCommand = "",
                            AudioFeedVolume = "1.0"
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

        public static void CheckDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Get the current schema version
                int currentVersion = SchemaUpdater.GetSchemaVersion(connection);

                // Apply schema updates based on current version
                if (currentVersion < 1) { SchemaUpdater.UpdateToVersion1(connection); }
                if (currentVersion < 2) { SchemaUpdater.UpdateToVersion2(connection); }
            }
        }

    }
}
