using UnityEngine;
using Utility;

namespace input {
    public abstract class InputKeyCode : MonoBehaviour, IUpdate {
        [SerializeField] private KeyCode keyCode;

        private int _idx = 0;

        public int idx => _idx;

        public void OnUpdate (float deltaTime) {
            if (!Input.GetKeyDown(keyCode))
                return;
            OnKeyPressed();
        }

        abstract protected void OnKeyPressed ();
    }
}
