using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class EntityAreaDetector : MonoBehaviour
    {
        private EntityManager manager;

        private void Awake ()
        {
            manager = GetComponentInParent<EntityManager>();
        }

        private void OnTriggerEnter (Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                manager.FindTarget(other.transform);
            }
        }

        private void OnTriggerExit (Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                manager.FindTarget(other.transform);
            }
        }
    }
}
