using Microsoft.WindowsAzure.MobileServices;
using PreStart.Abstractions;

namespace PreStart.Services
{
    public class AzureCloudService : ICloudService
    {
        MobileServiceClient Client;

        public AzureCloudService()
        {
            Client = new MobileServiceClient("https://prestart.azurewebsites.net");
        }

        public ICloudTable<T> GetTable<T>() where T : TableData
        {
            return new AzureCloudTable<T>(Client);
        }

        
    }
}
