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
    public partial class PopupOptions : PopupPage {
        public PopupOptions() {
            InitializeComponent();
        }
        //BUTTON EVENTS GO HERE (OPTIONS)
        public void SoundEffectChanged(object sender, ValueChangedEventArgs args) {
            /*
             * On Entering 0: Toggle the button visual to show muted
             * On leaving 0: Toggle the button visual to show unmuted
             * 
             */
            double value = args.NewValue;
        }
        public void MusicVolumeChanged(object sender, ValueChangedEventArgs args) {
            double value = args.NewValue;
        }
    }

}