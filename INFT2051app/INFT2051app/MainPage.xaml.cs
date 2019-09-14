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

using TouchTracking;



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

        private FamiliarsList familiarsList;    //Contains the entire Pet list
        private PetContainer petContainer;  //Controller for the pet
        private int credits;

        Timer gameloop;

        private Label creditsLabel;
        private Label statusLabel;
        private Label coordLabel;
        private Label deviceLabel;
        private Label timeLabel;
        private Label petLabel;
        private Label cursorLabel;

        private BoxView leftRegion;
        private BoxView rightRegion;
        private BoxView backgroundRegion;

        public MainPage() {
            InitializeComponent();

            Init();
            //The following maps the labels to variables so they may be changed during render time
            creditsLabel = this.FindByName<Label>("Credits_number");
            statusLabel = this.FindByName<Label>("Happiness_value");
            coordLabel = this.FindByName<Label>("coordinates");
            deviceLabel = this.FindByName<Label>("device");
            timeLabel = this.FindByName<Label>("time");
            petLabel = this.FindByName<Label>("petinfo");
            cursorLabel = this.FindByName<Label>("cursor_position");

            //The following maps the BoxViews to variables so the player can move the pet object during render time
            rightRegion = this.FindByName<BoxView>("side_right");
            leftRegion = this.FindByName<BoxView>("side_left");
            backgroundRegion = this.FindByName<BoxView>("background");

            //The following adds tap recognisers to listen to touches in that region
            rightRegion.GestureRecognizers.Add(new TapGestureRecognizer());
            leftRegion.GestureRecognizers.Add(new TapGestureRecognizer());


            gameloop = new Timer(Step);
            gameloop.Change(0, 33);
        }

        public void Init() {
            /*
             * This init function does the following things
             * - Initialise databases, loading from JSON
             * - Initialise petContainer, setting a random egg
             * 
             * 
             */
            petContainer = new PetContainer();
            main_layout.Children.Add(petContainer);
            credits = 0;
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
            //READ INPUT
            Device.BeginInvokeOnMainThread(StepInput);

            petContainer.update();
            //RENDERING
            Device.BeginInvokeOnMainThread(StepLabel);

        }

        void StepInput() {

        }

        void StepLabel() {
            /*
             * The Following code updates all the labels in the front end
             * 
             */
            creditsLabel.Text = "Credits: " + credits.ToString();
            statusLabel.Text = "Happiness: " + petContainer.currentPet.Happiness + "\nStatus: "  + petContainer.currentPet.Status();
            coordLabel.Text = "Pet X Coord: " + petContainer.Position_X + "\nPet Y Coord: " + petContainer.Position_Y;
            timeLabel.Text = "Current time: \n" + DateTime.Now.ToString();
            deviceLabel.Text = "Device Height: " + DeviceDisplay.MainDisplayInfo.Height + "\nDevice Width: " + DeviceDisplay.MainDisplayInfo.Width;
            petLabel.Text = "Pet Info: " + petContainer.currentPet.ToString();
            cursorLabel.Text = "Cursor X: " + petContainer.new_Position_X + "\nCursor Y: " + petContainer.new_Position_Y;
        }

        public void ChangeBackground() {
            /*
             * This changes the current background
             * 
             */
        }
    }
}
