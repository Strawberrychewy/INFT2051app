using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Essentials;
using System.Windows.Input;
using FFImageLoading.Forms;
using Rg.Plugins.Popup.Services;
using System.Timers;
using System.Reflection;
using System.IO;

using Newtonsoft.Json;
using System.Threading;

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
        public PlayerData playerData;
        readonly System.Timers.Timer gameloop;

        public int Credits = 0;

        private readonly Button FPButton;
        private readonly ProgressBar progressBar;

        public MainPage() {
            InitializeComponent();


            //https://stackoverflow.com/questions/47779937/how-to-allow-for-ios-status-bar-and-iphone-x-notch-in-xamarin-forms
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);


            Load();

            petContainer = new PetContainer(new Pet(playerData));

            optionsPopup = new PopupOptions();
            optionsPopup.popupNameChange.PlayerNameChanged += ChangeName;
            optionsPopup.popupNameChange.PetNameChanged += ChangePetName;
            optionsPopup.popupRestartPrompt.NewGame += HandleRestartGame;

            statusPopup = new PopupStatus(playerData.Name);
            foodShopPopup = new PopupFoodShop(playerData);
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

            Init();

            gameloop = new System.Timers.Timer(30 * 60 * 1000);//30 Minutes
            gameloop.Elapsed += Step;
            gameloop.AutoReset = true;
            gameloop.Start();
        }
        //------------------ SAVE/LOAD FUNCTIONS --------------------------------------------------------
        public void UpdatePlayerData() {
            playerData.Name = statusPopup.PlayerName;//Change this
            playerData.Credits = foodShopPopup.Credits;

            playerData.PetName = petContainer.CurrentPet.NickName;//Name of pet object
            playerData.BasePet = petContainer.CurrentPet.Base.Name;//Name of Base pet from pet object
            playerData.Age = petContainer.CurrentPet.Age;
            playerData.Hunger = petContainer.CurrentPet.Hunger;
            playerData.Happiness = petContainer.CurrentPet.Happiness;
            playerData.Hygiene = petContainer.CurrentPet.Hygiene;
            playerData.Health = petContainer.CurrentPet.Health;
        }

        public void OffloadPlayerData() {
            statusPopup.PlayerName = playerData.Name;//Change this
            foodShopPopup.Credits = playerData.Credits;

            petContainer.CurrentPet.NickName = playerData.PetName;//Name of pet object
            petContainer.CurrentPet.Age = playerData.Age;
            petContainer.CurrentPet.Hunger = playerData.Hunger;
            petContainer.CurrentPet.Happiness = playerData.Happiness;
            petContainer.CurrentPet.Hygiene = playerData.Hygiene;
            petContainer.CurrentPet.Health = playerData.Health;

            petContainer.CurrentPet.ChangePet(playerData.BasePet);
        }

        public void Save() {
            //Converts [playerData] into JSON format and saves it to file at specific path
            File.WriteAllText(App.savedata, JsonConvert.SerializeObject(playerData));
        }
        
        private void Load() {
            //Converts JSON Format file into [PlayerData] object and overrides the playerData variable
            if (File.Exists(App.savedata)) {
                playerData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(App.savedata));
            } else if (!File.Exists(App.savedata)) {
                FamiliarsList list = new FamiliarsList();
                playerData = new PlayerData(basePet: list.FindRandomBasicPet().Name);
            }
        }

        public void Init() {
            /*
             * This init function does the following things
             * - Initialise databases, loading from JSON
             * - Initialise petContainer 
             */


            //1. Add the background
            Background bg = new Background();
            main_layout.Children.Add(bg);
            //3. Add the pet image
            main_layout.Children.Add(petContainer.CurrentPet);

            //The top of the stack needs to be the input box
            main_layout.Children.Add(petContainer);

            //But the TOP TOP of the stack needs to be the sticky inputs (options/foodshop button)
            main_layout.RaiseChild(this.FindByName<Grid>("petGrid"));
            main_layout.LowerChild(this.FindByName<AbsoluteLayout>("lastGrid"));

        }

        public void Step(object source, ElapsedEventArgs e) {
            //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss}", e.SignalTime);
            foodShopPopup.Reset();

            UpdatePlayerData();
            Save();

            petContainer.UpdateStatus();
        }
        //------------------ UI BUTTON EVENTS----------------------------------------------------------------
        private async void InvokeSettings(object sender, EventArgs e) {
            optionsPopup.Update(playerData);
            await PopupNavigation.Instance.PushAsync(optionsPopup);
        }

        private async void InvokeFoodShop(object sender, EventArgs e) {

            await PopupNavigation.Instance.PushAsync(foodShopPopup);
            
        }

        private async void InvokeStatusPage(object sender, EventArgs e) {
            statusPopup.Update(petContainer.CurrentPet, playerData);
            await PopupNavigation.Instance.PushAsync(statusPopup);

        }

        private void InvokeCleanPet(object sender, EventArgs e) {
            //Basic cleaning pet function
            if (foodShopPopup.Credits >= 10) {

                foodShopPopup.Credits -= 10;
                petContainer.CurrentPet.Hygiene += 10;


                
                petContainer.CurrentPet.BounceMicro();
                petContainer.CurrentPet.BounceMicro();
                UpdatePlayerData();
                UpdateCreditsLabel();
                Save();
            }
        }

        private void InvokePlayPet(object sender, EventArgs e) {
            //Basic PLAYING WITH PET function
            petContainer.CurrentPet.Happiness += 10;
            petContainer.CurrentPet.BounceJump();
            UpdatePlayerData();
            Save();
        }

        //--------------------------------DEBUG---------------------------------------------------------
        private void SpeedUpTime(object sender, EventArgs e) {

            //This is literally the step function in the form of a Button method (Also cheats)
            petContainer.CurrentPet.Hygiene += 10;//OMIT WHEN DONE
            petContainer.CurrentPet.Health += 10;//OMIT WHEN DONE
            petContainer.CurrentPet.Happiness += 10;//OMIT WHEN DONE
            petContainer.CurrentPet.Hunger += 10;//OMIT WHEN DONE
            foodShopPopup.Reset();

            UpdatePlayerData();
            Save();

            petContainer.CurrentPet.UpdateStatus(1);
        }
        //------------------ GAME EVENTS----------------------------------------------------------------

        private void HandlePurchaseSucceeded(object sender, EventArgs e) {
            /*
             * When purchasing successfully, the player must then be able to feed the pet using
             * the fingerprint sensor.
             * 
             * Regardless, a progress bar is shown as an indication of the current progress the pet is in when eating
             */
            //main_layout.Children.Add(progressBar);

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

        private void HandleRestartGame(object sender, EventArgs e) {
            /*
             * 
             * 
             * 
             */
            FamiliarsList list = new FamiliarsList();
            playerData = new PlayerData(basePet: list.FindRandomBasicPet().Name);
            OffloadPlayerData();
            
        }

        private void HandleFeedingProcess(object sender, EventArgs e) {
            /*
             * Update the progress bar for every press
             * 
             */
            //progressBar.ProgressTo(progressBar.Progress + 1, 250, Easing.Linear);

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
            //progressBar.ProgressTo(progressBar.Progress + 1, 250, Easing.Linear);//ResetProgress bar
            //main_layout.Children.Remove(progressBar);//Remove Progress Bar from xaml

            petContainer.CurrentPet.Hunger += (int)(foodShopPopup.current.Cost / 10);

            UpdateCreditsLabel();
            UpdatePlayerData();
            Save();

            petContainer.CurrentPet.BounceLow();
            petContainer.CurrentPet.BounceLow();
            petContainer.CurrentPet.BounceHigh();
        }



        private void ChangeName(object sender, EventArgs e) {
            statusPopup.PlayerName = optionsPopup.popupNameChange.PlayerName;
            UpdatePlayerData();
        }

        private void ChangePetName(object sender, EventArgs e) {
            petContainer.CurrentPet.NickName = optionsPopup.popupNameChange.PetName;
            UpdatePlayerData();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            Accelerometer.Start(SensorSpeed.Game);
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            UpdateCreditsLabel();
            UpdatePlayerData();
            Save();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.Stop();
            UpdateCreditsLabel();
            UpdatePlayerData();
            Save();
        }

        public void Accelerometer_ShakeDetected(object sender, EventArgs e) {
            // Process shake event
            foodShopPopup.Credits++;
            UpdateCreditsLabel();
        }

        private void UpdateCreditsLabel() {
            credits.Text = "Credits: " + foodShopPopup.Credits; 
        }

    }
}
