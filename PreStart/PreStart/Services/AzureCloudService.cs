using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Plugin.Connectivity;
using PreStart.Abstractions;
using PreStart.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.Services
{
    public class AzureCloudService : ICloudService
    {
        private MobileServiceClient Client;

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

            if (!(await CrossConnectivity.Current.IsRemoteReachable(Client.MobileAppUri.Host, 443)))
            {
                return;
            }

            // Push the Operations Queue to the mobile backend
            
            await Client.SyncContext.PushAsync();
            
            
            
            

            // Pull each sync table
            var prestartTable = await GetTableAsync<Prestart>();     await prestartTable.PullAsync();
            var hazardTable =   await GetTableAsync<Hazard>();       await hazardTable.PullAsync();
            var signOnTable =   await GetTableAsync<SignOn>();       await signOnTable.PullAsync();
        }

        public Task LoginAsync()
        {
            var loginProvider = DependencyService.Get<ILoginProvider>();
            return loginProvider.LoginAsync(Client);
        }

        private List<AppServiceIdentity> identities = null;

        public async Task<AppServiceIdentity> GetIdentityAsync()
        {
            if (Client.CurrentUser == null || Client.CurrentUser?.MobileServiceAuthenticationToken == null)
            {
                throw new InvalidOperationException("Not Authenticated");
            }

            if (identities == null)
            {
                identities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
            }

            if (identities.Count > 0)
            {
                return identities[0];
            }
            return null;
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
            store.DefineTable<Hazard>();
            store.DefineTable<SignOn>();

            // Actually create the store and update the schema
            await Client.SyncContext.InitializeAsync(store);
        }
    }
}
