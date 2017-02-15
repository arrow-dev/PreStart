namespace Prestart.Abstractions
{
    public interface ICloudService
    {
        ICloudTable<T> GetTable<T>() where T : TableData;
    }
}
