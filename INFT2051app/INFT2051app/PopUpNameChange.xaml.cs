﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
/*
 * A6
* NAME: RG.PLUGIN.POPUP
* PURPOSE: To allow for popup menus and UI to be made as a main interface navigational tool
* DATE: 30/9/19
* SOURCE OF CODE AND ASSISTANCE: https://github.com/rotorgames/Rg.Plugins.Popup
* AUTHOR: Kirill Lyubimov
* DESCRIPTION OF ASSISTANCE: Directions on how to setup and create a popup page  
*/
namespace INFT2051app {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpNameChange : PopupPage {
        // readonly Entry petName;
        public string PlayerName { get; set; }
        public string PetName { get; set; }

        public event EventHandler PlayerNameChanged;
        public event EventHandler PetNameChanged;

        public PopUpNameChange() {
            InitializeComponent();


        }

        public void Update(PlayerData playerData) {
            //Updates the current strings in the popup namechange page
            PlayerName = playerData.Name;
            PetName = playerData.PetName;
        }

        private void EntryPlayerCompleted(object sender, EventArgs e) {
            PlayerName = ((Entry)sender).Text;
            PlayerNameChanged(this, EventArgs.Empty);
        }


        private void EntryPetCompleted(object sender, EventArgs e) {
            PetName = ((Entry)sender).Text;
            PetNameChanged(this, EventArgs.Empty);
        }

        private async void CompleteChangeName(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }


    }


}