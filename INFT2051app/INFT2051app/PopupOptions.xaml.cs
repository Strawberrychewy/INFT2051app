using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using MediaManager;
using MediaManager.Forms.Xaml;
using MediaManager.Playback;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;
/*
* NAME: RG.PLUGIN.POPUP
* PURPOSE: To allow for popup menus and UI to be made as a main interface navigational tool
* DATE: 30/9/19
* SOURCE OF CODE AND ASSISTANCE: https://github.com/xamarin/Essentials
* AUTHOR: Kirill Lyubimov
* DESCRIPTION OF ASSISTANCE: Directions on how to setup and create a popup page  
*/
namespace INFT2051app {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupOptions : PopupPage {

        public readonly PopupRestartPrompt popupRestartPrompt;
        public PopUpNameChange popupNameChange;
        private readonly IMediaManager mediaManager;

        readonly PopupCredits popupCredits;


        public PopupOptions() {
            InitializeComponent();
            popupCredits = new PopupCredits();
            popupNameChange = new PopUpNameChange();
            popupRestartPrompt = new PopupRestartPrompt();


        }


        //BUTTON EVENTS GO HERE (OPTIONS)
        private async void OnMusicButtonClicked(object sender, EventArgs e) {
            /*
             * This event triggers when the Music Button is clicked
             * It allows the user to toggle the music on or off
             */

            //=============================================
            //Reference A3
            //Purpose: pause and play music on button click
            //Date: Oct 24, 2019
            //Source: GitHub
            //URL: https://github.com/martijn00/XamarinMediaManager
            //Author: Martijn Van Dijk
            //Assistance: provided methods of play and pause
            //Adaptation: provided the if/else logic
            //=============================================

            if (CrossMediaManager.Current.IsPlaying() == true) {
                MusicButton.Text = "Music: Off";
                await CrossMediaManager.Current.Stop();
            }
            else if (CrossMediaManager.Current.IsStopped() == true) {
                MusicButton.Text = "Music: On";
                await CrossMediaManager.Current.Play();
            }

            //=============================================
            // End reference A3
            //=============================================

        }

        public void Update(PlayerData playerData) {
            popupNameChange.Update(playerData);
            popupCredits.Update(playerData);
        }

        //-------------------------------Bottom Button Events go here-----------------------------------------------------------
        private async void OnNameChangeClicked(object sender, EventArgs e) {

            await Navigation.PushPopupAsync(popupNameChange);
        }

        private async void OnCreditsClicked(object sender, EventArgs e) {
            /*
             * This event triggers upon Credits Button Clicked
             * It will:
             * 1. Pop the current popup
             * 2. Push the credits page popup
             * 
             */

            await PopupNavigation.Instance.PushAsync(popupCredits);

        }

        private async void OnRestartGameClicked(object sender, EventArgs e) {
            /*
             * This event triggers upon Restart button is Clicked
             * It will:
             * 1. Delete the save data
             * 2. Restart the app
             * 
             * NOTE: Be careful when deleting the data as it is used live in the game
             * It should not matter if the save dumps data into an object
             * but if the app continuously reads the save file during the game loop come crashes may occur
             * 
             * NOTE 2: This is probably the most difficult thing that I had to accomplish, because not only 
             * 
             */
            await PopupNavigation.Instance.PushAsync(popupRestartPrompt);
        }
    }

}