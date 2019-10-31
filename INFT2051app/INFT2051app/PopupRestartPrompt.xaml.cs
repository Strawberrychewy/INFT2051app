using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public partial class PopupRestartPrompt : PopupPage {


        public event EventHandler NewGame;

        public PopupRestartPrompt() {
            InitializeComponent();
        }

        public async void OnNoButtonClicked(object sender, EventArgs e) {
            
            await PopupNavigation.Instance.PopAsync(true);
        }

        public async void OnYesButtonClicked(object sender, EventArgs e) {
            /*
             * When the user clicks "Yes", saved data will be destroyed.
             * 1. Deletes the player save data, make sure that no other functions are reading any files in run time
             * 2. Restarts the game, and ensure the player gets to input username etc again
             * 
             */
            NewGame(this, EventArgs.Empty);
            await PopupNavigation.Instance.PopAsync(true);
        }


    }
}