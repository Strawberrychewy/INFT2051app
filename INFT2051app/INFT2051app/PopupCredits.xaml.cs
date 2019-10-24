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
    public partial class PopupCredits : PopupPage {

        PopUpNameChange popupNameChange;
        Pet pet;

        public PopupCredits() {
            InitializeComponent();
        }

        public void updateText() {
            /*
             * This function will read the player data and update the text in the xaml
             * 1. Name of the Pet (Name_Pet)
             * 2. Name of the Player (Name_Player)
             */
             //this is hardcoded
            this.Name_Pet.Text = "Josh";
        }



    }
}