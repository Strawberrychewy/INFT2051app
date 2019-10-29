using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051app {
    public class PlayerData {
        //Player Information
        public string Name { get; set; }
        public int Credits { get; set; }
        //Pet Information
        public string PetName { get; set; }
        public string BasePet { get; set; }
        public int Hunger { get; set; }
        public int Health { get; set; }
        public int Happiness { get; set; }
        public int Hygiene { get; set; }
        public int Age { get; set; }

        public PlayerData(string name = "Chewy", int credits = 1000, string petname = "Chewy", string basePet = "Rockworm", int age = 4, int hunger = 50, int health = 100, int happiness = 50, int hygiene = 50) {
            Name = name;
            Credits = credits;

            PetName = petname;
            BasePet = basePet;
            Age = age;
            Hunger = hunger;
            Happiness = happiness;
            Hygiene = hygiene;
            Health = health;
        }


    }
}
