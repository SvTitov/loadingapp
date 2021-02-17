using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using LoadingApp.Annotations;
using LoadingApp.Services;
using Xamarin.Forms;

namespace LoadingApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IFileService _fileService;
        private string _url = @"https://raw.githubusercontent.com/stleary/JSON-java/master/src/test/resources/Issue537.json";
        private string _openFilePath;

        public ContentPage Page { get; set; }

        public ObservableCollection<string> Files { get; } = new ObservableCollection<string>();

        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

    

        public ICommand OpenCommand { get; } 

        public ICommand FindDownloadsCommand { get; }

        public ICommand NavigateCommand { get; }

        private void OpenUrl()
        {
            var webService = new WebService();
            webService.OpenUrl(Url);
        }

        public MainViewModel(IFileService fileService)
        {
            _fileService = fileService;
            OpenCommand = new Command(OpenUrl);
            FindDownloadsCommand = new Command(FindDownloads);
            NavigateCommand = new Command<object>(Navigate);
        }

        private void Navigate(object obj)
        {
            Page.Navigation.PushAsync(new EditorPage(obj.ToString()), true);
        }


        private void FindDownloads()
        {
            Files.Clear();
            var path = _fileService.GetDownloadsPath();
            var files = Directory.GetFiles(path)
                .Where(file => file.EndsWith(".json"))
                .ToList();

            files.ForEach(f => Files.Add(f));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}