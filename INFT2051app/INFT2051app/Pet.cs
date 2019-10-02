using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using FFImageLoading.Forms;

namespace INFT2051app {
    class Pet : Image{
        /*
         * The Pet class defines the current pet that is used in the game.
         * The player can select from a number of pets that are within the JSON file. 
         * This JSON file can be edited for additional pets or quickly change the database.
         * 
         * RECOMMENDED: Load JSON into objects first instead of reading them constantly
         */

        //Variables changed by player
        public string NickName { get; set; }// NickName (PLAYER INPUT)

        //Variables changed by game loop (Always updated)
        public int Happiness { get; set; } // Happiness
        public int Age { get; set; } // Age
        public int Hunger { get; set; } // Age

        public String State { get; set; }

        BasePet Base {get; set;} //Base of the pet (If loading pets into objects at runtime [Slow start but better finish, does not matter too much if not many to load])

        public Pet(string nickname = "Chewy", int happiness = 0, int age = 0, int hunger = 0) {
            NickName = nickname;
            Happiness = happiness;
            Age = age;
            Hunger = hunger;
            Base = new BasePet();
            State = "";



            //Image properties
            Source = "egg_brown.png";
            AbsoluteLayout.SetLayoutBounds(this, new Rectangle(0.5, 0.8, 0.2, 0.2));
            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.All);

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

        public string Status() {
            /*
             * This function checks the Happiness variable and returns the status based on the range 
             * 
             * Under 30: Unhappy
             * Above 60: Happy
             * Between 30 and 60: Neutral
             */
            if (Happiness < 30) {
                return "Unhappy";
            } else if (Happiness > 60) {
                return "Happy";
            } else {
                return "Neutral";
            }
        }

        public override String ToString() {
            String s = "[Nickname: " + NickName;
            s += ", Happiness: " + Happiness;
            s += ", Age: " + Age +"]";
            s += "\n" + Base.ToString();
            return s;
        }
    }
}
