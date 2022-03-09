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
    public partial class pageModifActGratuite : ContentPage
    {
        public pageModifActGratuite()
        {
            InitializeComponent();
        }

        public void OnDateSelected(object sender, EventArgs e)
        {

        }

        private async void OnClickActGratuite(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageConsultationActGratuite()); // renvoie sur la page d'accueil
        }
    }
}