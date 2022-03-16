using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TeamUp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Map = Xamarin.Essentials.Map;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageAccueil : ContentPage
    {
        private string url = "http://gestionlocation.ddns.net/activite.php";

        private HttpClient client = new HttpClient();
        public pageAccueil()
        {
            InitializeComponent();

            

            var ListeSport = new List<string>();
            ListeSport.Add("Tennis");
            ListeSport.Add("Surf");
            ListeSport.Add("Basket");

            pickerSport.ItemsSource = ListeSport;

            var ListeLieu = new List<string>();
            ListeLieu.Add("Bayonne");
            ListeLieu.Add("Anglet");
            ListeLieu.Add("Biarritz");

            pickerLieu.ItemsSource = ListeLieu;
        }

        protected async override void OnAppearing()
        {
            var content = await client.GetStringAsync(url);
            var activite = JsonConvert.DeserializeObject<List<Activite>>(content);

            foreach (Activite act in activite)
            {
                if (act.titre == "")
                {
                    Console.WriteLine("BRUUUUUUUUH");
                }
                else
                {
                    Geocoder geoCoder = new Geocoder();

                    IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(act.lieu);
                    Position position = approximateLocations.FirstOrDefault();
                    //string coordinates = $"{position.Latitude}, {position.Longitude}";

                    Pin pin = new Pin
                    {
                        ClassId = act.id,
                        Label = act.titre,
                        Address = act.lieu,
                        Type = PinType.Place,
                        Position = new Position(position.Latitude, position.Longitude)
                        //pin.MarkerClicked = OnClickPin();
                    };
                    map.Pins.Add(pin);
                    pin.MarkerClicked += OnClickPin;
                }
            }

            //var res = await Geolocation.GetLastKnownLocationAsync();
            var res = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(10)));

            if (res != null)
            {
                Position position = new Position(res.Latitude, res.Longitude);

                Console.WriteLine($"Latitude: {res.Latitude}, Longitude: {res.Longitude}");
            }

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(res.Latitude, res.Longitude), new Distance(2000)));
            //map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(43.4775389, -1.5083868), new Distance(2000)));
        }

        private async void OnClickPin(object sender, EventArgs e)
        {
            string actId = ((Pin)sender).ClassId;

            //condition act gratuite
            bool inscrit = false;
            bool estPayant = false;
            string url = "http://gestionlocation.ddns.net/activite.php?id=" + actId;
            var content = await client.GetStringAsync(url);
            var activite = JsonConvert.DeserializeObject<List<Activite>>(content);

            foreach(var act in activite)
            {
                if (act.coute_id != null)
                {
                    estPayant = true;
                }
                if(int.Parse(act.a_pour_team_leader_id) == int.Parse(App.utilisateur.id))
                {
                    inscrit = true;
                }
            }

            url = "http://gestionlocation.ddns.net/teammates.php?idActivite=" + actId;
            content = await client.GetStringAsync(url);
            var teammates = JsonConvert.DeserializeObject<List<Teammate>>(content);

            foreach (var mates in teammates)
            {
                if (int.Parse(mates.utilisateur_id) == int.Parse(App.utilisateur.id))
                {
                    inscrit = true;
                }
            }

            if (inscrit == true && estPayant == false)
            {
                await Navigation.PushAsync(new pageActGratuiteRejointe(actId));
            }
            else if (inscrit == false && estPayant == false)
            {
                await Navigation.PushAsync(new pageConsultationActGratuite(actId));
            }

            else if (inscrit == false && estPayant == true)
            {
                await Navigation.PushAsync(new pageActPayanteRejointe(actId));
            }
            else
            {
                await Navigation.PushAsync(new pageConsultationActPayante(actId));
            }
        }

        private async void OnClickProfil(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageProfil()); // renvoie sur la page d'accueil
        }

        private async void OnClickCreationAct(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageCreationAct()); // renvoie sur la page d'accueil
        }

        private async void OnClickMessAccueil(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageMessAccueil()); // renvoie sur la page d'accueil
        }

        private async void OnClickSuiviAct(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageSuiviAct()); // renvoie sur la page d'accueil
        }
    }
}