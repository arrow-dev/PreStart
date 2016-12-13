using System.Threading.Tasks;

namespace PreStart.Abstractions
{
    public interface ICloudService
    {
        Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData;
    }
}
