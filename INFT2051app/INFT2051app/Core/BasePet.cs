using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    public class BasePet {
        /*
         * The BasePet class defines one row of the EXCEL document as one object.
         * This could also be used as part of a journal so players can see which ones are obtained.
         * 
         */

        public string Name { get; set; }// Name
        public string Type { get; set; } // Type of Pet [LAND, SEA, AIR]
        public string Description { get; set; } // Description
        public string EvolvesFrom { get; set; } // Previous Evolution (NULL IF BASIC)
        public string EvolvesInto { get; set; } // Next Evolution (NULL IF FINAL)
        public string SpritePath { get; set; } // Path of sprite animation

        public BasePet( string name = "Rockworm", 
                        string type = "Land",
                        string evolvesFrom = null,
                        string evolvesInto = "Roccoon",
                        string description = "TestObject") {
            Name = name;
            Type = type;
            Description = description;
            SpritePath = "Pet_" + name + ".png";
            EvolvesFrom = evolvesFrom;
            EvolvesInto = evolvesInto;

        }

        public override String ToString() {
            String s = "[" + Name + ", ";
            s += Type + ", ";
            s += Description + ", ";
            s += EvolvesFrom + ", ";
            s += EvolvesInto + ", ";
            s += SpritePath + ", ";
            return s;
        }
    }
}
