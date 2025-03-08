using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class EnemyAreaDetector : MonoBehaviour
    {
        private EnemyManager manager;

        private void Awake ()
        {
            manager = GetComponentInParent<EnemyManager>();
        }

        private void OnTriggerEnter (Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                manager.FindTarget(other.transform);
            }
        }
    }
}
