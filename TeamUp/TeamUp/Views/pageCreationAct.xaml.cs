using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pageCreationAct : ContentPage
    {
        public pageCreationAct()
        {
            InitializeComponent();

            var ListeNiveau = new List<string>();
            ListeNiveau.Add("Débutant");
            ListeNiveau.Add("Amateur");
            ListeNiveau.Add("Avancé");

            picker.ItemsSource = ListeNiveau;
        }

        public void OnDateSelected(object sender, EventArgs e)
        {

        }
    }
}