using System.Threading.Tasks;

namespace Prestart.Abstractions
{
    public interface ICloudService
    {
        Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData;
        Task SyncOfflineCacheAsync();
    }
}
