using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;


namespace INFT2051app
{
    public partial class App : Application {



        private int Steps;
        private readonly MainPage gamePage = new MainPage();
        public static string savedata;

        public App() {
            InitializeComponent();
            MainPage = new NavigationPage(new OpenPage(gamePage));
        }

        public App(string save) {
            InitializeComponent();
            MainPage = new NavigationPage(new OpenPage(gamePage));
            savedata = save;
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {

        }

        protected override void OnResume() {
        }
    }
}
