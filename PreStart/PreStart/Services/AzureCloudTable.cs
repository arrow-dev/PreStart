using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using PreStart.Abstractions;

namespace PreStart.Services
{
    public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
    {
        MobileServiceClient Client;
        IMobileServiceTable<T> Table;

        public AzureCloudTable(MobileServiceClient client)
        {
            Client = client;
            Table = Client.GetTable<T>();
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