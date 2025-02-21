using System;

namespace timer {
    public interface IPauser
    {
        event Action<bool> onPause;
        bool inPause { get; }

        public void Pause ();
        public void Resume();
    }
}