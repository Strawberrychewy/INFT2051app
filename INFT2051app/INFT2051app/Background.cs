using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INFT2051app {
    class Background : Image{

        public Background() {
            AdaptBackground();


        }


        public void AdaptBackground() {
            /*The following code checks the current time and changes the opening background splash based on time
             * Morning: 5am to 12pm
             * Noon: 12pm to 5pm
             * Night: 5pm to 5am
             * 
             */
            if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour < 12) {
                //MORNING
                Source = "bg_morning";
            }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 17) {
                //NOON
                Source = "bg_noon";
            }
            else {
                //NIGHT
                Source = "bg_night";
            }
        }
    }
}
