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
    public partial class pageInscription : ContentPage
    {
        public pageInscription()
        {
            InitializeComponent();
        }



        public void OnDateSelected(object sender, EventArgs e)
        {

        }

        private async void créerCompte(object sender, EventArgs e)
        {
            statusMessage.Text = "";


            if ( string.IsNullOrEmpty(identifiant.Text) || string.IsNullOrWhiteSpace(identifiant.Text) ||
                 string.IsNullOrEmpty(nom.Text) || string.IsNullOrWhiteSpace(nom.Text) ||
                 string.IsNullOrEmpty(prenom.Text) || string.IsNullOrWhiteSpace(prenom.Text) ||
                 string.IsNullOrEmpty(dateNaissance.ToString()) || string.IsNullOrWhiteSpace(dateNaissance.ToString()) ||
                 string.IsNullOrEmpty(email.Text) || string.IsNullOrWhiteSpace(email.Text) ||
                 string.IsNullOrEmpty(motDePasse.Text) || string.IsNullOrWhiteSpace(motDePasse.Text))
            {
                statusMessage.Text = "Veuillez renseigner tous les champs";
            }
            else
            {
                await App.VisiteurRepository.AddNewUserAsync(identifiant.Text, nom.Text, prenom.Text, dateNaissance.ToString(), email.Text, motDePasse.Text);

                statusMessage.Text = App.VisiteurRepository.StatusMessage;
            }


            
        }


       
    }
}