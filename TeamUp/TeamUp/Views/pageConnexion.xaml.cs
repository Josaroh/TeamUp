using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
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


            statusMessages.Text = "";

            if (string.IsNullOrEmpty(identifiant.Text) || string.IsNullOrWhiteSpace(identifiant.Text) ||
                 string.IsNullOrEmpty(motDePasse.Text) || string.IsNullOrWhiteSpace(motDePasse.Text))
            {
                statusMessages.Text = "Veuillez renseigner tous les champs";
            }
            else
            {
                List<Visiteur> visiteurs = await App.VisiteurRepository.GetVisiteurAsync(identifiant.Text, motDePasse.Text);

                bool isEmpty = !visiteurs.Any();
                if (isEmpty)
                {
                    statusMessages.Text = "Ce visiteur n'existe pas";
                }
                else
                {

                    Visiteur visiteur = visiteurs[0];

                    App.visiteur=visiteur;


                    await Navigation.PushAsync(new pageAccueil()); // renvoie sur la page d'accueil

                    statusMessages.Text = App.VisiteurRepository.StatusMessage;
                }




                
            }


            



            
        }

        //événement quand l'utilisateur a entré son mot de passe
      
    }
}