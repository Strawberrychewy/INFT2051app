namespace TouchTracking {
    /*
     * NAME: TOUCHTRACKING
     * PURPOSE: To allow x and y feedback from when user touches an area of the screen
     * DATE: 29/9/19
     * SOURCE OF CODE AND ASSISTANCE: https://github.com/xamarin/xamarin-forms-samples/tree/master/Effects/TouchTrackingEffect
     * AUTHOR: Kirill Lyubimov
     * DESCRIPTION OF ASSISTANCE: Certain files were extracted and used in this project. This file is one of them.
     */
    public enum TouchActionType {
        Entered,
        Pressed,
        Moved,
        Released,
        Exited,
        Cancelled
    }
}
