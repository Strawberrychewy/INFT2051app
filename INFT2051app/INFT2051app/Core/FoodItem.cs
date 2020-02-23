using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace INFT2051app {
    public class FoodItem : ImageButton {
        /*
         * The base object for a food item.
         * This is practically a token object
         * 
         * 
         */
        public string Name { get; set;}
        public string Type { get; set;}
        public int Cost { get; set; }

        public FoodItem() {
        }

        public FoodItem(string name, string type, int cost) {
            Name = name;
            Type = type;
            Cost = cost;
            Source = "FoodItem_" + name + ".png";
            BackgroundColor = Color.Transparent;
        }
    }
}
