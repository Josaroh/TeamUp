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
    public partial class pageProfil : ContentPage
    {
        public pageProfil()
        {
            InitializeComponent();

            var ListeSports = new List<string>();
            ListeSports.Add("Boxe");
            ListeSports.Add("Surf");
            ListeSports.Add("Pala");
            ListeSports.Add("Course à pied");
            ListeSports.Add("Yoga");
            ListeSports.Add("Rugby");
            ListeSports.Add("Tennis");

            picker.ItemsSource = ListeSports;
        }
        public void OnDateSelected(object sender, EventArgs e)
        {

        }
        private async void OnClickDeconnexion(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageConnexion()); // renvoie sur la page d'accueil
        }
    }
}