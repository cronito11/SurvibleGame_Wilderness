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

        [SerializeField] private  float moveSpeed = 200f;
        [SerializeField] private  float runSpeed = 250f;
        [SerializeField] private float rotationSpeed = 200.0f;

        [SerializeField] private Transform mainCamera;
        [SerializeField] private Animator anim;
        
        private bool isRunning = false;

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
            input.Sprint += OnSprint;
        }

        private void OnDisable ()
        {
            input.Move -= GetMovement;
            input.Sprint -= OnSprint;
        }

        private void FixedUpdate ()
        {
            UpdateMovement();
        }

        private void UpdateMovement () 
        {
            var adjustedDirection = Quaternion.AngleAxis(mainCamera.eulerAngles.y, Vector3.up) * movement;
            
                HandleRotation(adjustedDirection);
                
            if (adjustedDirection.magnitude > 0.1f)
            {
                HandleMovement(adjustedDirection);
            }
            else 
            {
                //Not change the rotation or movement, but need to apply rigidbody Y movement for gravity
                rb.linearVelocity = new Vector3(0.0f, rb.linearVelocity.y, 0.0f);
            }
            UpdateAnimation(adjustedDirection);
        }
        private void HandleMovement (Vector3 adjustedMovement)
        {
            float speed = isRunning ? runSpeed : moveSpeed;
            var velocity = adjustedMovement * (speed * Time.fixedDeltaTime);
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }

        private void HandleRotation(Vector3 adjustedMovement)
        {
            var targetRotation = Quaternion.AngleAxis(mainCamera.eulerAngles.y, Vector3.up) ;
            transform.rotation = targetRotation;
        }
        
        private void UpdateAnimation(Vector3 adjustedDirection)
        {
            bool isMoving = adjustedDirection.magnitude > 0.1f;
            anim.SetBool("IsWalking", isMoving);
            anim.SetBool("IsRunning", isMoving && isRunning);
        }
        
        private void GetMovement (Vector2 move)
        {
            movement.x = move.x;
            movement.z = move.y;
        }
        
        private void OnSprint(bool isSprinting)
        {
            isRunning = isSprinting;
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
