using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageActPayanteRejointe : ContentPage
    {
        public pageActPayanteRejointe(string id)
        {
            InitializeComponent();
            TextCell textCell = new TextCell();

            textCell.Text = App.utilisateur.identifiant;

            textCell.TextColor = Color.Black;

            Tableau.Add(textCell);
        }

        private async void OnClickModifActPayante(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageModifActGratuite());
        }

        private async void OnClickSupprActPayante(object sender, EventArgs e)
        {
            await DisplayAlert("Confirmation de la suppression", "Souhaitez-vous supprimer cette activité ?", "Non", "Oui");
        }

        private async void OnClickSupprTeammates(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageSupprTeammates());
        }

        private async void OnClickDesinscriptionActPayante(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation de la désinscription", "Souhaitez-vous quitter cette activité ?", "Non", "Oui");

            if (!answer)
            {
                _ = Navigation.PopAsync();
            }
        }

        private async void OnClickMessAccueil(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageMessAccueil());
        }
    }
}