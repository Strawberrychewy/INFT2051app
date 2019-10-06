using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

using TouchTracking;
using Plugin.Fingerprint;

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
        public int Position_X { get; set; } // X Position of current pet
        public int Position_Y { get; set; } // Y Position of current pet
        public int New_Position_X { get; set; } // new X Position of current pet
        public int New_Position_Y { get; set; } // new Y Position of current pet
        //Enum states = { Eating, Idle, Jumping };
        public event EventHandler NoFingerPrintSensorDetected;
        public event EventHandler FeedingProcess;
        public event EventHandler FeedingComplete;

        public int EatingProgress = 0;

        public string state;

        public PetContainer() {
            //This changes the dimensions of the box size to match the page height and width.
            AbsoluteLayout.SetLayoutBounds(this, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.All);
            Opacity = 0.25;//Remove when finalising
            Color = Color.Black;//Remove when finalising
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            //Variable initialisation
            CurrentPet = new Pet();

            state = "";

            /*TODO: THE START OF THE GAME CAUSES THE IMAGE TO FORCE MOVE TO THE LEFT, 
             * THIS IS BECAUSE IT CANNOT READ THE EXACT POINTS OF THE VIEW BOX 
             * SO IT TREATS THESE VALUES AS (0, 0)
             * 
             * TEMPORARY FIX OR EVEN PERMANENT: updating the pet image is only via touch
             * 
             */
            Position_X = (int)Width / 2;
            Position_Y = (int)Height / 2;
            New_Position_X = Position_X;
            New_Position_Y = Position_Y;

            

            //Allow for touch input
            TouchEffect effect = new TouchEffect();
            effect.TouchAction += OnTouchEffectAction;
            Effects.Add(effect);

        }

        private void OnTouchEffectAction(object sender, TouchActionEventArgs e) {
            New_Position_X = (int) e.Location.X;
            New_Position_Y = (int) e.Location.Y;
            TouchUpdate();
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
                state = "FPSensorDetected";
                //FingerPrintSensorDetected(this, EventArgs.Empty);
                //MainPage will display helper text signifying the user to use the fingerprint sensor
                //The rest of the sensor code can be written here
                int i = 1;
                while (i <= 5) {
                    var auth = await CrossFingerprint.Current.AuthenticateAsync("Feeding " + i);
                    if (auth.Authenticated) {
                        ButtonFeeding(this, EventArgs.Empty);
                        i++;
                    }
                }
                
            } else {//SENSOR UNAVAILABLE
                state = "NoFPSensorDetected";
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
            EatingProgress++;
            state = "Feeding " + EatingProgress;
            FeedingProcess(this, EventArgs.Empty);
            if (EatingProgress == 5) {
                EatingProgress = 0;
                state = "Fed";
                CurrentPet.Hunger += 10;
                FeedingComplete(this, EventArgs.Empty);
            }
        }

        //---------------------------------------MOVEMENT-----------------------------------------------------------------------------------
        public void TouchUpdate() {
            /*
             * This function updates the game regardless of player interaction
             */
            if (CurrentPet.X > New_Position_X) {//Cursor is left of pet
                MoveLeft();
            }
            else if (CurrentPet.X < New_Position_X) {//Cursor is right of pet
                MoveRight();
            }
        }

        public void MoveLeft() {
            int distance = (int) -(CurrentPet.X + CurrentPet.Width / 2) + New_Position_X;
            if (CurrentPet.X + CurrentPet.Width / 2 > 0) {//Check if image does not exceed left edge
                CurrentPet.TranslateTo(distance, 0, 500);
            }

        }

        public void MoveRight() {
            int distance = (int) (New_Position_X - (CurrentPet.X + (CurrentPet.Width / 2)));
            if (CurrentPet.X + CurrentPet.Width / 2 < Width) {//Check if image does not exceed right edge
                CurrentPet.TranslateTo(distance, 0, 500);
            }

        }


        //Only implement this when confident
        public void Jump() {
            /*
             * Jump mechanic 
             */
        }
    }
}
