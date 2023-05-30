using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using FriendsBook.Models;

namespace FriendsBook.Data
{
    public class FriendDataBase
    {
        readonly SQLiteAsyncConnection database;

        public FriendDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Friend>().Wait();
        }

        public Task<List<Friend>> GetFriendsAsync()
        {
            //Get all notes.
            return database.Table<Friend>().ToListAsync();
        }

        public Task<Friend> GetFriendsAsync(int id)
        {
            // Get a specific note.
            return database.Table<Friend>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveFriendAsync(Friend friend)
        {
            if (friend.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(friend);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(friend);
            }
        }

        public Task<int> DeleteFriendAsync(Friend friend)
        {
            return database.DeleteAsync(friend);    
        }

    }
}
