using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Plugin.Connectivity;
using Prestart.Abstractions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Prestart.Services
{
    public class AzureCloudService : ICloudService
    {
        MobileServiceClient client;

        public AzureCloudService()
        {
            client = new MobileServiceClient("https://prestart.azurewebsites.net");
        }

        public async Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData
        {
            await InitializeAsync();
            return new AzureCloudTable<T>(client);
        }

        async Task InitializeAsync()
        {
            if (client.SyncContext.IsInitialized)
            return;

            var store = new MobileServiceSQLiteStore("offlinecache.db");

            store.DefineTable<Model.Prestart>();
            store.DefineTable<Model.Hazard>();
            store.DefineTable<Model.SignOn>();

            await client.SyncContext.InitializeAsync(store);
        }

        public async Task SyncOfflineCacheAsync()
        {
            await InitializeAsync();

            if (!(await CrossConnectivity.Current.IsRemoteReachable(client.MobileAppUri.Host, 443)))
            {
                Debug.WriteLine($"Cannot connect to {client.MobileAppUri} right now - offline");
                return;
            }

            // Push the Operations Queue to the mobile backend
            await client.SyncContext.PushAsync();

            // Pull each sync table
            //var prestartTable = await GetTableAsync<Model.Prestart>(); await prestartTable.PullAsync();
            //var hazardTable = await GetTableAsync<Model.Hazard>(); await hazardTable.PullAsync();
            //var signOnTable = await GetTableAsync<Model.SignOn>(); await signOnTable.PullAsync();
        }
    }
}
