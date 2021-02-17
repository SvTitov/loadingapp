using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LoadingApp.Services
{
    public interface IWebService
    {
        Task OpenUrl(string url);
    }

    public class WebService : IWebService
    {
        public async Task OpenUrl(string url)
        {
            try
            {
                await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception e)
            {
                
            }
        }
    }
}