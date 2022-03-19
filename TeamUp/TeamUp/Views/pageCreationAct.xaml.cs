using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageCreationAct : ContentPage
    {
        private string urlAct = "http://gestionlocation.ddns.net/activite.php";
        private HttpClient client = new HttpClient();
        public pageCreationAct()
        {
            InitializeComponent();

            var ListeNiveau = new List<string>();
            ListeNiveau.Add("Débutant");
            ListeNiveau.Add("Amateur");
            ListeNiveau.Add("Avancé");

            Picker.ItemsSource = ListeNiveau;
        }

        public void OnDateSelected(object sender, EventArgs e)
        {

        }

        private async void OnClickCreationAct(object sender, EventArgs e)
        {
            var dateAct = startDatePicker.Date.ToString().Substring(0, 10);
            var nom = Nom.Text;
            var lieu = Lieu.Text;
            var tarif = Tarif.Text;
            var nbTeam = NbTeam.Text;

            if (string.IsNullOrEmpty(dateAct) || string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(lieu) || string.IsNullOrEmpty(Horaires.Text) || string.IsNullOrEmpty(Picker.SelectedItem.ToString()) || string.IsNullOrEmpty(nbTeam))
            {
                DisplayAlert("Un ou plusieurs champs obligatoires vides", "Veuillez saisir les informations manquantes", "Ok");
                return;
            }

            string horaires = Horaires.Text.ToString();
            int taille = horaires.Length;

            if (taille != 13)
            {
                DisplayAlert("Problème de format", "Veuillez saisir de nouveaux horaires respectant la nomenclature 00h00 - 00h00", "Ok");
                return;
            }

            var heureDeb = Horaires.Text.Substring(0, 5);
            var heureFin = Horaires.Text.Substring(8, 5);
            var niveau = Picker.SelectedItem.ToString();
            string contentType = "application/json";
            var actTerminee = "false";

            if (string.IsNullOrEmpty(tarif))
            {
                JObject json = new JObject
                {
                    { "a_pour_team_leader_id", App.utilisateur.id },
                    { "titre", nom },
                    { "date", dateAct },
                    { "heure_debut", heureDeb },
                    { "heure_fin", heureFin },
                    { "lieu", lieu },
                    { "niveau", niveau },
                    { "nbr_participant", nbTeam },
                    { "activite_terminee", actTerminee }
                };

                var content2 = new StringContent(json.ToString(), Encoding.UTF8, contentType);
                await client.PostAsync(urlAct, content2);
            }
            else
            {
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
                await client.PostAsync(urlAct, content2);
            }
            await Navigation.PushAsync(new pageAccueil()); // doit renvoyer sur la page de consultation de l'activité pageActiviteGratuite(id)
        }
    }
}