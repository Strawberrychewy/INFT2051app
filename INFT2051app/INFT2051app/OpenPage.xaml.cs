using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MediaManager;

namespace INFT2051app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenPage : ContentPage
    {
        
        private readonly MainPage gamePage;
        private readonly Timer subtitleTimer;
        
        public OpenPage(MainPage gamePage) {
            InitializeComponent();

            this.gamePage = gamePage;
            var number = 0;


            Background bg = new Background();
            this.FindByName<Image>("backgroundPic").Source = bg.Source;

            //
            subtitleTimer = new Timer(3 * 1000);//3 Seconds
            subtitleTimer.Elapsed += subtitleStep;
            subtitleTimer.AutoReset = true;
            subtitleTimer.Start();

            open_layout.RaiseChild(this.FindByName<Button>("welcome"));

            var moveGesture = new TapGestureRecognizer();
            moveGesture.NumberOfTapsRequired = 1;
            

        }

        private void subtitleStep(object source, ElapsedEventArgs e) {
            subtitleTimer.Stop();
            BobUpAndDown();
            subtitleTimer.Start();
        }

        private async void BobUpAndDown() {
            /*
             * This code will make the subtitle bob up and down
             * so that the user may be inclined to actually start the game
             */
            uint time = 50;
            await OpeningSubtitle.TranslateTo(0, -20, time, Easing.SinInOut);
            await OpeningSubtitle.TranslateTo(0, 20, time, Easing.Linear);
            await OpeningSubtitle.TranslateTo(0, -15, time, Easing.SinInOut);
            await OpeningSubtitle.TranslateTo(0, 15, time, Easing.Linear);
            await OpeningSubtitle.TranslateTo(0, -10, time, Easing.SinInOut);
            await OpeningSubtitle.TranslateTo(0, 10, time, Easing.Linear);
            await OpeningSubtitle.TranslateTo(0, -5, time, Easing.SinInOut);
            await OpeningSubtitle.TranslateTo(0, 5, time, Easing.Linear);
            OpeningSubtitle.TranslationY = 0;
        }

        
        private async void MoveToApp(object sender, EventArgs e) {

            await Navigation.PushAsync(new MainPage());
            await CrossMediaManager.Current.PlayFromAssembly("appMusic.wav", typeof(MainPage).Assembly);
            

        }

    }
}