using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    class Egg {
        /*
         * The Egg Class specifies which type the egg is (LAND, SEA, AIR)
         * The player could either choose which egg they want or get an egg randomly
         * For now the egg is generated randomly
         * 
         */

        string Type { get; set; } //TYPE OF EGG (LAND, SEA, AIR)
        string SpritePath { get; set; } // SPRITE PATH OF EGG

        public Egg() {
            Type = "Land";
            SpritePath = "";
        }

        public void Hatch() {

        }
    }
}
