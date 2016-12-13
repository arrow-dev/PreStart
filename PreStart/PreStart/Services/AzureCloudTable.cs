using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using PreStart.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreStart.Services
{
    public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
    {
        MobileServiceClient Client;
        IMobileServiceSyncTable<T> Table;

        public AzureCloudTable(MobileServiceClient client)
        {
            Client = client;
            Table = Client.GetSyncTable<T>();
        }

        public async Task PullAsync()
        {
            string queryName = $"incsync_{typeof(T).Name}";
            await Table.PullAsync(queryName, Table.CreateQuery());
        }

        public async Task<T> CreateItemAsync(T item)
        {
            await Table.InsertAsync(item);
            return item;
        }

        public async Task DeleteItemAsync(T item)
        {
            await Table.DeleteAsync(item);
        }

        public async Task<ICollection<T>> ReadAllItemsAsync()
        {
            return await Table.ToListAsync();
        }

        public Task<ICollection<T>> ReadItemsAsync(int start, int count)
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> ReadItemAsync(string id)
        {
            return await Table.LookupAsync(id);
        }

        public async Task<T> UpdateItemAsync(T item)
        {
            await Table.UpdateAsync(item);
            return item;
        }
    }
}