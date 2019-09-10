using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace INFT2051app {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        /*
         * This is the Main page for the Application, but not the root. The root is the App class
         * This page handles two different timings
         * - one for frames (INSTANTANEOUS animations and input controls)
         * - one for real world time (EVENTS hunger and age)
         * It is important to split these two apart as there is both a slow and fast element in this application
         * 
         */

        private FoodList foodList;  //Contains the entire food database
        private FoodShop foodShop;  //Contains a random x items from food pool
        private FamiliarsList familiarsList;    //Contains the entire Pet list
        private PetContainer petContainer;  //Controller for the pet
        private int credits;

        public MainPage() {
            InitializeComponent();
        }

        public void Init() {
            /*
             * This init function does the following things
             * - Initialise databases, loading from JSON
             * - Initialise petContainer, setting a random egg
             * 
             * 
             */
            credits = 0;
        }
        public void Step() {
            /*
             * The step function specifies what happens in one instance of the loop.
             * It does the following things (IN ORDER)
             * 
             * Read user input
             * Check Movement Collision
             * Perform Movement (If Any)
             * Check changes to State Change
             * Perform Rendering
             */
        }
    }
}
