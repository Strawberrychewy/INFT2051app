using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using MediaManager;
using MediaManager.Playback;
using MediaManager.Forms.Xaml;

namespace INFT2051app
{
    public partial class App : Application {

        int Steps;
        private readonly MainPage gamePage;
        public static string savedata;

        public App() {
            InitializeComponent();
            gamePage = new MainPage();
            MainPage = new NavigationPage(new OpenPage(gamePage));
        }

        public App(string save) {
            InitializeComponent();
            gamePage = new MainPage();
            MainPage = new NavigationPage(new OpenPage(gamePage));
            savedata = save;
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
            Application.Current.Properties["Credits"] = gamePage.foodShopPopup.Credits;
            Accelerometer.Start(SensorSpeed.Game);
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
        }

        protected override void OnResume() {
            // Handle when your app resumes
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            LoadPersistedValues();
            Accelerometer.Stop();
        }
        
        private void LoadPersistedValues() {
            // Load all saved values from when the app is in sleep mode
            if (Application.Current.Properties.ContainsKey("Credits")) {
                var creditsSaved = (int)Application.Current.Properties["Credits"];
                gamePage.foodShopPopup.Credits = creditsSaved + Steps;
                gamePage.UpdateCreditsLabel();
                Steps = 0;
            }
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e) {
            // Process shake event
            Steps++;
        }
    }
}
