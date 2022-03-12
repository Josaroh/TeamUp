using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            //trop chiant les dates sa mère
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

            lblclick.GestureRecognizers.Add(new TapGestureRecognizer()// "Le clique"
            {
                //commande asynchrone : elle ne se lance pas dès que la page se génére
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new PageChangementDeMotDePasse()); // attend le clique, si le lblclick est cliqué renvoie sur la page d'inscription
                })
            });
        }


        public void OnDateSelected(object sender, EventArgs e)
        {

        }
        private async void OnClickDeconnexion(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageConnexion()); // renvoie sur la page d'accueil
        }
    }
}