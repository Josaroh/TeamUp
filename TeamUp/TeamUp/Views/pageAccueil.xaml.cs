using System;

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
    }
}