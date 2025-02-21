using UnityEngine;

namespace Utility {
    public class AutomaticRotation : MonoBehaviour, IUpdate
    {
        
        [SerializeField] private Vector3 axy;
        [SerializeField] private float speed;

        private int _idx = 0;

        public int idx => _idx;

        public  void OnUpdate (float deltaTime) {
            transform.Rotate(axy, deltaTime * speed, Space.Self);
        }
    }
}
