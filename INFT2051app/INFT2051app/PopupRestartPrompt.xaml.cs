using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INFT2051app {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupRestartPrompt : PopupPage {


        public event EventHandler NewGame;


        public PopupRestartPrompt() {
            InitializeComponent();
        }

        public async void OnNoButtonClicked(object sender, EventArgs e) {
            //WHEN THE USER CLICKS NO, HE WILL BE TRANSPORTED THROUGH TIME AND SPACE TO BE SENT BACKWARD TO THE PREVIOUS MENU
            await PopupNavigation.Instance.PopAsync(true);
        }

        public async void OnYesButtonClicked(object sender, EventArgs e) {
            /*
             * When the user clicks "Yes", His save data will be destroyed and all hope is lost to recover it.
             * 
             * Honestly I don't know how to code this without ruining the game,
             * but I do know the steps necessary to make this function work:
             * 
             * 1. Delete the player save data, make sure that no other functions are reading any files in run time
             * 2. Restart the game, and ensure the player gets to input username etc again
             * 
             */
            NewGame(this, EventArgs.Empty);
            await PopupNavigation.Instance.PopAsync(true);
        }


    }
}