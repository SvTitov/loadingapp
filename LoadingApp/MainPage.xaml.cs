using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadingApp.Services;
using LoadingApp.ViewModels;
using Xamarin.Forms;

namespace LoadingApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var fs = DependencyService.Resolve<IFileService>();

            BindingContext = new MainViewModel(fs)
            {
                Page = this
            };
        }
    }
}
