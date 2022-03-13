using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageChargement : ContentPage
    {
        public pageChargement()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            Console.WriteLine("ON STAAAAAAAAAAAAAAAAAAAAAAAAAAART");
            await Task.Delay(1500);
            await Navigation.PushAsync(new pageConnexion());
        }
    }
}