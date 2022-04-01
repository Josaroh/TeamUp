using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Collections.ObjectModel;
using TeamUp.Models;
using Newtonsoft.Json;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageSuiviAct : ContentPage
    {
        private string urlTL = "http://appteamup.ddns.net/activite.php?idTeamLeader=" + App.utilisateur.id;
        private string urlTeammate = "http://appteamup.ddns.net/teammates.php?idUtilisateur=" + App.utilisateur.id;
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Activite> _activite;
        private ObservableCollection<Activite> _activiteR = new ObservableCollection<Activite>();

        public pageSuiviAct()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var contentTL = await _client.GetStringAsync(urlTL);
            var activitesTL = JsonConvert.DeserializeObject<List<Activite>>(contentTL);
            _activite = new ObservableCollection<Activite>(activitesTL);
            listViewActiviteC.ItemsSource = _activite;
            listViewActiviteC.BindingContext = this;

            var contentTM = await _client.GetStringAsync(urlTeammate);
            var activitesTM = JsonConvert.DeserializeObject<List<Teammate>>(contentTM);
            _activiteR.Clear();

            foreach(Teammate unTeammate in activitesTM)
            {
                var urlActivite = "http://gestionlocation.ddns.net/activite.php?id=";
                urlActivite += unTeammate.activite_id;
                var contentActivite = await _client.GetStringAsync(urlActivite);
                var activites = JsonConvert.DeserializeObject<List<Activite>>(contentActivite);
                _activite = new ObservableCollection<Activite>(activites);

                foreach(Activite act in _activite)
                {
                    _activiteR.Add(act);
                }
            }
            listViewActiviteR.ItemsSource = _activiteR;
            listViewActiviteR.BindingContext = this;
            base.OnAppearing();
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Activite tappedItem = (Activite) e.Item;
            if(tappedItem.prix != null)
            {
                await Navigation.PushAsync(new pageActPayanteRejointe(tappedItem.id));
            }
            else
            {
                await Navigation.PushAsync(new pageActGratuiteRejointe(tappedItem.id));
            }
        }
    }
}