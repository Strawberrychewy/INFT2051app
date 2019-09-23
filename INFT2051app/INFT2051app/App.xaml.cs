using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace INFT2051app
{
    public partial class App : Application {
        int Steps;

        private readonly MainPage gamePage;
        public App() {
            InitializeComponent();


            //App.Current.MainPage = new Models.OpenPage();
            gamePage = new MainPage();
            MainPage = gamePage;
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
            Application.Current.Properties["Credits"] = gamePage.credits;
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
                gamePage.credits = creditsSaved + Steps;
                Steps = 0;
            }
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e) {
            // Process shake event
            Steps++;
        }
    }
}
