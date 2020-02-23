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

            //Image Properties
            Aspect = Aspect.AspectFill;
            AbsoluteLayout.SetLayoutBounds(this, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.All);

        }

        public void AdaptBackground() {
            /*The following code checks the current time and changes the opening background splash based on time
             * Morning: 5am to 12pm
             * Noon: 12pm to 5pm
             * Night: 5pm to 5am
             * 
             */
            switch (DateTime.Now.Hour) {
                case int n when (n >= 5 && n < 12):
                    Source = "bg_morning";
                    break;
                case int n when (n >= 12 && n < 17):
                    Source = "bg_noon";
                    break;
                default:
                    Source = "bg_night";
                    break;
            }
        }
    }
}
