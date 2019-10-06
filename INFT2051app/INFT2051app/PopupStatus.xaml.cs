using Rg.Plugins.Popup.Pages;
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
        public PopupStatus() {
            InitializeComponent();
        }

        public void Update(String name, int happiness, int hunger) {
            titleLabel.Text = "Status: " + name;
            happinessBar.ProgressTo((float)happiness/100, 250, Easing.Linear);
            hungerBar.ProgressTo((float)hunger/100, 250, Easing.Linear);

        }


    }
}