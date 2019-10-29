﻿using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INFT2051app {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupStatus : PopupPage {
        /*
         * This class will show all the detailing on the pet
         * The details shown will be
         * 
         * 1. Pet Name
         * 2. Pet Image
         * 3. Happiness
         * 4. Hunger
         * 5. ????
         * 
         * 
         * 
         */
        public string PlayerName { get; set; }

        public PopupStatus() {
            InitializeComponent();
        }

        public PopupStatus(string playerName) {
            InitializeComponent();

            PlayerName = playerName;
        }


        public void Update(Pet pet, PlayerData player) {
            playerLabel.Text = "Player Name: " + player.Name;
            petLabel.Text = "Pet Name: " + pet.NickName + ", Age: " + pet.Age;

            basePetNameLabel.Text = "Species: " + pet.Base.Name;
            basePetTypeLabel.Text = "Type: " + pet.Base.Type;
            basePetDescriptionLabel.Text = "Description: " + pet.Base.Description;

            healthBar.ProgressTo((float)pet.Health / 100, 250, Easing.Linear);
            hungerBar.ProgressTo((float)pet.Hunger / 100, 250, Easing.Linear);
            happinessBar.ProgressTo((float)pet.Happiness/100, 250, Easing.Linear);
            hygieneBar.ProgressTo((float)pet.Hygiene/100, 250, Easing.Linear);

        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            healthBar.ProgressTo(0, 250, Easing.Linear);
            hungerBar.ProgressTo(0, 250, Easing.Linear);
            happinessBar.ProgressTo(0, 250, Easing.Linear);
            hygieneBar.ProgressTo(0, 250, Easing.Linear);
        }

    }
}