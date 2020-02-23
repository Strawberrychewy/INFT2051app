using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Essentials;
using System.Windows.Input;
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
        private readonly Background bg;
        public PopupFoodShop foodShopPopup;
        private readonly PopupOptions optionsPopup;
        private readonly PopupStatus statusPopup;
        readonly PetContainer petContainer;  
        public PlayerData playerData;
        readonly System.Timers.Timer gameloop;

        public int Credits = 0;

        public MainPage() {
            InitializeComponent();

            //=============================================
            //Reference A1
            //Purpose: add space to the top of iphone to avoid overlap
            //Date: 27 October 2019
            //Source: StackOverFlow
            //Author: Jason
            //https://stackoverflow.com/questions/47779937/how-to-allow-for-ios-status-bar-and-iphone-x-notch-in-xamarin-forms
            //=============================================

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            //=============================================
            // End reference A1
            //=============================================


            Load();

            bg = new Background();
            petContainer = new PetContainer(new Pet(playerData));

            optionsPopup = new PopupOptions();
            optionsPopup.popupNameChange.PlayerNameChanged += ChangeName;
            optionsPopup.popupNameChange.PetNameChanged += ChangePetName;
            optionsPopup.popupRestartPrompt.NewGame += HandleRestartGame;

            statusPopup = new PopupStatus(playerData.Name);
            foodShopPopup = new PopupFoodShop(playerData);
            foodShopPopup.PurchaseSucceeded += HandleFeedingComplete;
            foodShopPopup.PurchaseFailed += InsufficientFunds;

            Init();

            gameloop = new System.Timers.Timer(30 * 60 * 1000);//30 Minutes
            gameloop.Elapsed += Step;
            gameloop.AutoReset = true;
            gameloop.Start();
        }
        //------------------ SAVE/LOAD FUNCTIONS --------------------------------------------------------
           /* A7
            * NAME: NEWTONSOFT.JSON
            * PURPOSE: To Read and Write the PlayerData object to a file so it can be used after the app is closed
            * DATE: 27/10/19
            * SOURCE OF CODE AND ASSISTANCE: https://github.com/JamesNK/Newtonsoft.Json
            * AUTHOR: James Newton-King
            * URL: https://www.newtonsoft.com/json/help/html/SerializingJSON.htm
            * DESCRIPTION OF ASSISTANCE: Code examples to read and write to file
            */
        public void UpdatePlayerData() {
            /*
             * This saves all information to the playerdata class
             * 
             * 
             */
            playerData.Name = statusPopup.PlayerName;
            playerData.Credits = foodShopPopup.Credits;

            playerData.PetName = petContainer.CurrentPet.NickName;//Name of pet object
            playerData.BasePet = petContainer.CurrentPet.Base.Name;//Name of Base pet from pet object
            playerData.Age = petContainer.CurrentPet.Age;
            playerData.Hunger = petContainer.CurrentPet.Hunger;
            playerData.Happiness = petContainer.CurrentPet.Happiness;
            playerData.Hygiene = petContainer.CurrentPet.Hygiene;
            playerData.Health = petContainer.CurrentPet.Health;
            playerData.IsShiny = petContainer.CurrentPet.IsShiny;
        }

        public void OffloadPlayerData() {
            /*
             * This offloads the information from the playerdata object into the rest of the game
             * 
             * 
             */
            statusPopup.PlayerName = playerData.Name;
            foodShopPopup.Credits = playerData.Credits;

            petContainer.CurrentPet.NickName = playerData.PetName;//Name of pet object
            petContainer.CurrentPet.Age = playerData.Age;
            petContainer.CurrentPet.Hunger = playerData.Hunger;
            petContainer.CurrentPet.Happiness = playerData.Happiness;
            petContainer.CurrentPet.Hygiene = playerData.Hygiene;
            petContainer.CurrentPet.Health = playerData.Health;
            petContainer.CurrentPet.IsShiny = playerData.IsShiny;

            petContainer.CurrentPet.ChangePet(playerData.BasePet, petContainer.CurrentPet.IsShiny);
            credits.Text = "Credits: " + playerData.Credits;
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
                playerData = new PlayerData(basePet: list.FindRandomBasicPet().Name, shiny: ShinyRoll());
                playerData.PetName = playerData.BasePet;
            }
        }

        public void Init() {
            /*
             * - Initialise databases, loading from JSON
             * - Initialise petContainer 
             */

            // Add the background
            main_layout.Children.Add(bg);

            // Add the pet image
            main_layout.Children.Add(petContainer.CurrentPet);

            //The top of the stack needs to be the input box
            main_layout.Children.Add(petContainer);

            //But the TOP TOP of the stack needs to be the sticky inputs (options/foodshop button)
            main_layout.RaiseChild(this.FindByName<Grid>("petGrid"));
            main_layout.LowerChild(this.FindByName<AbsoluteLayout>("lastGrid"));

        }

        public void Step(object source, ElapsedEventArgs e) {
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

            foodShopPopup.Credits = playerData.Credits;
            await PopupNavigation.Instance.PushAsync(foodShopPopup);
            
        }

        private async void InvokeStatusPage(object sender, EventArgs e) {

            statusPopup.Update(petContainer.CurrentPet, playerData);
            await PopupNavigation.Instance.PushAsync(statusPopup);

        }

        private void InvokeCleanPet(object sender, EventArgs e) {
            //Basic cleaning pet function
            if (foodShopPopup.Credits >= 30) {
                foodShopPopup.Credits -= 30;
                petContainer.CurrentPet.Hygiene += 10;

                petContainer.CurrentPet.BounceMicro();
                petContainer.CurrentPet.BounceMicro();
                petContainer.CurrentPet.BounceMicro();
                petContainer.CurrentPet.BounceMicro();

                UpdatePlayerData();
                UpdateCreditsLabel();
                Save();
            } else {
                InsufficientFunds(this, EventArgs.Empty);
            }
        }

        private void InvokePlayPet(object sender, EventArgs e) {
            //Basic PLAYING WITH PET function
            if (foodShopPopup.Credits >= 30) {
                foodShopPopup.Credits -= 30;
                petContainer.CurrentPet.Happiness += 10;
                petContainer.CurrentPet.BounceJump();

                UpdatePlayerData();
                UpdateCreditsLabel();
                Save();
            } else {
                InsufficientFunds(this, EventArgs.Empty);
            }
        }

        //--------------------------------DEBUG---------------------------------------------------------
/*        private void SpeedUpTime(object sender, EventArgs e) {

            //This is literally the step function in the form of a Button method (Also cheats)
            //petContainer.CurrentPet.Hygiene += 10;//OMIT WHEN DONE
            //petContainer.CurrentPet.Health += 10;//OMIT WHEN DONE
            //petContainer.CurrentPet.Happiness += 10;//OMIT WHEN DONE
            //petContainer.CurrentPet.Hunger += 10;//OMIT WHEN DONE
            //foodShopPopup.Reset();

            UpdatePlayerData();
            Save();

            petContainer.CurrentPet.UpdateStatus(1);
        }*/
        //------------------ GAME EVENTS----------------------------------------------------------------

        private void HandleRestartGame(object sender, EventArgs e) {
            /*
             * Restarts the game
             * Sets all player data to default and generates a new random pet
             * 
             */
           if (foodShopPopup.Credits >= 50) {
                FamiliarsList list = new FamiliarsList();
                playerData = new PlayerData(basePet: list.FindRandomBasicPet().Name, shiny: ShinyRoll());
                playerData.PetName = playerData.BasePet;

                OffloadPlayerData();
                petContainer.UpdatePetStates();
                Save();
           } else {
                InsufficientFunds(this, EventArgs.Empty);
           }
            
        }
        private bool ShinyRoll() {
            //Basically checks if the new game will grant a shiny or not
            Random random = new Random();
            double shinyroll = random.NextDouble();
            if (shinyroll < 0.1) {
                return true;
            } else {
                return false;
            }
        }

        private void HandleFeedingComplete(object sender, EventArgs e) {
            /*
             * This event is triggered upon feeding completion
             * It will:
             * 1. Remove On-Screen button and Progress bar generated from having no fingerprint sensor
             * 2. Remove the food sprite from the container
             * 
             */
            playerData.Credits = foodShopPopup.Credits;
            petContainer.CurrentPet.Hunger += (int)(foodShopPopup.current.Cost / 10);

            petContainer.CurrentPet.BounceLow();
            petContainer.CurrentPet.BounceLow();
            petContainer.CurrentPet.BounceHigh();

            UpdateCreditsLabel();
            UpdatePlayerData();
            Save();
        }

        private void ChangeName(object sender, EventArgs e) {
            statusPopup.PlayerName = optionsPopup.popupNameChange.PlayerName;
            UpdatePlayerData();
            optionsPopup.Update(playerData);
            Save();
        }

        private void ChangePetName(object sender, EventArgs e) {
            petContainer.CurrentPet.NickName = optionsPopup.popupNameChange.PetName;
            UpdatePlayerData();
            optionsPopup.Update(playerData);
            Save();

        }

        private async void InsufficientFunds(object sender, EventArgs e) {
            credits.TextColor = Color.Red;
            await credits.TranslateTo(-50, TranslationY, 50, Easing.Linear);//LEFT
            await credits.TranslateTo(0, TranslationY, 50, Easing.Linear);//RIGHT
            await credits.TranslateTo(-25, TranslationY, 25, Easing.Linear);//LEFT
            await credits.TranslateTo(0, TranslationY, 25, Easing.Linear);//RIGHT
            await credits.TranslateTo(-10, TranslationY, 10, Easing.Linear);//LEFT
            await credits.TranslateTo(0, TranslationY, 10, Easing.Linear);//RIGHT
            credits.TextColor = Color.White;

        }

        protected override void OnAppearing() {
            //subscribes to accelerometer service then updates everything to game and on file
            base.OnAppearing();
            Accelerometer.Start(SensorSpeed.Game);
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            UpdateCreditsLabel();
            UpdatePlayerData();
            Save();
        }

        protected override void OnDisappearing() {
            //Unsubscribes from using accelerometer then updates everything to game and on file
            base.OnDisappearing();
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.Stop();
            UpdateCreditsLabel();
            UpdatePlayerData();
            Save();
        }

        private void UpdateCreditsLabel() {
            credits.Text = "Credits: " + playerData.Credits; 
        }

        public void Accelerometer_ShakeDetected(object sender, EventArgs e) {
            /*
             * A8
             * NAME: XAMARIN.ESSENTIALS
             * PURPOSE: To allow for shakes to be detected using the accelerometer on the device
             * DATE: 15/9/19
             * SOURCE OF CODE AND ASSISTANCE: https://github.com/xamarin/Essentials
             * AUTHOR: Microsoft
             * DESCRIPTION OF ASSISTANCE: Code Tutorial to subscribe and unsubscribe to the accelerometer service
            *
            */
            // Process shake event
            
            playerData.Credits++;
            foodShopPopup.Credits = playerData.Credits;
            UpdateCreditsLabel();
        }
    }
}
