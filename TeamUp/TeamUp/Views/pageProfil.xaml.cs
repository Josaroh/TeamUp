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
        private string url = "http://appteamup.ddns.net/utilisateur.php?id=" + App.utilisateur.id;
        private string urlProfil = "http://appteamup.ddns.net/profil.php?id=" + App.utilisateur.profil_id;
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
            
            string url = "http://appteamup.ddns.net/profil.php?id=";
            url += App.utilisateur.profil_id;
            
            var content =  await client.GetStringAsync(url);
            var profil = JsonConvert.DeserializeObject<List<Profil>>(content);

            foreach (Profil pro in profil)
            {
                Localisation.Text = pro.localisation;

                string[] tab = pro.perimetre.Split(' ');

                Etiquette.Text = tab[0] + " km";

                double value = Convert.ToDouble(tab[0]);

                slider.Value = value;

                picker.SelectedItem = pro.preference;
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

            Console.WriteLine("C'est parti!");

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


            Console.WriteLine("Partie Profile1");

            //partie PROFIL A RESOUDRE
            var localisation = Localisation.Text;
            Console.WriteLine("Partie Profile2");
            var perimetre = Etiquette.Text;
            Console.WriteLine("Partie Profile3");

            string preference;

            if(picker.SelectedItem == null)
            {
                preference = "NULL";
            }
            else { 
                preference = picker.SelectedItem.ToString(); 
            }

            Console.WriteLine("Partie Profile4");



            string contentType2 = "application/json";

            Console.WriteLine("Partie Profile5");

            JObject json2 = new JObject
            {
                { "localisation", localisation },
                { "perimetre", perimetre },
                { "preference", preference }
            };

            Console.WriteLine("Partie Profile6");

            Console.WriteLine("Objet:"+json2.ToString());



            var content2 = new StringContent(json2.ToString(), Encoding.UTF8, contentType2);

            Console.WriteLine("URL:" + urlProfil);
            await client2.PutAsync(urlProfil, content2);

            await Navigation.PushAsync(new pageAccueil());
        }
    }
}