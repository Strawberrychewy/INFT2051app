using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    class FoodShop {
        /*
         * The foodshop class holds a limited amount of food items (currently 3)
         * These food items are taken randomly from the set list
         * 
         * 
         * 
         */

        FoodItem[] Shop = new FoodItem[3];
        public FoodShop() {

        }

        public void Restock() {
            /*
             * Clears the Shop array and restocks it with three random items from the full list
             * 
             */
            Array.Clear(Shop, 0, Shop.Length-1);//Clear the array (Array Name, start, stop)
        }
    }
}
