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
    public partial class pageConnexion : ContentPage
    {
        public pageConnexion()
        {
            InitializeComponent();

            //permet d'ajouter l'événement du clique sur un text
            lblClickFucn();
        }

        //événement du clique sur lblclick
        void lblClickFucn()
        {

            lblclick.GestureRecognizers.Add(new TapGestureRecognizer()// "Le clique"
            {
                //commande asynchrone : elle ne se lance pas dès que la page se génére
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new pageInscription()); // attend le clique, si le lblclick est cliqué renvoie sur la page d'inscription
                })
            });
        }

        //événement du clique sur le bouton OnClickSeConnecter
        private async void OnClickSeConnecter(object sender, EventArgs e)
        {
            //mettre condition mot de passe et identifiant

            await Navigation.PushAsync(new pageAccueil()); // renvoie sur la page d'accueil
        }

        //événement quand l'utilisateur a entré son mot de passe
        private void Entry_Completed(object sender, EventArgs e)
        {
            var editor = (Entry)sender; //initialise l'objet editor pour avoir accès aux attributs 

            motDePasse.Text = "Le mot de passe est : " + editor.Text; // affiche le text
        }
    }
}