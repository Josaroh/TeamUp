using System;
using TeamUp.Services;
using TeamUp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Rubik-Regular.ttf", Alias = "rubik")]
[assembly: ExportFont("Rubik-Bold.ttf", Alias = "rubikB")]
[assembly: ExportFont("Rubik-Italic.ttf", Alias = "rubikI")]

namespace TeamUp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new UtilisateurPage())
            {
                BarBackgroundColor = Color.FromHex("#24b6ff")
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
