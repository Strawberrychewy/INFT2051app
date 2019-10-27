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

namespace INFT2051app.Android
{
    [Activity(Label = "INFT2051app", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        INotificationManager notificationManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            CrossMediaManager.Current.Init(this);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);


            CrossFingerprint.SetDialogFragmentType<FragmentFingerprint>();
            CrossFingerprint.SetCurrentActivityResolver(() => this);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            

        }
        protected void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] global::Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnStart()
        {
            base.OnStart();
            //CrossMediaManager.Current.PlayFromAssembly("appMusic.wav", typeof(MainPage).Assembly);
        }

        protected override void OnResume()
        {
            base.OnResume();
            //CrossMediaManager.Current.PlayFromAssembly("appMusic.wav", typeof(MainPage).Assembly);
        }

        protected override void OnPause()
        {
            base.OnPause();
            audioCleanup();
        }

        protected override void OnStop()
        {
            base.OnStop();
            audioCleanup();
        }

        protected override void OnDestroy()
        {
            audioCleanup();
        }

        protected void audioCleanup()
        {
            if (CrossMediaManager.Current.IsPlaying())
            {
                CrossMediaManager.Current.Stop();
            }
        }

    }
}