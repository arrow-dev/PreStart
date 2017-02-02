using Microsoft.WindowsAzure.MobileServices;
using PreStart.Abstractions;
using System.Threading.Tasks;
using TaskList.iOS.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(iOSLoginProvider))]
namespace TaskList.iOS.Services
{
    public class iOSLoginProvider : ILoginProvider
    {
        public async Task LoginAsync(MobileServiceClient client)
        {
            await client.LoginAsync(RootView, "aad");
        }

        public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;
    }
}