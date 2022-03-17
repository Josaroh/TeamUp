using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class pageActGratuiteRejointe : ContentPage
    {
        private string urlAct = "http://gestionlocation.ddns.net/activite.php?id=";
        private string urlTeam = "http://gestionlocation.ddns.net/teammates.php?idActivite=";
        public string idPage;
        private bool isTeamLeader = false;

        private HttpClient client = new HttpClient();

        private List<string> listUser = new List<string>();
        private string nbMaxTeam;
        private int cptTeam = 0;
       


        public pageActGratuiteRejointe(string id)
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
            var contentAct = await client.GetStringAsync(urlAct);
            var activite = JsonConvert.DeserializeObject<List<Activite>>(contentAct);

            foreach (Activite act in activite)
            {
                Date.Text = act.date;
                Nom.Text = act.titre;
                Lieu.Text = act.lieu;
                Horaires.Text = act.heure_debut + " - " + act.heure_fin;
                Niveau.Text = act.niveau;
                nbMaxTeam = act.nbr_participant;

                if (act.a_pour_team_leader_id == App.utilisateur.id)
                {
                    isTeamLeader = true;
                }
                else
                {
                    Item1.IsEnabled = false;
                    Item2.IsEnabled = false;
                    Item3.IsEnabled = false;
                }


                var urlTL = "http://gestionlocation.ddns.net/utilisateur.php?id=";
                    urlTL += act.a_pour_team_leader_id;

                    var contentTL = await client.GetStringAsync(urlTL);
                    var teamLeader = JsonConvert.DeserializeObject<List<Utilisateur>>(contentTL);
                    var teamL = teamLeader.Find(x => x.id.Contains(act.a_pour_team_leader_id));
                    LstTeammates.Add(new TextCell { Text = teamL.identifiant });
                    cptTeam++;
                 

            }

        
                var contentTeam = await client.GetStringAsync(urlTeam);
                var teammate = JsonConvert.DeserializeObject<List<Teammate>>(contentTeam);

                foreach (Teammate team in teammate)
                {
                    var urlUser = "http://gestionlocation.ddns.net/utilisateur.php?id=";

                    urlUser += team.utilisateur_id;

                    var contentUser = await client.GetStringAsync(urlUser);
                    var utilisateur = JsonConvert.DeserializeObject<List<Utilisateur>>(contentUser);
                    var user = utilisateur.Find(x => x.id.Contains(team.utilisateur_id));
                    LstTeammates.Add(new TextCell { Text = user.identifiant });
                    cptTeam++;
                }

                string teamInscrits = "Teammates inscrits " + cptTeam.ToString() + "/" + nbMaxTeam;
                NbTeam.Text = teamInscrits;
               



        }

        private async void OnClickModifActGratuite(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageModifActGratuite(idPage));
        }

        private async void OnClickSupprActGratuite(object sender, EventArgs e)
        {
            
            bool answer = await DisplayAlert("Confirmation de la suppression", "Souhaitez-vous supprimer cette activité ?", "Non", "Oui");
            string urlSuppr = "";

            if (!answer)
            {

                await client.DeleteAsync(urlAct);

                var contentTeam = await client.GetStringAsync(urlTeam);
                var teammate = JsonConvert.DeserializeObject<List<Teammate>>(contentTeam);



                foreach (Teammate team in teammate)
                {
                    urlSuppr += urlTeam + "&idUtilisateur=" + team.utilisateur_id;
                    await client.DeleteAsync(urlSuppr);
                }


                _ = Navigation.PopAsync();
            }
        }

        private async void OnClickSupprTeammates(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageSupprTeammates());
        }

        private async void OnClickDesinscriptionActGratuite(object sender, EventArgs e)
        {

            if (isTeamLeader == true)
            {
                await DisplayAlert("Vous êtes Team Leader !","Vous ne pouvez pas vous désinscrire de votre propre activité", "Ok");
                btn.IsEnabled = false;
            }
            else
            {
                bool answer = await DisplayAlert("Confirmation de la désinscription", "Souhaitez-vous quitter cette activité ?", "Non", "Oui");

                string urlSuppr = "";

                if (!answer)
                {

                    urlSuppr += urlTeam + "&idUtilisateur=" + App.utilisateur.id;
                    await client.DeleteAsync(urlSuppr);

                    _ = Navigation.PopAsync();
                }

            }


            
        }

        private async void OnClickMessAccueil(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pageMessAccueil());
        }
    }
}