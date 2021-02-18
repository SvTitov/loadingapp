using System;

namespace LoadingApp.Services
{
    public interface IFileService
    {
        string GetDownloadsPath();

        void GetFiles(Action<string> func);
    }
}