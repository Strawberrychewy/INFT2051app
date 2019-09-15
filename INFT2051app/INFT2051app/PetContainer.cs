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

        public PetContainer() {
            //This changes the dimensions of the box size.
            //TODO: Change these requests to match the height and width of the parent element
            WidthRequest = 411;
            HeightRequest = 790;
            Opacity = 0.25;//Remove when finalising
            Color = Color.Black;//Remove when finalising

            //Variable initialisation
            CurrentPet = new Pet();
            Position_X = (int) Width/2;
            Position_Y = (int) Height/2;
            New_Position_X = Position_X;
            New_Position_Y = Position_Y;

            //Allow for touch input
            TouchEffect effect = new TouchEffect();
            effect.TouchAction += OnTouchEffectAction;
            Effects.Add(effect);

        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs e) {
            New_Position_X = (int) e.Location.X;
            New_Position_Y = (int) e.Location.Y;
        }

        public void Update() {
            /*
             * This function updates the game regardless of player interaction
             */
            if (Position_X > New_Position_X) {//Cursor is left of pet
                MoveLeft();
            } else if (Position_X < New_Position_X) {//Cursor is right of pet
                MoveRight();
            }
        }


        /*MOVEMENT*/
        public void MoveLeft() {
            if (Position_X > 0) {//Check if image does not exceed left edge
                Position_X--;
            }
        }

        public void MoveRight() {
            if (Position_X < Width) {//Check if image does not exceed right edge
                Position_X++;
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
