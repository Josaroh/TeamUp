using System;
using System.IO;
using Xamarin.Essentials;
using TeamUp.Services;
using TeamUp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.Repositories;
using TeamUp.Models;

namespace TeamUp
{
    public partial class App : Application
    {
        private string dbPath = Path.Combine(FileSystem.AppDataDirectory,"database.db3");

        public static VisiteurRepository VisiteurRepository { get; set; }

        public static Visiteur visiteur { get; set; }



        public App()
        {
            InitializeComponent();

            VisiteurRepository = new VisiteurRepository(dbPath);

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
