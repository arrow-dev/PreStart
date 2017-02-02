using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace PreStart.Abstractions
{
    public interface ILoginProvider
    {
        Task LoginAsync(MobileServiceClient client);
    }
}
