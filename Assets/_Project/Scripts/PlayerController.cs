using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : Subject<IObserver>
    {
        [SerializeField] private InputReader input;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector3 movement;

        [SerializeField] private  float movSpeed = 200f;
        [SerializeField] private float rotationSpeed = 200.0f;

        [SerializeField] private Transform mainCamera;

        private void Awake ()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            mainCamera = Camera.main.transform;
        }
        private void Start ()
        {
            input.EnablePlayerActions();
            NotifyObservers();
        }

        private void OnEnable ()
        {
            input.Move += GetMovement;
        }

        private void OnDisable ()
        {
            input.Move -= GetMovement;
        }

        private void FixedUpdate ()
        {
            UpdateMovement();
        }

        private void UpdateMovement () 
        {
            var adjustedDirection = Quaternion.AngleAxis(mainCamera.eulerAngles.y, Vector3.up) * movement;
                HandleRoation(adjustedDirection);
            if (adjustedDirection.magnitude > 0)
            {
                HandleMovement(adjustedDirection);
            }
            else 
            {
                //Not change the rotation or movement, but need to apply rigidbody Y movement for gravity
                rb.linearVelocity = new Vector3(0.0f, rb.linearVelocity.y, 0.0f);
            }
        }
        private void HandleMovement (Vector3 adjustedMovement)
        {
            var velocity = adjustedMovement * movSpeed*Time.fixedDeltaTime;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }

        private void HandleRoation (Vector3 adjustedMovement)
        {
            var targetRotation = Quaternion.AngleAxis(mainCamera.eulerAngles.y, Vector3.up) ;
            transform.rotation = targetRotation;
        }

        private void GetMovement (Vector2 move)
        {
            movement.x = move.x;
            movement.z = move.y;
        }
        public override void NotifyObservers ()
        {
            foreach (IObserver observer in observers)
            {
                observer.OnNotify();
            }
        }
    }
}
