using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

using TouchTracking;

namespace INFT2051app {
    class PetContainer : BoxView {
        /*
         * The Pet container class specifies the current pet being played with
         * It also acts as a controller for the Pet object ensuring that it does not go beyond the bounding boxes
         * 
         * 
         * Things that it controls
         * - Animation States (Eating, idle, jumping)
         * - State Transitions
         * - Xpos Ypos Movement (Collision Handling)
         * - 
         * - 
         */
        public Pet currentPet { get; set; } //The current pet being played with
        public double Position_X { get; set; } // X Position of current pet
        public double Position_Y { get; set; } // Y Position of current pet
        public double new_Position_X { get; set; } // new X Position of current pet
        public double new_Position_Y { get; set; } // new Y Position of current pet
        //Enum states = { Eating, Idle, Jumping };

        public PetContainer() {
            currentPet = new Pet();
            Position_X = DeviceDisplay.MainDisplayInfo.Width/2;
            Position_Y = DeviceDisplay.MainDisplayInfo.Height/2;
            new_Position_X = Position_X;
            new_Position_Y = Position_Y;

            TouchEffect effect = new TouchEffect();
            effect.TouchAction += OnTouchEffectAction;
            Effects.Add(effect);

            WidthRequest = DeviceDisplay.MainDisplayInfo.Width;
            HeightRequest = DeviceDisplay.MainDisplayInfo.Height;

        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs e) {
            new_Position_X = e.Location.X;
            new_Position_Y = e.Location.Y;
        }

        public void update() {
            /*
             * This function updates the game regardless of player interaction
             */
        }


        /*MOVEMENT*/
        public void moveLeft() {
            if ((Position_X > 0) && (Position_X > new_Position_X)) {//Check if image does not exceed left edge
                Position_X--;
            }
        }

        public void moveRight() {
            if ((Position_X < DeviceDisplay.MainDisplayInfo.Width) && (Position_X < new_Position_X)) {//Check if image does not exceed right edge
                Position_X++;
            }
        }


        //Only implement this when confident
        public void jump() {
            /*
             * Jump mechanic 
             */
        }
    }
}
