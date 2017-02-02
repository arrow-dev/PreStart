using Microsoft.WindowsAzure.MobileServices;
using PreStart.Abstractions;
using System.Threading.Tasks;
using TaskList.UWP.Services;

[assembly: Xamarin.Forms.Dependency(typeof(UWPLoginProvider))]
namespace TaskList.UWP.Services
{
    public class UWPLoginProvider : ILoginProvider
    {
        public async Task LoginAsync(MobileServiceClient client)
        {
            await client.LoginAsync("aad");
        }
    }
}