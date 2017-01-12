using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using PreStart.Abstractions;
using PreStart.Models;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace PreStart.Services
{
    public class AzureCloudService : ICloudService
    {
        MobileServiceClient Client;

        public AzureCloudService()
        {
            Client = new MobileServiceClient("https://prestart.azurewebsites.net");
        }

        public async Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData
        {
            await InitializeAsync();
            return new AzureCloudTable<T>(Client);
        }

        public async Task SyncOfflineCacheAsync()
        {
            await InitializeAsync();

            // Push the Operations Queue to the mobile backend
            await Client.SyncContext.PushAsync();
            
            

            // Pull each sync table
            var prestartTable = await GetTableAsync<Prestart>(); await prestartTable.PullAsync();
            var taskTable = await GetTableAsync<Models.Task>(); await taskTable.PullAsync();
            var hazardTable = await GetTableAsync<Hazard>(); await hazardTable.PullAsync();
            var siteTable = await GetTableAsync<Site>(); await siteTable.PullAsync();
        }

        async Task InitializeAsync()
        {
            // Short circuit - local database is already initialized
            if (Client.SyncContext.IsInitialized)
                return;
           
            // Create a reference to the local sqlite store
            var store = new MobileServiceSQLiteStore("offlinecache.db");
            // Define the database schema
            store.DefineTable<Prestart>();
            store.DefineTable<Models.Task>();
            store.DefineTable<Hazard>();
            store.DefineTable<Site>();

            // Actually create the store and update the schema
            await Client.SyncContext.InitializeAsync(store);
        }
    }
}
