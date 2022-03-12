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
    public partial class pageConsultationActGratuite : ContentPage
    {
        public pageConsultationActGratuite()
        {
            InitializeComponent();
        }

        private async void OnClickModifActGratuite(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageModifActGratuite());
        }

        private async void OnClickSupprActGratuite(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation de la suppression", "Souhaitez-vous supprimer cette activité ?", "Non", "Oui");

            if (!answer)
            {
                _ = Navigation.PopAsync();
            }
        }

        private async void OnClickSupprTeammates(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageSupprTeammates());
        }

        private async void OnClickInscriptionActGratuite(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageActGratuiteRejointe());
        }
    }
}