using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppScanning.Services
{
    public class RosterDatabase
    {
        SQLiteAsyncConnection Database;
        public RosterDatabase()
        {
        }
        async Task Init()
        {
            if (Database is not null)
                return;

            if (!File.Exists(RosterDataConstants.DatabasePath))
            {
                using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(RosterDataConstants.DatabaseFilename);
                using Stream dbStream = File.OpenWrite(RosterDataConstants.DatabasePath);
                fileStream.CopyTo(dbStream);

                fileStream.Dispose();
                dbStream.Dispose();
            }

            Database = new SQLiteAsyncConnection(RosterDataConstants.DatabasePath, RosterDataConstants.Flags);
            //var result = await Database.CreateTableAsync<Roster>();
        }

        public async Task<List<Roster>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Roster>().ToListAsync();
        }

        public async Task<Roster> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Roster>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Roster item)
        {
            await Init();
            if (item.Id!= 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(Roster item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
