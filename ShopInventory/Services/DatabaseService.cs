using SQLite;
using ShopInventory.Models;

namespace ShopInventory.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {

        }

        private async Task Init()
        {
            if (_database is not null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "ShopInventory.db");
            _database = new SQLiteAsyncConnection(databasePath);

            await _database.CreateTableAsync<PurchasedItem>();
            await _database.CreateTableAsync<SoldItem>();
        }

        // Purchased Items CRUD operations
        public async Task<List<PurchasedItem>> GetPurchasedItemsAsync()
        {
            await Init();
            return await _database.Table<PurchasedItem>().OrderByDescending(x => x.PurchaseDate).ToListAsync();
        }

        public async Task<int> SavePurchasedItemAsync(PurchasedItem item)
        {
            await Init();
            if (item.Id != 0)
                return await _database.UpdateAsync(item);
            else
                return await _database.InsertAsync(item);
        }

        public async Task<int> DeletePurchasedItemAsync(PurchasedItem item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }

        // Sold Items CRUD operations
        public async Task<List<SoldItem>> GetSoldItemsAsync()
        {
            await Init();
            return await _database.Table<SoldItem>().OrderByDescending(x => x.SaleDate).ToListAsync();
        }

        public async Task<int> SaveSoldItemAsync(SoldItem item)
        {
            await Init();
            if (item.Id != 0)
                return await _database.UpdateAsync(item);
            else
                return await _database.InsertAsync(item);
        }

        public async Task<int> DeleteSoldItemAsync(SoldItem item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }

        // Delete sold items older than specified days
        public async Task<int> DeleteOldSoldItemsAsync(int daysOld = 90)
        {
            await Init();
            var cutoffDate = DateTime.Today.AddDays(-daysOld);
            return await _database.ExecuteAsync("DELETE FROM SoldItems WHERE SaleDate < ?", cutoffDate);
        }
    }
}