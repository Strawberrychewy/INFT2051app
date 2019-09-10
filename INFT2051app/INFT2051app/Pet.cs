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

        //Variables changed by game loop (Always updated)
        public int Happiness { get; set; } // Happiness
        public int Age { get; set; } // Age

        BasePet Base {get; set;} //Base of the pet (If loading pets into objects at runtime [Slow start but better finish, does not matter too much if not many to load])

        public Pet() {
            Base = new BasePet();

        }

        public void Evolve() {
            /*
             * The pet will look at the next evolution in the base pet class/JSON file to evolve into
             * BASE CLASS USED, only "Base" variable needs to be changed based on next evolution of current Base
             * JSON FILE USED, need to search JSON file first for all information, then replace information whereever necessary
             *
             * The below code specifies using swapping the BASE object for one in the entire dex
             */
            string EvolveInto = Base.EvolvesInto; //Finds the next evolution chain object from its current Base object
        }

        //HELPER FUNCTIONS (UNHAPPY<--------------NEUTRAL-------------->HAPPY)
        public bool IsUnhappy() {
            return Happiness < 30; 
        }
        public bool IsNeutral() {
            return !IsUnhappy() && !IsHappy();
        }
        public bool IsHappy() {
            return Happiness > 60;
        }
    }
}
