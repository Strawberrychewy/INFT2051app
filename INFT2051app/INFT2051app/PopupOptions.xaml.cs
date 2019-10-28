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
using MediaManager.Player;
using MediaManager.Volume;
using MediaManager.Library;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;

namespace INFT2051app {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupOptions : PopupPage {

        readonly PopupRestartPrompt popupRestartPrompt;
        readonly PopUpNameChange popupNameChange;
        private readonly IMediaManager mediaManager;
        private IPlaybackManager PlaybackController => CrossMediaManager.Current;

        //private readonly PopupChangeName popupChangeName;
        readonly PopupCredits popupCredits;
        double SFXValue;
        double MusicValue;

        public PopupOptions() {
            InitializeComponent();
            SFXValue = 100;
            MusicValue = 100;
            popupCredits = new PopupCredits();
            popupNameChange = new PopUpNameChange();
            popupRestartPrompt = new PopupRestartPrompt();

            

        }
        
        //SLIDER EVENTS GO HERE (OPTIONS)
        public void SoundEffectChanged(object sender, ValueChangedEventArgs args) {
            /*
             * On Entering 0: Toggle the button visual to show muted
             * On leaving 0: Toggle the button visual to show unmuted
             * 
             */
            double value = args.NewValue;
            if (value == 0) {
                SFXButton.Text = "Muted";
            } else if (value > 0) {
                SFXButton.Text = "UnMuted";
            }
            SFXValue = value;
        }

        public void MusicVolumeChanged(object sender, ValueChangedEventArgs args) {
            /*
             * On Entering 0: Toggle the button visual to show muted
             * On leaving 0: Toggle the button visual to show unmuted
             * 
             */
            double value = args.NewValue;
            if (value == 0) {
                MusicButton.Text = "Muted";
            } else if (value > 0) {
                MusicButton.Text = "UnMuted";
            }
            MusicValue = value;

        }

        //BUTTON EVENTS GO HERE (OPTIONS)
        private async void OnMusicButtonClicked(object sender, EventArgs e) {
            /*
             * This event triggers when the Music Button is clicked
             * It will:
             * 1. Mute/Unmute the Music volume
             * 2. Mute: Visually show corresponding slider is on 0 
             * 3. Unmute: Visually show corresponding slider is on last saved value (OR 50, if thats too hard)
             */
 
            await CrossMediaManager.Current.PlayPause();

            if (CrossMediaManager.Current.IsPlaying() == true)
            {
                MusicButton.Text = "Music Off";
            }
            else if (CrossMediaManager.Current.IsStopped() == true)
            {
                MusicButton.Text = "Music On";
            }


            //if (MusicSlider.Value == 0)
            //{//UNMUTE
            //    MusicButton.Text = "UnMuted";
            //    if (MusicValue == 0)
            //    {
            //        MusicSlider.Value = 50;
            //    }
            //    else
            //    {
            //        MusicSlider.Value = MusicValue;
            //    }
            //}
            //else
            //{//MUTE
            //    MusicButton.Text = "Muted";
            //    MusicValue = MusicSlider.Value;

            //    /*  
            //     * Must unsubscribe to volume change before changing Slider value to 0
            //     * Otherwise it will trigger the volume change event and set the last saved value equal to 0
            //     * 
            //     * 
            //     */
            //    MusicSlider.ValueChanged -= MusicVolumeChanged;
            //    MusicSlider.Value = 0;
            //    MusicSlider.ValueChanged += MusicVolumeChanged;
            //}
        }

        private void OnSFXButtonClicked(object sender, EventArgs e) {
            /*
             * This event triggers when the SFX button is pressed.
             * 1. Mute/Unmute the SFX volume
             * 2. Mute: Visually show corresponding slider is on 0 
             * 3. Unmute: Visually show corresponding slider is on last saved value (OR 50, if thats too hard)
             */
            if (SFXSlider.Value == 0) {//UNMUTE
                SFXButton.Text = "UnMuted";
                if (SFXValue == 0) {
                    SFXSlider.Value = 50;
                } else {
                    SFXSlider.Value = SFXValue;
                }
            } else {//MUTE
                SFXButton.Text = "Muted";
                SFXValue = SFXSlider.Value;

                /*  
                 * Must unsubscribe to volume change before changing Slider value to 0
                 * Otherwise it will trigger the volume change event and set the last saved value equal to 0
                 * 
                 * 
                 */
                SFXSlider.ValueChanged -= SoundEffectChanged;
                SFXSlider.Value = 0;
                SFXSlider.ValueChanged += SoundEffectChanged;
            }
        }

        public void Update(Pet pet, PlayerData player) {
            popupCredits.updateText(pet, player);
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