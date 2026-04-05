using System;

namespace LastKnownPosition.Events
{
    public class OnStepChangedEventArgs : EventArgs
    {
        public string Text { get; set; }
    }
}