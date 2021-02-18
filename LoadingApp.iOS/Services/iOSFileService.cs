using System;
using System.IO;
using System.Linq;
using Foundation;
using LoadingApp.iOS.Services;
using LoadingApp.Services;
using SafariServices;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using MobileCoreServices;
using System.Collections.Generic;

[assembly: Dependency(typeof(iOSFileService))]
namespace LoadingApp.iOS.Services
{
    public class iOSFileService : IFileService
    {
        private Action<string> _pickerCallback;

        public void GetFiles(Action<string> func)
        {
            _pickerCallback = func;

            OpenPicker();
        }

        public string GetDownloadsPath()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library");
            return path;
        }

        private void OpenPicker()
        {
            var vc1 = Platform.GetCurrentUIViewController();

            var allowedUTIs = new string[] { UTType.UTF8PlainText,
                    UTType.PlainText,
                    UTType.RTF,
                    UTType.PNG,
                    UTType.Text,
                    UTType.PDF,
                    UTType.Image };


            var pvc = new UIDocumentPickerViewController(allowedUTIs, UIDocumentPickerMode.Import);
            vc1.PresentViewController(pvc, true, null);

            pvc.DidPickDocument += Pvc_DidPickDocument;
            pvc.DidPickDocumentAtUrls += Pvc_DidPickDocumentAtUrls;
        }

        private void Pvc_DidPickDocumentAtUrls(object sender, UIDocumentPickedAtUrlsEventArgs e)
        {
            try
            {
                var path = e.Urls.First().Path;

                _pickerCallback(path);
            }
            catch (Exception ex)
            {

            }
        }

        private void Pvc_DidPickDocument(object sender, UIDocumentPickedEventArgs e)
        {
            
        }
    }
}
