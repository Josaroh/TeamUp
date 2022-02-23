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
    public partial class pageSupprTeammates : ContentPage
    {
        public pageSupprTeammates()
        {
            InitializeComponent();
        }
        private async void OnClickSupprTeammate(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageSupprTeammate()); // renvoie sur la page d'accueil
        }
    }
}