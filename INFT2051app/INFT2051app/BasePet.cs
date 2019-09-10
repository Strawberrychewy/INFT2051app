using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    class BasePet {
        /*
         * The BasePet class defines one row of the EXCEL document as one object.
         * This could also be used as part of a journal so players can see which ones are obtained.
         * 
         * 
         * 
         */

        public string Name { get; set; }// Name
        public string Type { get; set; } // Type of Pet [LAND, SEA, AIR]
        public string Stage { get; set; } // Stage of Pet (BASIC, MIDDLE, FINAL) MIGHT NOT NEED THIS AS BASIC PETS HAVE NO PREVIOUS EVOLUTION
        public string Description { get; set; } // Description
        public string EvolvesFrom { get; set; } // Previous Evolution (NULL IF BASIC)
        public string EvolvesInto { get; set; } // Next Evolution (NULL IF FINAL)
        public string SpritePath { get; set; } // Path of sprite animation
        public string SoundPath { get; set; } // Path of sound effect animation (Unused until sound is implemented)

        //TODO: ADD ACTUAL DEFAULT PARAMETERS SO CONSTRUCTOR BECOMES FOOLPROOF
        public BasePet( string name = "", 
                        string type = "", 
                        string stage = "", 
                        string description = "", 
                        string evolvesInto = "",
                        string evolvesFrom = "",
                        string spritePath = "", 
                        string soundPath = "") {
            Name = name;
            Type = type;
            Stage = stage;
            Description = description;
            SpritePath = spritePath;
            SoundPath = soundPath;
            EvolvesFrom = evolvesFrom;
            EvolvesInto = evolvesInto;

        }
    }
}
