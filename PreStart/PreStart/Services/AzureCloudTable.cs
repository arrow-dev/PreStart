using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Prestart.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prestart.Services
{
    public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
    {
        
        IMobileServiceSyncTable<T> table;

        public AzureCloudTable(MobileServiceClient client)
        {
            this.table = client.GetSyncTable<T>();
        }
        
        public async Task<T> CreateItemAsync(T item)
        {
            await table.InsertAsync(item);
            return item;
        }

        public Task<T> UpsertItemAsync(T item)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteItemAsync(T item)
        {
            await table.DeleteAsync(item);
        }

        public async Task<ICollection<T>> ReadAllItemsAsync()
        {
            List<T> allItems = new List<T>();

            var pageSize = 50;
            var hasMore = true;
            while (hasMore)
            {
                var pageOfItems = await table.Skip(allItems.Count).Take(pageSize).ToListAsync();
                if (pageOfItems.Count > 0)
                {
                    allItems.AddRange(pageOfItems);
                }
                else
                {
                    hasMore = false;
                }
            }
            return allItems;
        }

        public Task<ICollection<T>> ReadItemsAsync(int start, int count)
        {
            throw new System.NotImplementedException();
        }

        public async Task PullAsync()
        {
            string queryName = $"incsync_{typeof(T).Name}";
            await table.PullAsync(queryName, table.CreateQuery());
        }

        public async Task<T> ReadItemAsync(string id)
        {
            return await table.LookupAsync(id);
        }

        public async Task<T> UpdateItemAsync(T item)
        {
            await table.UpdateAsync(item);
            return item;
        }
    }
}
