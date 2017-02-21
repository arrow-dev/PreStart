using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Prestart.Abstractions;
using System.Diagnostics;
using System.Threading.Tasks;
using Prestart.Model;

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
            store.DefineTable<Hazard>();
            store.DefineTable<SignOn>();

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
            try
            {
                await client.SyncContext.PushAsync();
            }
            catch (MobileServicePushFailedException ex)
            {
                if (ex.PushResult != null)
                {
                    if (ex.PushResult.Status == MobileServicePushStatus.Complete)
                    {
                        foreach (var error in ex.PushResult.Errors)
                        {
                            await CancelOperationAsync(error);
                        }
                        return;
                    }
                    foreach (var error in ex.PushResult.Errors)
                    {
                        switch (error.TableName)
                        {
                            case "Prestart":
                                await ResolveConflictAsync<Model.Prestart>(error);
                                break;
                            case "Hazard":
                                await ResolveConflictAsync<Model.Hazard>(error);
                                break;
                            case "SignOn":
                                await ResolveConflictAsync<Model.SignOn>(error);
                                break;
                        }
                    }
                    
                }
                
            }

            //Pull each sync table
            var prestartTable = await GetTableAsync<Model.Prestart>(); await prestartTable.PullAsync();
            var hazardTable = await GetTableAsync<Model.Hazard>(); await hazardTable.PullAsync();
            var signOnTable = await GetTableAsync<Model.SignOn>(); await signOnTable.PullAsync();
        }
        
        async Task ResolveConflictAsync<T>(MobileServiceTableOperationError error) where T : TableData
        {
            var serverItem = error.Result.ToObject<T>();
            var localItem = error.Item.ToObject<T>();

            // Note that you need to implement the public override Equals(TodoItem item)
            // method in the Model for this to work
            if (serverItem.Equals(localItem))
            {
                // Items are the same, so ignore the conflict
                await error.CancelAndDiscardItemAsync();
                return;
            }

            // Client Always Wins
            localItem.Version = serverItem.Version;
            await error.UpdateOperationAsync(JObject.FromObject(localItem));

            // Server Always Wins
            // await error.CancelAndDiscardItemAsync();
        }

        async Task CancelOperationAsync(MobileServiceTableOperationError error)
        {
            await error.CancelAndDiscardItemAsync();
        }
    }
}
