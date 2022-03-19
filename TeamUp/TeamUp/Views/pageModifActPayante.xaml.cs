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
    public partial class pageModifActPayante : ContentPage
    {
        private string urlAct = "http://gestionlocation.ddns.net/activite.php?id=";
        private HttpClient client = new HttpClient();

        public pageModifActPayante(string id)
        {
            InitializeComponent();

            var ListeNiveau = new List<string>();
            ListeNiveau.Add("Débutant");
            ListeNiveau.Add("Amateur");
            ListeNiveau.Add("Avancé");

            Picker.ItemsSource = ListeNiveau;
            urlAct += id;
        }

        protected async override void OnAppearing()
        {
            var contentAct = await client.GetStringAsync(urlAct);
            var activite = JsonConvert.DeserializeObject<List<Activite>>(contentAct);

            foreach (Activite act in activite)
            {
                Nom.Text = act.titre;
                Lieu.Text = act.lieu;
                Horaire.Text = act.heure_debut + " - " + act.heure_fin;
                nbMax.Text = act.nbr_participant;
                Tarif.Text = act.prix;
            }
        }
        public void OnDateSelected(object sender, EventArgs e)
        {

        }

        private async void OnClickActPayante(object sender, EventArgs e)
        {
            var dateAct = startDatePicker.Date.ToString().Substring(0, 10);
            var nom = Nom.Text;
            var lieu = Lieu.Text;
            var tarif = Tarif.Text;
            var nbTeam = nbMax.Text;

            if (string.IsNullOrEmpty(dateAct) || string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(lieu) || string.IsNullOrEmpty(Horaire.Text) || string.IsNullOrEmpty(Picker.SelectedItem.ToString()) || string.IsNullOrEmpty(nbTeam))
            {
                DisplayAlert("Un ou plusieurs champs obligatoires vides", "Veuillez saisir les informations manquantes", "Ok");
                return;
            }

            string horaires = Horaire.Text.ToString();
            int taille = horaires.Length;

            if (taille != 13)
            {
                DisplayAlert("Problème de format", "Veuillez saisir de nouveaux horaires respectant la nomenclature 00h00 - 00h00", "Ok");
                return;
            }

            var heureDeb = Horaire.Text.Substring(0, 5);
            var heureFin = Horaire.Text.Substring(8, 5);
            var niveau = Picker.SelectedItem.ToString();
            string contentType = "application/json";
            var actTerminee = "false";

            JObject json = new JObject
            {
                { "a_pour_team_leader_id", App.utilisateur.id },
                { "titre", nom },
                { "date", dateAct },
                { "heure_debut", heureDeb },
                { "heure_fin", heureFin },
                { "lieu", lieu },
                { "prix", tarif },
                { "niveau", niveau },
                { "nbr_participant", nbTeam },
                { "activite_terminee", actTerminee }
            };

            var content2 = new StringContent(json.ToString(), Encoding.UTF8, contentType);
            await client.PutAsync(urlAct, content2);
            await Navigation.PushAsync(new pageAccueil());
        }
    }
}