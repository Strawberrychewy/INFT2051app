using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MediaManager;
using MediaManager.Notifications;

using Plugin.Fingerprint;
using System.IO;

namespace INFT2051app.Android
{
    [Activity(Label = "Uni Pets", Icon = "@drawable/unipets", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        INotificationManager notificationManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            CrossMediaManager.Current.Init(this);

            CrossFingerprint.SetDialogFragmentType<FragmentFingerprint>();
            CrossFingerprint.SetCurrentActivityResolver(() => this);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            string savedata = "savedata.json";
            string folderpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string savepath = Path.Combine(folderpath, savedata);


            LoadApplication(new App(savepath));

            

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] global::Android.Content.PM.Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnStart() {
            base.OnStart();
            //CrossMediaManager.Current.PlayFromAssembly("appMusic.wav", typeof(MainPage).Assembly);
        }

        protected override void OnResume() {
            base.OnResume();
            //CrossMediaManager.Current.PlayFromAssembly("appMusic.wav", typeof(MainPage).Assembly);
        }

        //----------------------------------
        //Reference A4
        //Purpose: get music to stop playing when app closes
        //Date: 28 October 2019 
        //Source: Xamarin Forums
        //Author: Nickprovs
        //URL: https://forums.xamarin.com/discussion/106204/handling-onstop-cleanup-on-ios-and-cross-platform
        //Assistance: gave the idea that I could put my Stop() methods inside OnStop()
        //Adaptation: used Stop() instead of the user's methods
        //----------------------------------
        protected override void OnPause() {
            base.OnPause();
            CrossMediaManager.Current.Stop();
        }

        protected override void OnStop() {
            base.OnStop();
            CrossMediaManager.Current.Stop();

        }

        protected override void OnDestroy() {
            CrossMediaManager.Current.Stop();
        }
        //----------------------------------
        //End Reference A4
        //----------------------------------
    }
}