using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppScanning.Services
{
    class RosterDataConstants
    {
        public const string DatabaseFilename = "Scan.db";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath { get
            {
                var foo = FileSystem.Current.AppDataDirectory;
                var path = Path.Combine(FileSystem.Current.AppDataDirectory, DatabaseFilename);
                return path;
            } }
            
    }
}
