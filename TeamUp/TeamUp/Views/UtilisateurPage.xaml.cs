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
    public partial class UtilisateurPage : ContentPage
    {
        private const string url = "http://192.168.1.38/Api/utilisateur.php";  //changer l'ip avec votre ip: ouvrir le cmd et lancer la commande ipconfig. 
                                                                              //Copier coller l'ipv4.
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Utilisateur> _utilisateurs;


        public UtilisateurPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var content = await _client.GetStringAsync(url);
            var utilisateurs = JsonConvert.DeserializeObject<List<Utilisateur>>(content);
            _utilisateurs = new ObservableCollection<Utilisateur>(utilisateurs);
            listViewUtilisateur.ItemsSource = _utilisateurs;
            base.OnAppearing();
        }
    }
}