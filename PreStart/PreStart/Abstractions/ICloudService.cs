using PreStart.Models;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace PreStart.Abstractions
{
    public interface ICloudService
    {
        Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData;

        Task SyncOfflineCacheAsync();

        Task LoginAsync();

        Task<AppServiceIdentity> GetIdentityAsync();
    }
}
