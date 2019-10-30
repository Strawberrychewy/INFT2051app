using System;
using Xamarin.Forms;

namespace TouchTracking {
    /*
    * The touchTracking information can be found here
    *
    *https://github.com/xamarin/xamarin-forms-samples/tree/master/Effects/TouchTrackingEffect
    *   
    *
    */
    public class TouchActionEventArgs : EventArgs {
        public TouchActionEventArgs(long id, TouchActionType type, Point location, bool isInContact) {
            Id = id;
            Type = type;
            Location = location;
            IsInContact = isInContact;
        }

        public long Id { private set; get; }

        public TouchActionType Type { private set; get; }

        public Point Location { private set; get; }

        public bool IsInContact { private set; get; }
    }
}