using System;
using UnityEngine;

namespace Utility {
    public class CircularMovementY : MonoBehaviour, IMovementOneAxi {

        [SerializeField] private float speedMultiplier = 1.5f;
        [SerializeField] private Vector3 axy;

        public event Action<float> OnInput;

        public void Move (float input) {
            OnInput?.Invoke(input);
            transform.Rotate(axy, input * speedMultiplier, Space.Self);
        }
    }
}
