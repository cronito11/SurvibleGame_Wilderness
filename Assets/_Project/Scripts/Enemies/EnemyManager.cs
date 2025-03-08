using UnityEngine;
using UnityEngine.AI;

namespace Surviblewilderness
{
    public enum EnemyState 
    {
        None = 0,
        Evaluating = 4,
        Chasing = 1,
        Hunting  = 2,
        Attacking = 3,
    }

    public class EnemyManager : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Transform target;
        private Vector3 destination;
        private EnemyState state;

        private void Awake ()
        {
            agent = GetComponentInParent<NavMeshAgent>();
        }

        public void FindTarget(Transform newTarget)
        {
            target = newTarget;
            state = EnemyState.Evaluating;
            Debug.Log(target.gameObject, target);

        }

        private void Update ()
        {
            UpdateTarget();
        }

        private void UpdateTarget ()
        {
            switch (state)
            {
                case EnemyState.Evaluating:

                    break;
            }
            agent.destination = destination;
            if (!target)
                return;
            destination = target.position;
        }
    }
}
