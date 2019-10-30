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
* NAME: RG.PLUGIN.POPUP
* PURPOSE: To allow for popup menus and UI to be made as a main interface navigational tool
* DATE: 30/9/19
* SOURCE OF CODE AND ASSISTANCE: https://github.com/xamarin/Essentials
* AUTHOR: Kirill Lyubimov
* DESCRIPTION OF ASSISTANCE: Directions on how to setup and create a popup page  
*/
namespace INFT2051app {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupCredits : PopupPage {

        public PopupCredits() {
            InitializeComponent();
        }

        public void Update(PlayerData playerData) {
            /*
             * This function will read the player data and update the text in the xaml
             * 1. Name of the Pet (Name_Pet)
             * 2. Name of the Player (Name_Player)
             */

            Name_Pet.Text = playerData.PetName;
            Name_Player.Text = playerData.Name;
        }



    }
}