using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using TeamUp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageSupprTeammates : ContentPage
    {
        private string urlTeam = "http://gestionlocation.ddns.net/teammates.php?idActivite=";
        private HttpClient clientTeam = new HttpClient();
        private ObservableCollection<Utilisateur> _utilisateur = new ObservableCollection<Utilisateur>();

        public pageSupprTeammates(string id)
        {
            InitializeComponent();
            urlTeam += id;
        }

        protected async override void OnAppearing()
        {
            var contentTeam = await clientTeam.GetStringAsync(urlTeam);
            var teammate = JsonConvert.DeserializeObject<List<Teammate>>(contentTeam);
            foreach (Teammate team in teammate)
            {
                var urlUser = "http://gestionlocation.ddns.net/utilisateur.php?id=";
                urlUser += team.utilisateur_id;
                var contentUser = await clientTeam.GetStringAsync(urlUser);
                var utilisateur = JsonConvert.DeserializeObject<List<Utilisateur>>(contentUser);
                foreach(Utilisateur unUtilisateur in utilisateur)
                {
                    _utilisateur.Add(unUtilisateur);
                }
            }
            lv_suppression.ItemsSource = _utilisateur;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Utilisateur tappedItem = (Utilisateur)e.Item;
            bool supprT = await DisplayAlert("Supprimer le teammate", "Voulez-vous vraiment supprimer le teammate ?", "Non", "Oui");
            string urlSuppr = "";

            if (!supprT)
            {
                await clientTeam.DeleteAsync(urlTeam);

                var contentTeam = await clientTeam.GetStringAsync(urlTeam);
                var teammate = JsonConvert.DeserializeObject<List<Teammate>>(contentTeam);

                foreach (Teammate team in teammate)
                {
                    urlSuppr += urlTeam + "&idUtilisateur=" + team.utilisateur_id;
                    await clientTeam.DeleteAsync(urlSuppr);
                }
                _ = Navigation.PopAsync();
            }
        }
    }
}