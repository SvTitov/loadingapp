using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LoadingApp.Annotations;
using LoadingApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LoadingApp.ViewModels
{
    public class FinishPageViewModel : INotifyPropertyChanged
    {
        private readonly string _path;
        public ICommand SendEmailCommand { get; }

        public ICommand UploadAzureCommand { get; }

        public FinishPageViewModel(string path)
        {
            _path = path;
            SendEmailCommand = new Command(SendEmail);
            UploadAzureCommand = new Command(UploadAzure);
        }

        private async void UploadAzure()
        {
            await new BlogService()
                .Upload();
        }

        private async void SendEmail()
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "Subject",
                    Body = "Test body!",
                    To = new List<string>(new [] { "test@test.com" }),
                    Attachments = new List<EmailAttachment>
                    {
                        new EmailAttachment(_path)
                    }
                };
                await Email.ComposeAsync(message);
            }
            catch (Exception e)
            {
               
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}