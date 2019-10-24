using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using MediaManager;


namespace INFT2051app
{
    public partial class App : Application {

        int Steps;
        private readonly MainPage gamePage;


        public App() {
            InitializeComponent();

            
            gamePage = new MainPage();
            MainPage = new NavigationPage(new OpenPage(gamePage));

        }

        //private async void playMusic(object sender, EventArgs e)
        //{

        //    await CrossMediaManager.Current.PlayFromAssembly("appMusic.wav", typeof(MainPage).Assembly);
        //}


        protected override void OnStart() {
            // Handle when your app starts
            /*
             * Use this method to do these following things:
             * 1. Load player save data to be used if save data is present, 
             * 2. Create player save data to be used if no save data is present
             */
            

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
                Steps = 0;
            }
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e) {
            // Process shake event
            Steps++;
        }
    }
}
