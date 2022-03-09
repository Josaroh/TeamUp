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

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageConnexion : ContentPage
    {
        static Utilisateur utilisateurConnecter;
        private string url = "http://10.3.229.19/Api/utilisateur.php?identifiant=";
        //private string url = "http://192.168.1.38/Api/utilisateur.php?identifiant=";

        private HttpClient client = new HttpClient();

        public pageConnexion()
        {
            InitializeComponent();
            MotDePasse.IsPassword = true;
            //permet d'ajouter l'événement du clique sur un text
            lblClickFucn();
        }

        //événement du clique sur lblclick
        void lblClickFucn()
        {

            lblclick.GestureRecognizers.Add(new TapGestureRecognizer()// "Le clique"
            {
                //commande asynchrone : elle ne se lance pas dès que la page se génére
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new pageInscription()); // attend le clique, si le lblclick est cliqué renvoie sur la page d'inscription
                })
            });
        }
        
        //action de log
        //événement du clic sur le bouton OnClickSeConnecter
        private async void OnClickSeConnecter(object sender, EventArgs e)
        {
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

            Console.WriteLine("L'identifiant est " + identifiant);
            Console.WriteLine("Le mot de passe est " + motDePasse);
            Console.WriteLine("L'URL est " + url);
            Console.WriteLine("L'utilisateur est " + utilisateur);

            bool existe = false;

            foreach(Utilisateur user in utilisateur)
            {
                existe = true;
                App.utilisateur = user;
                Console.WriteLine("La liste est " + user.identifiant);
            }

            if(existe == true)
            {
                utilisateurConnecter.identifiant = identifiant;
                await Navigation.PushAsync(new pageAccueil()); // renvoie sur la page d'accueil
            }
            else
            {
                DisplayAlert("Identifiant erroné", "Veuillez saisir à nouveau votre identifiant", "Ok");
                return;
            }
        }

        //événement quand l'utilisateur a entré son mot de passe
        private void Entry_Completed(object sender, EventArgs e)
        {
            var editor = (Entry)sender; //initialise l'objet editor pour avoir accès aux attributs 

            //motDePasse.Text = "Le mot de passe est : " + editor.Text; // affiche le text
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