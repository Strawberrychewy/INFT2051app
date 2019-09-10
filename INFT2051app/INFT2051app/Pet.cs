using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    class Pet{
        /*
         * The Pet class defines the current pet that is used in the game.
         * The player can select from a number of pets that are within the JSON file. 
         * This JSON file can be edited for additional pets or quickly change the database.
         * 
         * RECOMMENDED: Load JSON into objects first instead of reading them constantly
         */

        //Variables changed by player
        string NickName { get; set; }// NickName (PLAYER INPUT)

        //Variables pulled from JSON file (A Base pet class can be formed so pets are loaded during runtime) 
        string Name { get; set; }// Name
        string Type { get; set; } // Type of Pet
        string Description { get; set; } // Description
        string SpritePath { get; set; } // Path of sprite animation
        //string SoundPath { get; set; } // Path of sound effect animation (Unused until sound is implemented)

        //Variables changed by game loop (Always updated)
        int Happiness { get; set; } // Happiness
        int Age { get; set; } // Age
        int Position_X { get; set; } // X Position
        int Position_Y { get; set; } // Y Position

        //BasePet Base {get; set;} //Base of the pet (If loading pets into objects at runtime [Slow start but better finish, does not matter too much if not many to load])

        public Pet() {


        }

        void Evolve() {
            /*
             * The pet will look at the next evolution in the base pet class/JSON file to evolve into
             * BASE CLASS USED, only "Base" variable needs to be changed
             * JSON FILE USED, need to search JSON file first for information, then replace information whereever necessary
             *
             */

        }

    }
}
