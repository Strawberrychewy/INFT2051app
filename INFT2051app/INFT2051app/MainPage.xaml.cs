using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Windows.Input;
using FFImageLoading.Forms;
using Rg.Plugins.Popup.Services;

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

        private readonly FamiliarsList familiarsList;    //Contains the entire Pet list
        public PopupFoodShop foodShopPopup;
        private readonly PopupOptions optionsPopup;
        PetContainer petContainer;  //Controller for the pet

        readonly Timer gameloop;

        private readonly Label creditsLabel;
        private Label debugLabel;

        public MainPage() {
            InitializeComponent();

            petContainer = new PetContainer();
            optionsPopup = new PopupOptions();

            foodShopPopup = new PopupFoodShop();
            foodShopPopup.PurchaseSucceeded += HandlePurchaseSucceeded;
            foodShopPopup.PurchaseFailed += HandlePurchaseFailed;

            //The following maps the labels to variables so they may be changed during render time
            creditsLabel = this.FindByName<Label>("Credits_number");
            debugLabel = this.FindByName<Label>("DebugLabel");

            Init();

            gameloop = new Timer(Step);
            gameloop.Change(0, 33);
        }

        private void HandlePurchaseFailed(object sender, EventArgs e) {
            petContainer.CurrentPet.State = "Insufficient Funds";
        }

        private void HandlePurchaseSucceeded(object sender, EventArgs e) {
            petContainer.CurrentPet.State = foodShopPopup.getCurrent();
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

            //3. Add the pet image (Right now its an egg, don't sue me)
            main_layout.Children.Add(petContainer.CurrentPet);

            //This pushes the debug/credits label to the top of the stack, this allows input to be seen above backgrounds etc
            main_layout.RaiseChild(debugLabel);
            main_layout.RaiseChild(creditsLabel);

            //The top of the stack needs to be the input box
            main_layout.Children.Add(petContainer);

            //But the TOP TOP of the stack needs to be the sticky inputs (options/foodshop button)
            main_layout.RaiseChild(this.FindByName<ImageButton>("Options"));
            main_layout.RaiseChild(this.FindByName<ImageButton>("Foodshop"));

        }
        public void Step(object state) {
            /*
             * The step function specifies what happens in one instance of the loop.
             * It does the following things (IN ORDER)
             * 
             * Read user input
             * Check Movement Collision (SEE PetContainer.cs)
             * Perform Movement (If Any)
             * Check changes to State Change
             * Perform Rendering
             */

            //RENDERING
            Device.BeginInvokeOnMainThread(StepLabel);
        }

        void StepLabel() {
            /*
             * The Following code updates all the labels in the front end
             * Comment out calls to this function when project is being finialised
             */
            //creditsLabel.Text = "Credits: " + credits.ToString();
            debugLabel.Text = "Happiness: " + petContainer.CurrentPet.Happiness + " State: " + petContainer.CurrentPet.State + "\n"
                            + "Status: " + petContainer.CurrentPet.Status() + "\n"
                            + "Pet Coord info: [" + petContainer.Position_X + " , " + petContainer.Position_Y + "]\n"
                            + "Current time: " + DateTime.Now.ToString() + "\n"
                            + "Device info: [H: " + DeviceDisplay.MainDisplayInfo.Height + ", W: " + DeviceDisplay.MainDisplayInfo.Width + "]\n"
                            + "Page info: [" + (int)Width + ", " + (int)Height + "]\n"
                            + "Cursor info: [" + petContainer.New_Position_X + ", " + petContainer.New_Position_Y + "]\n"
                            + "Pet Info: " + petContainer.CurrentPet.ToString() + "\n"
                            + "Egg Info: [" + ((int)petContainer.CurrentPet.X + (int)petContainer.CurrentPet.Width / 2) + ", " + (int)petContainer.CurrentPet.Y + "][H: " + (int)petContainer.CurrentPet.Height + ", W: " + (int)petContainer.CurrentPet.Width + "]\n"
                            + "Credits: " + foodShopPopup.credits.ToString();
        }

        private async void InvokeSettings(object sender, EventArgs e) {

            await PopupNavigation.Instance.PushAsync(optionsPopup);
        }

        private async void InvokeFoodShop(object sender, EventArgs e) {

            await PopupNavigation.Instance.PushAsync(foodShopPopup);
            
        }

        public void ChangeBackground() {
            /*
             * This changes the current background
             * 
             */
        }
    }
}
