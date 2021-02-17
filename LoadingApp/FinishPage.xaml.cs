using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoadingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinishPage : ContentPage
    {
        public FinishPage(string path)
        {
            InitializeComponent();

            BindingContext = new FinishPageViewModel(path);
        }
    }
}