using Xamarin.Forms;

namespace TouchTracking {
    /*
   * The touchTracking information can be Found here
   *
   *https://github.com/xamarin/xamarin-forms-samples/tree/master/Effects/TouchTrackingEffect
   *   
   *
   */
    public class TouchEffect : RoutingEffect {
        public event TouchActionEventHandler TouchAction;

        public TouchEffect() : base("XamarinDocs.TouchEffect") {
        }

        public bool Capture { set; get; }

        public void OnTouchAction(Element element, TouchActionEventArgs args) {
            TouchAction?.Invoke(element, args);
        }
    }
}