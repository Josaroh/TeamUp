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
    public partial class voirMotDePasse : ContentView
    {
        public voirMotDePasse()
        {
            InitializeComponent();
            MotDePasse.IsPassword = true;
        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var imageButton = sender as ImageButton;
            if (MotDePasse.IsPassword)
            {
                imageButton.Source = ImageSource.FromFile("oeil.png");
                MotDePasse.IsPassword = false;
            }
            else
            {
                imageButton.Source = ImageSource.FromFile("oeil_cache.png");
                MotDePasse.IsPassword = true;
            }
        }
    }
}