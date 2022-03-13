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
    public partial class pageSuiviAct : ContentPage
    {
        private string url = "http://gestionlocation.ddns.net/activite.php?idTeamLeader=" + App.utilisateur.id;
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Activite> _activite;


        public pageSuiviAct()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var content = await _client.GetStringAsync(url);
            var activites = JsonConvert.DeserializeObject<List<Activite>>(content);
            _activite = new ObservableCollection<Activite>(activites);
            listViewActivite.ItemsSource = _activite;
            base.OnAppearing();
        }
    }
}