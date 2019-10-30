using System;
using Xamarin.Forms;

namespace TouchTracking {
    /*
     * NAME: TOUCHTRACKING
     * PURPOSE: To allow x and y feedback from when user touches an area of the screen
     * DATE: 29/9/19
     * SOURCE OF CODE AND ASSISTANCE: https://github.com/xamarin/xamarin-forms-samples/tree/master/Effects/TouchTrackingEffect
     * AUTHOR: Kirill Lyubimov
     * DESCRIPTION OF ASSISTANCE: Certain files were extracted and used in this project. This file is one of them.
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