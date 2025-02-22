using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static InputSystem_Actions;

namespace Surviblewilderness
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction Interact = delegate { };
        InputSystem_Actions input;

        private void OnEnable ()
        {
            if (input == null)
            {
                input = new InputSystem_Actions();
                input.Player.SetCallbacks(this);            
            }
        }

        public void EnablePlayerActions ()
        {
            input.Enable();
        }

        public void DisablePlayerActions ()
        { 
            input.Disable();
        }

        public void OnAttack (InputAction.CallbackContext context)
        {
        }

        public void OnCrouch (InputAction.CallbackContext context)
        {
        }

        public void OnInteract (InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Interact?.Invoke();
                    break;
                default:
                    break;
            }
        }

        public void OnJump (InputAction.CallbackContext context)
        {
        }

        public void OnLook (InputAction.CallbackContext context)
        {
        }

        public void OnMove (InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Canceled:
                case InputActionPhase.Performed:
                    Move?.Invoke(context.ReadValue<Vector2>());
                    break;
                default:
                    break;
            }
        }

        public void OnNext (InputAction.CallbackContext context)
        {
        }

        public void OnPrevious (InputAction.CallbackContext context)
        {
        }

        public void OnSprint (InputAction.CallbackContext context)
        {
        }
    }
}
