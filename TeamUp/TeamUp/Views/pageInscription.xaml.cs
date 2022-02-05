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
    public partial class pageInscription : ContentPage
    {
        public pageInscription()
        {
            InitializeComponent();
        }

        public void OnDateSelected(object sender, EventArgs e)
        {

        }
    }
}