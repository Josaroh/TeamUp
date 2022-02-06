using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
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

            identifiant.Text = App.visiteur.Identifiant;
        }

        private async void OnClickProfil(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageProfil()); // renvoie sur la page d'accueil
        }

        private async void OnClickActGratuite(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageConsultActGratuite()); // renvoie sur la page d'accueil
        }

        private async void OnClickActPayante(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageConsultActPayante()); // renvoie sur la page d'accueil
        }
    }
}