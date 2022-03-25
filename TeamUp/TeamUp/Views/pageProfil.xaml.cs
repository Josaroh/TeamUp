using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TeamUp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageProfil : ContentPage
    {
        private string url = "http://gestionlocation.ddns.net/utilisateur.php?id=" + App.utilisateur.id;
        private string urlProfil = "http://gestionlocation.ddns.net/profil.php?id=" + App.utilisateur.profil_id;
        private HttpClient client = new HttpClient();
        private HttpClient client2 = new HttpClient();

        public pageProfil()
        {
            InitializeComponent();

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
            DateNaiss.Text = App.utilisateur.date_naissance;
            IdentifiantBis.Text = App.utilisateur.identifiant;
            Mail.Text = App.utilisateur.email;
            MotDePasse.Text = App.utilisateur.mot_de_passe;
            
            string url = "http://gestionlocation.ddns.net/profil.php?id=";
            url += App.utilisateur.profil_id;
            
            var content =  await client.GetStringAsync(url);
            var profil = JsonConvert.DeserializeObject<List<Profil>>(content);

            foreach (Profil pro in profil)
            {
                Localisation.Text = pro.localisation;
            }

            slider.ValueChanged += (sender, args) =>
            {
                if(args.NewValue < 10)
                {
                    Etiquette.Text = String.Format("0{0} km", (int)args.NewValue);
                }
                else
                {
                    Etiquette.Text = String.Format("{0} km", (int)args.NewValue);
                }
            };
        }

        private async void OnClickDeconnexion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageConnexion()); 
        }

        private async void OnClickEnregistrer(object sender, EventArgs e)
        {
            // partie UTILISATEUR
            var identifiant = IdentifiantBis.Text;
            var nom = Nom.Text;
            var prenom = Prenom.Text;
            var dateNaiss = DateNaiss.Text;
            var mail = Mail.Text;
            var motDePasse = MotDePasse.Text;

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
            await client.PutAsync(url, content);

            App.utilisateur.identifiant = identifiant;
            App.utilisateur.nom = nom;
            App.utilisateur.prenom = prenom;
            App.utilisateur.date_naissance = dateNaiss;
            App.utilisateur.email = mail;
            App.utilisateur.mot_de_passe = motDePasse;

            /* partie PROFIL A RESOUDRE
            var localisation = Localisation.Text;
            var perimetre = Etiquette.Text;
            var preference = picker.SelectedItem.ToString();

            string contentType2 = "application/json";

            JObject json2 = new JObject
            {
                { "localisation", localisation },
                { "perimetre", perimetre },
                { "preference", preference }
            };

            var content2 = new StringContent(json2.ToString(), Encoding.UTF8, contentType2);
            await client2.PutAsync(urlProfil, content2);*/

            await Navigation.PushAsync(new pageAccueil());
        }
    }
}