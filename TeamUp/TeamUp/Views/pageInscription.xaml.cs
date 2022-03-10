using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Collections.ObjectModel;
using TeamUp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageInscription : ContentPage
    {
        private string url = "http://gestionlocation.ddns.net/utilisateur.php";
        //private string url = "http://10.3.229.19/Api/utilisateurs";
        //private string url = "http://192.168.1.38/Api/utilisateurs";

        private HttpClient client = new HttpClient();
        private ObservableCollection<Utilisateur> _utilisateurs;
        public pageInscription()
        {
            InitializeComponent();
            MotDePasse.IsPassword = true;
        }


        public void OnDateSelected(object sender, EventArgs e)
        {

        }

        private async void OnClickConnexion(object sender, EventArgs e)
        {
            var nom = Nom.Text;
            var prenom = Prenom.Text;
            var dateNaiss = startDatePicker.Date.ToString().Substring(0, 10);
            var identifiant = Identifiant.Text;
            var mail = Mail.Text;
            var motDePasse = MotDePasse.Text;


            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(dateNaiss) || string.IsNullOrEmpty(identifiant) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(motDePasse))
            {
                DisplayAlert("Un ou plusieurs champs vides", "Veuillez saisir les informations manquantes", "Ok");
                return;
            }

            // vérification si identifiant existe

            var contentUtilisateur = await client.GetStringAsync(url);
            var utilisateur = JsonConvert.DeserializeObject<List<Utilisateur>>(contentUtilisateur);

            bool existe = false;

            foreach (Utilisateur user in utilisateur)
            {
                if(user.identifiant == identifiant || user.email == mail) 
                { 
                    existe = true; 
                }
            }

            if (existe == false)
            {
                // création du compte et ajout dans la BD

                string contentType = "application/json";

                JObject json = new JObject
                {
                    { "identifiant", identifiant },
                    { "nom", nom },
                    { "prenom", prenom },
                    { "date_naissance", dateNaiss },
                    { "email", mail },
                    { "mot_de_passe", motDePasse }
                };

                var content = new StringContent(json.ToString(), Encoding.UTF8, contentType);
                await client.PostAsync(url, content);

                await Navigation.PushAsync(new pageConnexion()); // renvoie sur la page de connexion
            }
            else
            {
                DisplayAlert("Identifiant ou adresse mail déjà existants", "Veuillez saisir un nouvel identifiant ou une nouvelle adresse mail", "Ok");
                return;
            }
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