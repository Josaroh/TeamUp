using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using TeamUp.Models;
using Newtonsoft.Json;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageConnexion : ContentPage
    {
        private HttpClient client = new HttpClient();

        public pageConnexion()
        {
            InitializeComponent();
            MotDePasse.IsPassword = true;
            lblClickFucn();
        }

        void lblClickFucn()
        {
            lblclick.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new pageInscription()); 
                })
            });
        }

        private async void OnClickSeConnecter(object sender, EventArgs e)
        {
            string url = "http://gestionlocation.ddns.net/utilisateur.php?identifiant=";
            var identifiant = identifiantOrEmail.Text;
            var motDePasse = MotDePasse.Text;

            if (string.IsNullOrEmpty(identifiant) || string.IsNullOrEmpty(motDePasse))
            {
                DisplayAlert("Un ou plusieurs champs vides", "Veuillez saisir votre identifiant et votre mot de passe", "Ok");
                return;
            }

            url += identifiant;
            var content = await client.GetStringAsync(url);
            var utilisateur = JsonConvert.DeserializeObject<List<Utilisateur>>(content);
            bool existe = false;

            foreach (Utilisateur user in utilisateur)
            {
                existe = true;
                App.utilisateur = user;
            }

            if (existe == true && motDePasse == App.utilisateur.mot_de_passe)
            {
                await Navigation.PushAsync(new pageAccueil());
            }
            else
            {
                DisplayAlert("Identifiant ou mot de passe erroné", "Veuillez saisir à nouveau votre identifiant et votre mot de passe", "Ok");
                return;
            }
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            var editor = (Entry)sender;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var imageButton = sender as ImageButton;
            if (MotDePasse.IsPassword)
            {
                imageButton.Source = ImageSource.FromFile("oeil.png");
                MotDePasse.IsPassword = false;
            }
            else
            {
                imageButton.Source = ImageSource.FromFile("oeil_cache.png");
                MotDePasse.IsPassword = true;
            }
        }
    }
}