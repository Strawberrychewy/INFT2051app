using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Plugin.Fingerprint.Dialog;

namespace INFT2051app.Android {
    public class FragmentFingerprint : FingerprintDialogFragment {
        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            view.Background = new ColorDrawable(Color.Ivory);
            return view;
        }
    }
}