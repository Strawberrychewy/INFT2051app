using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    class FoodList {
        /*
         * Holds all the food items generated from the JSON file
         * This list could also double as a compendium of food items so the player can check what is in the game
         * 
         * 
         */

        List<FoodItem> Foods = new List<FoodItem>();

        public FoodList() {

        }

        public void LoadJSON(string filePath) {
            /*
             * This function takes the JSON file and turns each row into objects. This objects are added to the list.
             * 
             * Foods.add(e)
             */

        }
    }
}
