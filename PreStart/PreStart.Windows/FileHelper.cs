using PreStart.Abstractions;
using PreStart.Windows;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace PreStart.Windows
{
    class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
