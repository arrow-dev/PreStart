using Microsoft.WindowsAzure.MobileServices;
using Prestart.Abstractions;

namespace Prestart.Services
{
    public class AzureCloudService : ICloudService
    {
        MobileServiceClient client;

        public AzureCloudService()
        {
            client = new MobileServiceClient("https://prestart.azurewebsites.net");
        }

        public ICloudTable<T> GetTable<T>() where T : TableData
        {
            return new AzureCloudTable<T>(client);
        }
    }
}
