using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Windows.Input;
using FFImageLoading.Forms;
using Rg.Plugins.Popup.Services;
using System.Timers;
using System.Reflection;
using System.IO;
using INFT2051app.ViewModels;
using MediaManager;
//using INFT2051app.Services;

namespace INFT2051app {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        /*
         * This is the Main page for the Application, but not the root. The root is the App class
         * This page handles two different timings
         * - one for frames (INSTANTANEOUS animations and input controls)
         * - one for real world time (EVENTS hunger and age)
         * It is important to split these two apart as there is both a slow and fast element in this application
         * 
         */

        //private readonly FamiliarsList familiarsList;    //Contains the entire Pet list
        public PopupFoodShop foodShopPopup;
        private readonly PopupOptions optionsPopup;
        private readonly PopupStatus statusPopup;
        readonly PetContainer petContainer;  //Controller for the pet

        readonly Timer gameloop;

        private readonly Label creditsLabel;
        private readonly Label debugLabel;
        private readonly Button FPButton;
        private readonly ProgressBar progressBar;

        public MainPage() {
            InitializeComponent();

            petContainer = new PetContainer();

            optionsPopup = new PopupOptions();
            statusPopup = new PopupStatus();
            foodShopPopup = new PopupFoodShop();
            foodShopPopup.PurchaseSucceeded += HandlePurchaseSucceeded;

            FPButton = new Button();
            AbsoluteLayout.SetLayoutBounds(FPButton, new Rectangle(0.5, 0.5, 0.4, 0.3));
            AbsoluteLayout.SetLayoutFlags(FPButton, AbsoluteLayoutFlags.All);
            FPButton.Text = "Feed";
            FPButton.HorizontalOptions = LayoutOptions.Center;
            FPButton.VerticalOptions = LayoutOptions.Center;
            FPButton.Clicked += petContainer.ButtonFeeding;//Calls ButtonFeeding method from petContainer upon button press

            progressBar = new ProgressBar();//Includes Progress bar to layout, signifying eating progress
            AbsoluteLayout.SetLayoutBounds(progressBar, new Rectangle(0.5, 0.7, 0.8, 0.4));
            AbsoluteLayout.SetLayoutFlags(progressBar, AbsoluteLayoutFlags.All);


            


            //var audio = GetStreamFromFile("appMusic.mp3");
            //var music = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            //music.Load(audio);
            //music.Play();

            //Stream GetStreamFromFile(string filename)
            //{
            //    var assembly = typeof(App).GetTypeInfo().Assembly;
            //    var stream = assembly.GetManifestResourceStream("INFT2051app" + filename);
            //    return stream;
            //}

            Init();

            gameloop = new Timer(30 * 60 * 1000);//30 Minutes
            gameloop.Elapsed += Step;
            gameloop.AutoReset = true;
            gameloop.Start();
        }

        public void Init() {
            /*
             * This init function does the following things
             * - Initialise databases, loading from JSON
             * - Initialise petContainer, setting a random egg
             * 
             * 
             */

            //1. Add the background
            Background bg = new Background();
            main_layout.Children.Add(bg);

            //2. Add the Pet controller input

            //3. Add the pet image
            main_layout.Children.Add(petContainer.CurrentPet);

            //This pushes the debug/credits label to the top of the stack, this allows input to be seen above backgrounds etc
            main_layout.RaiseChild(debugLabel);
            main_layout.RaiseChild(creditsLabel);

            //The top of the stack needs to be the input box
            main_layout.Children.Add(petContainer);

            //But the TOP TOP of the stack needs to be the sticky inputs (options/foodshop button)
            main_layout.RaiseChild(this.FindByName<Grid>("petGrid"));

        }


        //got help from a classmate -- 
        private async void playMusic(object sender, EventArgs e)
        {

            await CrossMediaManager.Current.PlayFromAssembly("appMusic.wav", typeof(MainPage).Assembly);
        }

        public void Step(object source, ElapsedEventArgs e) {
            //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss}", e.SignalTime);
            petContainer.UpdateStatus();
        }

        //------------------ UI BUTTON EVENTS----------------------------------------------------------------
        private async void InvokeSettings(object sender, EventArgs e) {

            await PopupNavigation.Instance.PushAsync(optionsPopup);
        }

        private async void InvokeFoodShop(object sender, EventArgs e) {

            foodShopPopup.updateShopText();
            await PopupNavigation.Instance.PushAsync(foodShopPopup);
            
        }

        private async void InvokeStatusPage(object sender, EventArgs e) {
            string name = petContainer.CurrentPet.NickName;
            int health = petContainer.CurrentPet.Health;
            int happiness = petContainer.CurrentPet.Happiness;
            int hunger = petContainer.CurrentPet.Hunger;
            int hygiene = petContainer.CurrentPet.Hygiene;
            statusPopup.Update(name, health, hunger, happiness, hygiene);
            await PopupNavigation.Instance.PushAsync(statusPopup);

        }

        //------------------ GAME EVENTS----------------------------------------------------------------

        private void HandlePurchaseSucceeded(object sender, EventArgs e) {
            /*
             * When purchasing successfully, the player must then be able to feed the pet using
             * the fingerprint sensor.
             * 
             * Regardless, a progress bar is shown as an indication of the current progress the pet is in when eating
             */
            credits.Text = "Credits: " + foodShopPopup.Credits;
            main_layout.Children.Add(progressBar);

            petContainer.CurrentPet.State = foodShopPopup.GetCurrent();

            petContainer.NoFingerPrintSensorDetected += HandleNoFingerPrintSensorDetected;//Subscribes the event in petcontainer to trigger the HandleNoFingerPrintSensorDetected function
            petContainer.FeedingProcess += HandleFeedingProcess;
            petContainer.FeedingComplete += HandleFeedingComplete;

            //trying to put a picture of the food when the button is clicked 
            //i dont think I can do this until the JSON is loaded 

            petContainer.StartFeedingProcess();//Delegates the feeding task to the petContainer
        }

        private void HandleNoFingerPrintSensorDetected(object sender, EventArgs e) {
            /*
             * When no fingerprint sensor is detected/or when time has passed when the user does not use the sensor, 
             * The mainpage will issue an Onscreen Button
             * This Onscreen button will act as a Pseudo Fingerprint sensor 
             * 
             */

            main_layout.Children.Add(FPButton);
        }

        private void HandleFingerPrintSensorDetected(object sender, EventArgs e) {
            /*
             * When a fingerprint sensor is detected, display helper text signifying that the user should use the fingerprint sensor
             * 
             * Never used, might delete later
             */
        }

        private void HandleFeedingProcess(object sender, EventArgs e) {
            /*
             * Update the progress bar for every press
             * 
             */
            progressBar.ProgressTo(progressBar.Progress + 0.2, 250, Easing.Linear);

        }

        private void HandleFeedingComplete(object sender, EventArgs e) {
            /*
             * This event is triggered upon feeding completion
             * It will:
             * 1. Remove On-Screen button and Progress bar generated from having no fingerprint sensor
             * 2. Remove the food sprite from the container
             * 
             */
            //Start unsubscribing to all events that trigger when the purchase started
            petContainer.NoFingerPrintSensorDetected -= HandleNoFingerPrintSensorDetected;
            petContainer.FeedingProcess -= HandleFeedingProcess;
            petContainer.FeedingComplete -= HandleFeedingComplete;

            main_layout.Children.Remove(FPButton);//Remove FPButton from xaml
            progressBar.ProgressTo(0, 1, Easing.Linear);//ResetProgress bar
            main_layout.Children.Remove(progressBar);//Remove Progress Bar from xaml
             
        }

        public void ChangeBackground() {
            /*
             * This changes the current background
             * 
             */
        }
    }
}
