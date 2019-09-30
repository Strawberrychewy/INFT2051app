using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace INFT2051app {
    class FoodList : BoxView {
        /*
         * Holds all the food items generated from the JSON file
         * This list could also double as a compendium of food items so the player can check what is in the game
         * 
         * EDIT: FoodShop is now under this class so it becomes easier to distribute items from the main list into it.
         */

        //ONE LIST FOR THE FULL FOOD LIST
        readonly List<FoodItem> Foods = new List<FoodItem>();

        //ONE SUBLIST FOR SHOP
        readonly List<FoodItem> foodShop = new List<FoodItem>();  //Contains a random x items from food pool

        public FoodList() {
            //This changes the dimensions of the box size to match the page height and width.

            LoadJSON();
            resetShop();

        }
        public void LoadJSON() {
            /*
             * This function takes the JSON file and turns each row into objects. This objects are added to the list.
             * THIS FUNCTION IS FAKE ATM DON'T SUE ME
             * Foods.add(e)
             */

            Foods.Add(new FoodItem("Carrot", "Vegetable", 10));
            Foods.Add(new FoodItem("Apple", "Fruit", 10));
            Foods.Add(new FoodItem("Orange", "Fruit", 11));
            Foods.Add(new FoodItem("Blueberries", "Fruit", 12));

            Foods.Add(new FoodItem("Grapes", "Fruit", 11));
            Foods.Add(new FoodItem("Fish", "Meat", 16));
            Foods.Add(new FoodItem("Lettuce", "Vegetable", 13));
            Foods.Add(new FoodItem("Steak", "Meat", 20));
            Foods.Add(new FoodItem("Chicken", "Meat", 19));

        }

        public List<FoodItem> resetShop() {
            List<FoodItem> shop = new List<FoodItem>();
            for (int i = 0; i < 8; i++) {
                var random = new Random();
                int index = random.Next(Foods.Count);
                FoodItem item = Foods[index];
                shop.Add(item);
            }
            return shop;
        }

        public FoodItem findFoodItem(string name) {
            return Foods.Find(x => x.Name == name);
        }

    }
}
