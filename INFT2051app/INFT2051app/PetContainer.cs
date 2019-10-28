using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

using TouchTracking;
using Plugin.Fingerprint;
using System.Timers;

namespace INFT2051app {
    class PetContainer : BoxView {
        /*
         * The Pet container class specifies the current pet being played with
         * It also acts as a controller for the Pet object ensuring that it does not go beyond the bounding boxes
         * 
         * This class extends from BoxView Class so it acts as an invisible layer for the player input
         * 
         * Things that it controls
         * - Animation States (Eating, idle, jumping)
         * - State Transitions
         * - Xpos Ypos Movement (Collision Handling)
         * - 
         * - 
         */
        public Pet CurrentPet { get; set; } //The current pet being played with
        public int New_Position_X { get; set; } // new X Position of current pet
        public int New_Position_Y { get; set; } // new Y Position of current pet
        public int BoxWidth { get; set; }
        public int BoxHeight { get; set; }
        //Enum states = { Eating, Idle, Jumping };
        public event EventHandler NoFingerPrintSensorDetected;
        public event EventHandler FeedingProcess;
        public event EventHandler FeedingComplete;

        public enum PetStates {Inactive, Active}
        public PetStates petStateValue;
        public int EatingProgress = 0;

        readonly Timer PetTimer;// This timer will check if no touch action is used in the last 5 seconds. Any touch interaction will reset this timer

        public PetContainer(Pet pet) {
            //This changes the dimensions of the box size to match the page height and width.
            AbsoluteLayout.SetLayoutBounds(this, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.All);
            Opacity = 0.25;//Remove when finalising
            Color = Color.Black;//Remove when finalising
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            //Variable initialisation
            CurrentPet = pet;
            New_Position_X = 0;
            New_Position_Y = 0;

            UpdatePetStates();

            //Allow for touch input
            TouchEffect effect = new TouchEffect();
            effect.TouchAction += OnTouchEffectAction;
            Effects.Add(effect);

            PetTimer = new Timer(5 * 1000);//5 Seconds
            PetTimer.Elapsed += Step;
            PetTimer.AutoReset = true;
            PetTimer.Stop();
            PetTimer.Start();
        }


        //--------------------------------------INTERACTIVITY-------------------------------------------------------------------------------------------
        private void OnTouchEffectAction(object sender, TouchActionEventArgs e) {
            /*
             * This code triggers when the user clicks on the petContainer, which spans the entire screen
             * 1. Detect X and Y coordinates
             * 2. Update the pet upon those coordinates
             * 3. Restart Timer
             */
            PetTimer.Stop();
            PetTimer.Interval = 5 * 1000; // 5 SECONDS
            PetTimer.Start();
            New_Position_X = (int) e.Location.X;
            New_Position_Y = (int) e.Location.Y;
            MoveToPosition();

            //when user touches the screen make a sound effect 
        }

        public async void StartFeedingProcess() {
            /*
             * The MainPage will call this event upon successful purchase of a food item
             * This food item will appear on the screen and the player must use the 
             * fingerprint sensor to 'Charge up a bar to motivate the pet into eating the food'
             * 
             * It will:
             * 1. Subscribe to the Fingerprint sensor, 
             *    If no fingerprint sensor is present on the device, [Or when time has passed without using the sensor],
             *    An onscreen button will show which will allow the player to hold down
             *    As a pseduo fingerprint sensor
             * 2. Iterate from 0 - 100 while the fingerprint sensor/OnScreenButton is held
             * 3. Unsubscribe to the Fingerprint Sensor/OnScreenButton
             * 
             */
            var result = await CrossFingerprint.Current.IsAvailableAsync(true);//Check if sensor is available
            if (result) {//SENSOR AVAILABLE
                //FingerPrintSensorDetected(this, EventArgs.Empty);
                //MainPage will display helper text signifying the user to use the fingerprint sensor
                //The rest of the sensor code can be written here
                var auth = await CrossFingerprint.Current.AuthenticateAsync("Feeding ");
                if (auth.Authenticated) {
                    ButtonFeeding(this, EventArgs.Empty);
                }
                
            } else {//SENSOR UNAVAILABLE
                NoFingerPrintSensorDetected(this, EventArgs.Empty);//MainPage will issue a new button and allow the user to click that instead
            }
        }

        public void ButtonFeeding(object sender, EventArgs e) {
            /*
             * The MainPage will call this when the OnScreenButton is held down 
             * When the finger is released, the feeding progress stops
             * 
             * 
             */
            FeedingProcess(this, EventArgs.Empty);
            CurrentPet.Hunger += 10;
            FeedingComplete(this, EventArgs.Empty);
        }
        //---------------------------------------STATES-------------------------------------------------------------------------------------------------

        public void Step(object source, ElapsedEventArgs e) {
            /* This code triggers every 5 seconds blah blah blah
             * It will simulate idle movement when going without user interaction
             * 1. Generate an x coordinate within the bounds of the size of the window
             * 2. Act as if the player were to control the pet using that coordinate
             * 
             */
            Random random = new Random();
            int i = random.Next(0, 5);
            switch (i) {
                case (0):
                    //Random Movement (LEFT/RIGHT)
                    IdleMove();
                    break;
                case (1):
                    //Bounce High
                    CurrentPet.BounceHigh();
                    break;
                case (2):
                    //Bounce Low
                    CurrentPet.BounceLow();
                    break;
                case (3):
                    //Bounce High with a jump
                    CurrentPet.BounceJump();
                    break;
                case (4):
                    //Bounce High with a jump
                    CurrentPet.BounceMicro();
                    break;
            }
            PetTimer.Stop();//Having this here keeps the pet from lagging during opening idle animations
            PetTimer.Interval = 6 * 1000;//3 seconds, shorter time for idle state
            PetTimer.Start();
        }

        public void UpdatePetStates() {
            if (CurrentPet.PetStateValue == Pet.PetState.Awake) {
                petStateValue = PetStates.Active;
            }
        }

        public void UpdateStatus() {
            //This code triggers every 30 minutes
            CurrentPet.UpdateStatus(1);
        }
        //---------------------------------------MOVEMENT-----------------------------------------------------------------------------------------------
        /*
         * Moving animations are relative to the positioning of the boxview, thus all of the following functions are on this compared to the Pet object
         */
        private void IdleMove() {
            Random random = new Random();
            New_Position_X = random.Next(0, BoxWidth);
            MoveToPosition();
        }
        private void MoveToPosition() {
            //This code will trigger upon either idle state or when user touches the screen
            
            if (CurrentPet.X + CurrentPet.Width / 2 > New_Position_X) {//Cursor is left of pet
                CurrentPet.RotateYTo(360, 1);
                MoveLeft();
            } else if (CurrentPet.X + CurrentPet.Width / 2 < New_Position_X) {//Cursor is right of pet
                CurrentPet.RotateYTo(180, 1);
                MoveRight();
            }
        }

        private async void MoveLeft() {
            int distance = (int) -(CurrentPet.X + CurrentPet.Width / 2) + New_Position_X;
            if (CurrentPet.X + CurrentPet.Width / 2 > 0) {//Check if image does not exceed left edge
                if (New_Position_X < CurrentPet.Width / 2) {
                    await CurrentPet.TranslateTo(distance + CurrentPet.Width / 2, 0, 500, Easing.Linear);
                } else {
                    await CurrentPet.TranslateTo(distance, 0, 500, Easing.Linear);

                }
            }

        }

        private async void MoveRight() {
            int distance = (int) (New_Position_X - (CurrentPet.X + (CurrentPet.Width / 2)));
            if (CurrentPet.X + CurrentPet.Width / 2 < BoxWidth) {//Check if image does not exceed right edge
                if (New_Position_X > BoxWidth - CurrentPet.Width / 2) {
                    await CurrentPet.TranslateTo(distance - CurrentPet.Width / 2, 0, 500, Easing.Linear);
                } else {
                    await CurrentPet.TranslateTo(distance, 0, 500, Easing.Linear);
                }
            }

        }
        //---------------------------------------OVERRIDES-----------------------------------------------------------------------------------------------
        protected override void OnSizeAllocated(double width, double height) {
            //This sets the height and width that the class can access. If this function is not implemented, Height and Width will return 0
            BoxHeight = (int) height;
            BoxWidth = (int) width;
        }
        public override string ToString() {
            return AbsoluteLayout.GetLayoutBounds(this).Size.Width.ToString();
        }
    }
}
