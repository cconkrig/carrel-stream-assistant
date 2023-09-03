using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Carrel_Stream_Assistant
{
    public static class SchemaUpdater
    {
        public static void UpdateToVersion1(SQLiteConnection connection)
        {
            // Create default tables
            string createTablesQuery = @"
                CREATE TABLE IF NOT EXISTS NetCues (
                    Id INTEGER PRIMARY KEY,
                    NetCue TEXT UNIQUE NOT NULL,
                    FriendlyName TEXT,
                    Type TEXT DEFAULT 'simple' NOT NULL,
                    Muted INTEGER DEFAULT '0' NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Rotations (
                    Id INTEGER PRIMARY KEY,
                    NetCueID INTEGER,
                    SortOrder INTEGER,
                    Marker INTEGER,
                    CartPath TEXT,
                    StartDate TEXT,
                    EndDate TEXT
                );

                CREATE TABLE IF NOT EXISTS Settings (
                    Id INTEGER PRIMARY KEY,
                    NetCuePort INTEGER,
                    NetCueProcessingMode TEXT DEFAULT 'Always Process Incoming NetCues' NOT NULL,
                    NetCueStartCommand TEXT,
                    NetCueStopCommand TEXT,
                    AudioFeedDevice TEXT,
                    AudioOutputDevice TEXT
                );

                INSERT OR IGNORE INTO Settings (Id, NetCuePort) VALUES (1, 9963)
            ";
            using (SQLiteCommand createTablesCommand = new SQLiteCommand(createTablesQuery, connection))
            {
                createTablesCommand.ExecuteNonQuery();
            }

            SetSchemaVersion(connection, 1);
        }

        public static void UpdateToVersion2(SQLiteConnection connection)
        {
            using (SQLiteCommand updateCommand = new SQLiteCommand(connection))
            {
                updateCommand.CommandText = "ALTER TABLE Settings ADD COLUMN AudioFeedVolume TEXT DEFAULT '1.0' NOT NULL;";
                updateCommand.ExecuteNonQuery();
            }

            SetSchemaVersion(connection, 2);
        }

        public static void UpdateToVersion3(SQLiteConnection connection)
        {
            using (SQLiteCommand updateCommand = new SQLiteCommand(connection))
            {
                updateCommand.CommandText = @"CREATE TABLE IF NOT EXISTS ReelToReel (
                    Id INTEGER PRIMARY KEY,
                    Format INTEGER,
                    Filename TEXT,
                    StartCommand TEXT,
                    StopCommand TEXT,
                    MaxLengthSecs INTEGER DEFAULT 0 NOT NULL
                ); ";
                updateCommand.ExecuteNonQuery();
            }

            SetSchemaVersion(connection, 3);
        }

        public static void UpdateToVersion4(SQLiteConnection connection)
        {
            using (SQLiteCommand updateCommand = new SQLiteCommand(connection))
            {
                updateCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS FTPServers (
                    Id INTEGER PRIMARY KEY,
                    Hostname TEXT,
                    Username TEXT,
                    Password TEXT,
                    Salt TEXT,
                    SecurityMode INT DEFAULT 0 NOT NULL,
                    TransferMode INT DEFAULT 0 NOT NULL
                ); 

                ALTER TABLE ReelToReel ADD COLUMN FTPServerId INTEGER;
                ALTER TABLE ReelToReel ADD COLUMN FTPPath TEXT DEFAULT '' NOT NULL;
                ";
                updateCommand.ExecuteNonQuery();
            }

            SetSchemaVersion(connection, 4);
        }

        public static void UpdateToVersion5(SQLiteConnection connection)
        {
            using (SQLiteCommand updateCommand = new SQLiteCommand(connection))
            {
                updateCommand.CommandText = @"
                ALTER TABLE Settings ADD COLUMN InputVolumeControl INTEGER DEFAULT '0' NOT NULL;;
                ";
                updateCommand.ExecuteNonQuery();
            }

            SetSchemaVersion(connection, 5);
        }

        public static int GetSchemaVersion(SQLiteConnection connection)
        {
            using (SQLiteCommand getVersionCommand = new SQLiteCommand("PRAGMA user_version;", connection))
            {
                return Convert.ToInt32(getVersionCommand.ExecuteScalar());
            }
        }

        private static void SetSchemaVersion(SQLiteConnection connection, int version)
        {
            using (SQLiteCommand setVersionCommand = new SQLiteCommand(connection))
            {
                setVersionCommand.CommandText = $"PRAGMA user_version = {version};";
                setVersionCommand.ExecuteNonQuery();
            }
        }
    }
}
