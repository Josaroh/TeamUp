using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private string url = "http://192.168.1.38/Api/utilisateurs";  //changer l'ip avec votre ip: ouvrir le cmd et lancer la commande ipconfig. 
                                                                                      //Copier coller l'ipv4.
        private HttpClient _client = new HttpClient();
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
            //mettre condition mot de passe et identifiant
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

            //Utilisateur utilisateur = new Utilisateur(identifiant, nom, prenom, dateNaiss, mail, motDePasse);

            //Console.WriteLine("Nathan est relou " + utilisateur.toString());

            //String jsonUtilisateur = JsonConvert.SerializeObject(utilisateur);

            //var data = "{ \"identifiant\" : \""+identifiant+"\" , \"nom\" : \"" + nom + "\" , \"prenom\" : \"" + prenom + "\" , \"date_naissance\" : \"" + dateNaiss + "\" , \"email\" : \"" + mail + "\" , \"mot_de_passe\" : \"" + motDePasse + "\"}";

            //JObject json = JObject.Parse(data);

            //var contentRequest = new StringContent(json, Encoding.UTF8, "application/json");

            //Console.WriteLine("woa trop beau ce json " + data);

            //await _client.PostAsync(url, contentRequest);

            //Console.WriteLine("Nathan est relou " + jsonUtilisateur);

            //HttpContent content = new StringContent(jsonUtilisateur);

            //await _client.PostAsync(url, content);



            await Navigation.PushAsync(new pageConnexion());
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