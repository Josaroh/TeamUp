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
    public partial class pageConsultationActPayante : ContentPage
    {
        private string urlAct = "http://gestionlocation.ddns.net/activite.php?id=";
        private string urlTeam = "http://gestionlocation.ddns.net/teammates.php?idActivite=";
        private string urlTeammates = "http://gestionlocation.ddns.net/teammates.php";
        public string idPage;
        private HttpClient clientAct = new HttpClient();
        private HttpClient clientTeam = new HttpClient();
        private string nbMaxTeam;
        private int cptTeam = 0;

        public pageConsultationActPayante(string id)
        {
            InitializeComponent();
            urlAct += id;
            urlTeam += id;
            idPage = id;
        }

        protected override void OnDisappearing()
        {
            LstTeammates.Clear();
            cptTeam = 0;
        }

        protected async override void OnAppearing()
        {
            var contentAct = await clientAct.GetStringAsync(urlAct);
            var activite = JsonConvert.DeserializeObject<List<Activite>>(contentAct);

            foreach (Activite act in activite)
            {
                Date.Text = act.date;
                Nom.Text = act.titre;
                Lieu.Text = act.lieu;
                Horaires.Text = act.heure_debut + " - " + act.heure_fin;
                Niveau.Text = act.niveau;
                nbMaxTeam = act.nbr_participant;
                Tarif.Text = act.prix + "€";
                var urlTL = "http://gestionlocation.ddns.net/utilisateur.php?id=";
                urlTL += act.a_pour_team_leader_id;
                var contentTL = await clientTeam.GetStringAsync(urlTL);
                var teamLeader = JsonConvert.DeserializeObject<List<Utilisateur>>(contentTL);
                var teamL = teamLeader.Find(x => x.id.Contains(act.a_pour_team_leader_id));
                LstTeammates.Add(new TextCell { Text = teamL.identifiant });
                cptTeam++;
            }

            var contentTeam = await clientTeam.GetStringAsync(urlTeam);
            var teammate = JsonConvert.DeserializeObject<List<Teammate>>(contentTeam);

            foreach (Teammate team in teammate)
            {
                var urlUser = "http://gestionlocation.ddns.net/utilisateur.php?id=";
                urlUser += team.utilisateur_id;
                var contentUser = await clientTeam.GetStringAsync(urlUser);
                var utilisateur = JsonConvert.DeserializeObject<List<Utilisateur>>(contentUser);
                var user = utilisateur.Find(x => x.id.Contains(team.utilisateur_id));
                LstTeammates.Add(new TextCell { Text = user.identifiant });
                cptTeam++;
            }

            string teamInscrits = "Teammates inscrits " + cptTeam.ToString() + "/" + nbMaxTeam;
            NbTeam.Text = teamInscrits;
        }

        private async void OnClickModifActPayante(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageModifActPayante(idPage));
        }

        private async void OnClickSupprActPayante(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation de la suppression", "Souhaitez-vous supprimer cette activité ?", "Non", "Oui");

            if (!answer)
            {
                _ = Navigation.PopAsync();
            }
        }

        private async void OnClickSupprTeammates(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageSupprTeammates(idPage));
        }

        private async void OnClickInscriptionActPayante(object sender, EventArgs e)
        {
            if (cptTeam == int.Parse(nbMaxTeam))
            {
                DisplayAlert("Team complète", "Vous ne pouvez plus rejoindre cette activité", "Ok");
                btn_inscr.IsEnabled = false;
            }
            else
            {
                string contentType = "application/json";
                JObject json = new JObject
                {
                    { "activite_id", idPage },
                    { "utilisateur_id", App.utilisateur.id }
                };

                var content2 = new StringContent(json.ToString(), Encoding.UTF8, contentType);
                await clientTeam.PostAsync(urlTeammates, content2);
                await Navigation.PushAsync(new pageActPayanteRejointe(this.idPage));
            }
        }
    }
}