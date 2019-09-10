using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    class PetContainer {
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
        public int Position_X { get; set; } // X Position of current pet
        public int Position_Y { get; set; } // Y Position of current pet
        //Enum states = { Eating, Idle, Jumping };

        public PetContainer(int pos_X, int pos_Y) {
            Position_X = pos_X;
            Position_Y = pos_Y;
        }

        public void update() {
            /*
             * This function updates the game regardless of player interaction
             */

        }

        public void moveLeft() {
            if (true) {//Check if image does not exceed left edge
                Position_X--;

            }
        }

        public void moveRight() {
            if (true) {//Check if image does not exceed right edge
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
