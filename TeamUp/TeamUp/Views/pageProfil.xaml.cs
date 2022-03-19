using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TeamUp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageProfil : ContentPage
    {
        private HttpClient client = new HttpClient();
        public pageProfil()
        {
            InitializeComponent();

            lblClickFucn();
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

        protected async override void OnAppearing()
        {
            Identifiant.Text = App.utilisateur.identifiant;
            Nom.Text = App.utilisateur.nom;
            Prenom.Text = App.utilisateur.prenom;
            // manque date
            IdentifiantBis.Text = App.utilisateur.identifiant;
            Mail.Text = App.utilisateur.email;
            string url = "http://gestionlocation.ddns.net/profil.php?id=";
            url += App.utilisateur.profil_id;
            var content =  await client.GetStringAsync(url);
            var profil = JsonConvert.DeserializeObject<List<Profil>>(content);

            foreach (Profil pro in profil)
            {
                Localisation.Text = pro.localisation;
            }
        }

        void lblClickFucn()
        {
            lblclick.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new PageChangementDeMotDePasse());
                })
            });
        }

        public void OnDateSelected(object sender, EventArgs e)
        {

        }

        private async void OnClickDeconnexion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageConnexion()); 
        }

        private async void OnClickMdpOublie(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageMotDePasseOublie());
        }
    }
}