using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using FFImageLoading.Forms;

namespace INFT2051app {
    public class Pet : Image{
        /*
         * The Pet class defines the current pet that is used in the game.
         * The player can select from a number of pets that are within the JSON file. 
         * This JSON file can be edited for additional pets or quickly change the database.
         * 
         * RECOMMENDED: Load JSON into objects first instead of reading them constantly
         */

        //Variables changed by player
        public string NickName { get; set; }// NickName (PLAYER INPUT)
        public FamiliarsList PetList { get; set; } = new FamiliarsList();
        //Variables changed by game loop (Always updated)
        public int Health { get; set; } //Health
        public int Happiness { get; set; } // Happiness
        public int Age { get; set; } // Age
        public int Hunger { get; set; } // Hunger
        public int Hygiene { get; set; } //Hygiene

        public event EventHandler EvolutionReady;

        //This set of enums describe the range of states the pet can have per parameter
        public enum PetState {Awake, Asleep, Dead}//Awake from start of morning until night time, sleep time differs on age
        public enum HealthState {UnHealthy, Healthy, Neutral } //Health starts at 100% and lowers if [Hungry] or [Dirty]
        public enum HappinessState {UnHappy, Happy, Neutral } //Happiness starts at 50% and the rate of change differs on both bad and good states
        public enum HungerState {Starving, Hungry, Neutral, Full, Overfed } //Hunger ranges are [0-20, 20-50, 40-70, 70-100, >100] Overfeeding and starving will also lower health
        public enum HygieneState {Dirty, Clean, Neutral }

        //This set of states describe the current state of the pet per parameter
        public PetState PetStateValue { get; set; }
        public HealthState HealthStateValue { get; set; }
        public HappinessState HappinessStateValue { get; set; }
        public HungerState HungerStateValue { get; set; }
        public HygieneState HygieneStateValue { get; set; }

        public BasePet Base {get; set;} //Base of the pet (If loading pets into objects at runtime [Slow start but better finish, does not matter too much if not many to load])

        public Pet(string nickname = "Josh", int happiness = 50, int age = 0, int hunger = 50, int hygiene = 50, int health = 100, string basepet = "Rockworm") {
            NickName = nickname;
            Happiness = happiness;
            Age = age;
            Hunger = hunger;
            Hygiene = hygiene;
            Health = health;
            Base = PetList.FindPetByName(basepet);

            //Image properties
            Source = "Pet_" + basepet + ".png";//CHANGE THIS TO PET IMAGE FILE NAMES ["Pet_" + Base.Name + ".png"]
            AbsoluteLayout.SetLayoutBounds(this, new Rectangle(0.5, 0.8, 0.2, 0.2));
            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.All);

            UpdatePetState();
            UpdateHygieneState();
            UpdateHungerState();
            UpdateHealthState();
            UpdateHappinessState();
        }

        public void ChangePet(string basepet = "Rockworm") {
            Base = PetList.FindPetByName(basepet);
            Source = "Pet_" + basepet + ".png";//CHANGE THIS TO PET IMAGE FILE NAMES ["Pet_" + Base.Name + ".png"]
        }
        public void CapValues() {
            if (Health > 100) {
                Health = 100;
            }
            if (Happiness > 100) {
                Health = 100;
            }
            if (Hygiene > 100) {
                Health = 100;
            }
        }

        public void UpdateStatus(int timeInterval = 1) {
            /*
             * This code is run at 30 minute intervals 
             * When the game is closed or put into the background, the app records the current time
             *      the game exits, and compares the difference between that and the return time.
             *      this code will then run those intervals upon return to make up for the background discrepancy
             * At every interval:
             * 1. If Hunger is already at a poor state, or is overfed, Health reduces by 5 (Hungry/Overfed) or 10 (Starving)
             * 2. If Hygiene is already at a bad state, Health reduces by 5
             * 3. If Happiness is already at a bad state, Health reduces by 5
             * 4. Lower Hygiene by a value (Current Value 5)
             * 5. Lower Hunger by a value (5 when not poor, 8 when poor). This value is reduced by 2 when the pet is asleep
             * 
             * 6. If the 24th interval is run, (12hrs), Health reduces by 1 for every 1 age above the 'old' cap (Old cap currently is at 10)
             * 7. When the 48th interval is run (24hrs), increment the age counter by 1
             * 8. When all stats are 'good' and the age is equal or above 5, the pet is able to 'evolve'
             * 
             */
            for(int i = 0, j = 0; i < timeInterval && PetStateValue != PetState.Dead; i++) {
                j++;
                Interval_1();//Run this code every 1 step
                if (j == 12) {
                    Interval_12();//Run this code every 12 steps
                }
                if (i == 24) {
                    Interval_12();//Run this code every 12 steps (factor of 24th interval)
                    Interval_24();//run this code every 24 steps
                    j = 0;//reset
                }
            }
            //Update states based on values
            UpdateHappinessState();
            UpdateHealthState();
            UpdateHungerState();
            UpdateHygieneState();
            UpdatePetState();
            CapValues();
        }

        private void Interval_1() {
            /*
             * This code simulates every 1 hour that passes
             * 
             */

            //DECREMENT HEALTH BASED ON OTHER STATES
            //HUNGER
            if (HungerStateValue == HungerState.Starving) {
                Health -= 10;
            } else if (HungerStateValue == HungerState.Overfed || HungerStateValue == HungerState.Hungry) {
                Health -= 5;
            }
            //HYGIENE
            if (HygieneStateValue == HygieneState.Dirty) {
                Health -= 5;
            }
            //HAPPINESS
            if (HappinessStateValue == HappinessState.UnHappy) {
                Health -= 5;
            }
            if (Health <= 0) {
                //Pet dies
                PetStateValue = PetState.Dead;
            }

            if (PetStateValue != PetState.Dead) {//If the pet has not died to this point
                //DECREMENT HYGIENE
                Hygiene -= 5;

                //DECREMENT HUNGER
                int hungerDecrement;
                if (HungerStateValue == HungerState.Starving) {
                    hungerDecrement = 8;
                }
                else {
                    hungerDecrement = 5;
                }
                if (PetStateValue == PetState.Asleep) {
                    hungerDecrement -= 2;
                }
                Hunger -= hungerDecrement;
            }
        }

        private void Interval_12() {
            /*
             * This code simulates every 12 hours that pass
             * 
             */
            if (Age > 10) {//If Health is over 10
                Health -= Age - 10; //Subtract difference
            }
        }

        private void Interval_24() {
            /*
             * This code simulates every 24 hours that pass
             * 
             */
            Age++;//Increment Age
            if (Happiness >= 50 && Health >= 50 && Hygiene >= 50 && Age >= 5) {
                EvolutionReady(this, EventArgs.Empty);
            }
        }
        //----------------------------------------------IDLE STATE ANIMATIONS-------------------------------------------------------------------------------
        public async void BounceHigh() {
            await this.TranslateTo(TranslationX, -200, 200, Easing.Linear);//UP
            await this.TranslateTo(TranslationX, 0, 200, Easing.Linear);//DOWN
        }

        public async void BounceLow() {
            await this.TranslateTo(TranslationX, -100, 100, Easing.Linear);//UP
            await this.TranslateTo(TranslationX, 0, 100, Easing.Linear);//DOWN
        }

        public async void BounceMicro() {
            await this.TranslateTo(TranslationX, -50, 50, Easing.Linear);//UP
            await this.TranslateTo(TranslationX, 0, 50, Easing.Linear);//DOWN
            await this.TranslateTo(TranslationX, -25, 25, Easing.Linear);//UP
            await this.TranslateTo(TranslationX, 0, 25, Easing.Linear);//DOWN
            await this.TranslateTo(TranslationX, -10, 10, Easing.Linear);//UP
            await this.TranslateTo(TranslationX, 0, 10, Easing.Linear);//DOWN
        }

        public async void BounceJump() {
            await this.TranslateTo(TranslationX, -200, 200, Easing.Linear);//UP
            await this.RotateTo(+360, 360, Easing.Linear);//ROTATE 360
            this.Rotation = 0;//RESET ROTATION VARIABLE
            await this.TranslateTo(TranslationX, 0, 200, Easing.Linear);//DOWN

        }
        //----------------------------------------------UPDATE CURRENT STATES-------------------------------------------------------------------------------
        /*
         * This code updates each of the Current State Values to parameters within the range of its respective enum
         * 
         */
        private void UpdateHappinessState() {
            switch (Happiness) {
                case int n when (n < 30):
                    HappinessStateValue = HappinessState.UnHappy;
                    break;
                case int n when (n < 60):
                    HappinessStateValue = HappinessState.Neutral;
                    break;
                default:
                    HappinessStateValue = HappinessState.Happy;
                    break;
            }
        }

        private void UpdateHungerState() {
            switch (Hunger) {
                case int n when (n < 30):
                    HungerStateValue = HungerState.Starving;
                    break;
                case int n when (n < 50):
                    HungerStateValue = HungerState.Hungry;
                    break;
                case int n when (n < 70):
                    HungerStateValue = HungerState.Neutral;
                    break;
                case int n when (n <= 100):
                    HungerStateValue = HungerState.Full;
                    break;
                case int n when (n > 100):
                    HungerStateValue = HungerState.Overfed;
                    break;
            }
        }

        private void UpdateHygieneState() {
            switch (Hygiene) {
                case int n when (n < 30):
                    HygieneStateValue = HygieneState.Dirty;
                    break;
                case int n when (n < 60):
                    HygieneStateValue = HygieneState.Neutral;
                    break;
                default:
                    HygieneStateValue = HygieneState.Clean;
                    break;
            }
        }

        private void UpdateHealthState() {
            switch (Health) {
                case int n when (n < 30):
                    HealthStateValue = HealthState.UnHealthy;
                    break;
                case int n when (n < 60):
                    HealthStateValue = HealthState.Neutral;
                    break;
                default:
                    HealthStateValue = HealthState.Healthy;
                    break;
            }
        }

        private void UpdatePetState() {
            if (DateTime.Now.Hour > 5 && DateTime.Now.Hour < 17 && PetStateValue != PetState.Dead) {
                PetStateValue = PetState.Awake;
            } else {
                PetStateValue = PetState.Asleep;
            }
        }

        public override String ToString() {
            String s = "[Nickname: " + NickName;
            s += ", Happiness: " + Happiness;
            s += ", Age: " + Age +"]";
            s += "\n" + Base.ToString();
            return s;
        }
    }
}
