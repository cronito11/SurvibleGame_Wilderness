using Unity.Cinemachine;
using UnityEngine;

namespace Platformer397
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [Space(10)]
        [Header("Cameras")]
        //References to the CinemachineVirtualCamera
        [SerializeField] private CinemachineCamera freeLookCamera;

        private void Awake ()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (player != null)
                return;
            
            player = GameObject.FindWithTag("Player").transform;

        }

        private void OnEnable ()
        {
            freeLookCamera.Target.TrackingTarget = player;
        }
    }
}
