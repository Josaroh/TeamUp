using System;
using TeamUp.Services;
using TeamUp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.Models;

[assembly: ExportFont("Rubik-Regular.ttf", Alias = "rubik")]
[assembly: ExportFont("Rubik-Bold.ttf", Alias = "rubikB")]
[assembly: ExportFont("Rubik-Italic.ttf", Alias = "rubikI")]

namespace TeamUp
{
    public partial class App : Application
    {
        public static Utilisateur utilisateur { get; set; }
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new pageConnexion())
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
