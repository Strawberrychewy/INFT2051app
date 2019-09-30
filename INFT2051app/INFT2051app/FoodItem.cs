using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    class FoodItem {
        /*
         * The base object for a food item.
         * This is practically a token object
         * 
         * 
         */
        public string Name { get; set;}
        public string Type { get; set;}
        public int Cost { get; set;}

        public FoodItem() {

        }

        public FoodItem(string name, string type, int cost ) {
            Name = name;
            Type = type;
            Cost = cost;
        }
    }
}
