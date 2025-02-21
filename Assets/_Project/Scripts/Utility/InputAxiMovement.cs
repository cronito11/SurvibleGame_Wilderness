using UnityEngine;
using Utility;

namespace input {
    public class InputAxiMovement : MonoBehaviour, IUpdate {
        [SerializeField] private string axiInput;

        private IMovementOneAxi movementOneAxi;
        private int _idx = 0;

        public int idx => _idx;

        public void OnUpdate (float deltaTime) {
            movementOneAxi.Move(Input.GetAxis(axiInput));
        }

        private void Start () {
            movementOneAxi = GetComponent<IMovementOneAxi>();
        }
    }
}