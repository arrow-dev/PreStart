using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using PreStart.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreStart.Services
{
    public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
    {
        IMobileServiceSyncTable<T> Table;
        IMobileServiceTable<T> HistoricTable;

        public AzureCloudTable(MobileServiceClient client)
        {
            Table = client.GetSyncTable<T>();
            HistoricTable = client.GetTable<T>();
        }

        public async Task PullAsync()
        {
            var queryName = $"incsync_{typeof(T).Name}";
            var query = Table.CreateQuery()
                .Where(r => r.CreatedAt < DateTimeOffset.Now.AddDays(-14));
            await Table.PullAsync(queryName, query);
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