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
    public partial class pageConsultationActPayante : ContentPage
    {
        public pageConsultationActPayante()
        {
            InitializeComponent();
        }

        private async void OnClickModifActPayante(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageModifActPayante());
        }

        private async void OnClickSupprActPayante(object sender, EventArgs e)
        {
            await DisplayAlert("Confirmation de la suppression", "Souhaitez-vous supprimer cette activité ?", "Non", "Oui");
        }

        private async void OnClickSupprTeammates(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageSupprTeammates());
        }

        private async void OnClickInscriptionActPayante(object sender, EventArgs e)
        {
            await DisplayAlert("Confirmation de l'inscription", "Souhaitez-vous participer à cette activité ?", "Non", "Oui");
        }
    }
}