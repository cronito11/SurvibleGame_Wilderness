using System;


namespace Utility {
    interface IMovementOneAxi {
        event Action<float> OnInput;
        void Move (float input);
    }
}