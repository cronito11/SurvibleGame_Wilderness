using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace Surviblewilderness
{
    public class PlayerInteractableController : MonoBehaviour
    {
        [SerializeField] private InputReader input;
        [SerializeField] private float viewRadius = 2f;
        [SerializeField] private float viewAngle = 110f;
        [SerializeField] private LayerMask targetMask; // Layer of the objects to detect

        private SortedList<IInteractable, float> orderedList = new SortedList<IInteractable, float>();
        private Collider[] targetsInViewRadius = new Collider[10];
        private int targetsFound;



        private void Start ()
        {
            input.EnablePlayerActions();
        }

        private void OnEnable ()
        {
            input.Interact += DetectTargets;
        }

        private void OnDisable ()
        {
            input.Interact -= DetectTargets;
        }

        private void DetectTargets ()
        {
            targetsFound = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, targetsInViewRadius, targetMask);
            Debug.Log($"Call {targetsFound}");

            if (targetsFound == 0)
                return;
            orderedList.Clear();

            for (int idx= 0; idx< targetsFound; idx++)
            {
                Collider target = targetsInViewRadius[idx];
                Transform targetTransform = target.transform;
                Vector3 dirToTarget = (targetTransform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
                    orderedList.Add(target.GetComponent<IInteractable>(), distanceToTarget);
                    Debug.Log($"target.GetComponent<IInteractable>() {target.GetComponent<IInteractable>()}", target);
                }
            }
            if (orderedList.Count == 0)
                return;
            orderedList.Keys[0].Interact();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos ()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, viewRadius);

            Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward * viewRadius;
            Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward * viewRadius;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
        }
#endif
    }
}
