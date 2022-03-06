using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageAccueil : ContentPage
    {
        public pageAccueil()
        {
            InitializeComponent();

            var ListeSport = new List<string>();
            ListeSport.Add("Tennis");
            ListeSport.Add("Surf");
            ListeSport.Add("Basket");

            pickerSport.ItemsSource = ListeSport;

            var ListeLieu = new List<string>();
            ListeLieu.Add("Bayonne");
            ListeLieu.Add("Anglet");
            ListeLieu.Add("Biarritz");

            pickerLieu.ItemsSource = ListeLieu;
        }

        private async void OnClickProfil(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageProfil()); // renvoie sur la page d'accueil
        }

        private async void OnClickActGratuite(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageConsultationActGratuite()); // renvoie sur la page d'accueil
        }

        private async void OnClickActPayante(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageConsultationActPayante()); // renvoie sur la page d'accueil
        }

        private async void OnClickCreationAct(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageCreationAct()); // renvoie sur la page d'accueil
        }

        private async void OnClickMessAccueil(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageMessAccueil()); // renvoie sur la page d'accueil
        }

        private async void OnClickSuiviAct(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageSuiviAct()); // renvoie sur la page d'accueil
        }
    }
}