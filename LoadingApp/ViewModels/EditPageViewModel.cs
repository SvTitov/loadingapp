using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LoadingApp.Annotations;
using Xamarin.Forms;

namespace LoadingApp.ViewModels
{
    public class EditPageViewModel : INotifyPropertyChanged
    {
        private readonly string _path;
        private string _fileText;

        public string FileText
        {
            get => _fileText;
            set
            {
                _fileText = value;
                OnPropertyChanged(nameof(FileText));
            }
        }

        public ContentPage Page { get; set; }

        public ICommand SaveEditedCommand { get; }

        public ICommand FinishCommand { get; }


        public EditPageViewModel(string path)
        {
            _path = path;

            Task.Run(()=> OpenFile(path));

            SaveEditedCommand = new Command(SaveEdited);
            FinishCommand = new Command(Finish);
        }

        private void Finish()
        {
            Page.Navigation.PushAsync(new FinishPage(_path));
        }

        private void SaveEdited()
        {
            if (string.IsNullOrEmpty(_path))
            {
                return;
            }

            File.WriteAllText(_path, FileText, Encoding.UTF8);

            FileText = string.Empty;
        }

        private Task OpenFile(string path)
        {
            FileText = File.ReadAllText(path);
            
            return Task.CompletedTask;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}