using System.ComponentModel;
using TeamUp.ViewModels;
using Xamarin.Forms;

namespace TeamUp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}