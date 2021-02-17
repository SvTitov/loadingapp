using LoadingApp.Droid.Services;
using LoadingApp.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidFileService))]
namespace LoadingApp.Droid.Services
{
    public class AndroidFileService : IFileService
    {
        public string GetDownloadsPath()
        {
            return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
        }
    }
}