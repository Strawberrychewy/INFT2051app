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
        private PetContainer petContainer;  //Controller for the pet
        public int credits = 0;

        readonly Timer gameloop;

        private readonly Label creditsLabel;
        private readonly Label debugLabel;

        public MainPage() {
            InitializeComponent();



            Init();
            //The following maps the labels to variables so they may be changed during render time
            creditsLabel = this.FindByName<Label>("Credits_number");
            debugLabel = this.FindByName<Label>("DebugLabel");

            gameloop = new Timer(Step);
            gameloop.Change(0, 33);


            //code from https://docs.microsoft.com/en-us/xamarin/xamarin-forms/xaml/xaml-basics/get-started-with-xaml?tabs=windows
            Button button = new Button()
            {
                Text = "Open Settings",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            button.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new INFT2051app.Models.Settings());
            };


            //var absLayout = new AbsoluteLayout();
            //var image = new Image();
            //var stackLayout = GetStackLayout();

            //absLayout.Add(image, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            //absLayout.Add(stackLayout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
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
            petContainer.HeightRequest = 411;
            petContainer.WidthRequest = 790;
            main_layout.Children.Add(petContainer);
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

            petContainer.Update();
            //RENDERING
            Device.BeginInvokeOnMainThread(StepLabel);

        }

        void StepLabel() {
            /*
             * The Following code updates all the labels in the front end
             * Comment out calls to this function when project is being finialised
             */
            creditsLabel.Text = "Credits: " + credits.ToString();
            debugLabel.Text = "Happiness: " + petContainer.CurrentPet.Happiness + "\n"
                            + "Status: " + petContainer.CurrentPet.Status() + "\n"
                            + "Pet X Coord: " + petContainer.Position_X + " Pet Y Coord: " + petContainer.Position_Y + "\n"
                            + "Current time: " + DateTime.Now.ToString() + "\n"
                            + "Device Height: " + DeviceDisplay.MainDisplayInfo.Height + " Device Width: " + DeviceDisplay.MainDisplayInfo.Width + "\n"
                            + "PageWidth: " + (int)Width + " PageHeight: " + (int)Height + "\n"
                            + "Cursor X: " + petContainer.New_Position_X + " Cursor Y: " + petContainer.New_Position_Y + "\n"
                            + "Pet Info: " + petContainer.CurrentPet.ToString() + "\n";
        }

        public void ChangeBackground() {
            /*
             * This changes the current background
             * 
             */
        }
    }
}
