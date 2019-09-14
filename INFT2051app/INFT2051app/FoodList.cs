using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    class FoodList {
        /*
         * Holds all the food items generated from the JSON file
         * This list could also double as a compendium of food items so the player can check what is in the game
         * 
         * EDIT: FoodShop is now under this class so it becomes easier to distribute items from the main list into it.
         */

        List<FoodItem> Foods = new List<FoodItem>();

        FoodShop foodShop;  //Contains a random x items from food pool

        public FoodList() {

        }

        public void LoadJSON(string filePath) {
            /*
             * This function takes the JSON file and turns each row into objects. This objects are added to the list.
             * 
             * Foods.add(e)
             */

        }

        public void Purchase(int ID) {
            /*
             * This function purchases the item in the entire list
             * 
             */
        }

    }
}
