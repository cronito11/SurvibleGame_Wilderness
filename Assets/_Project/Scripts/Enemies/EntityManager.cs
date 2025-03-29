using UnityEngine;
using UnityEngine.AI;

namespace Surviblewilderness
{
    public abstract class EntityManager : MonoBehaviour
    {
        protected NavMeshAgent agent;
        protected Vector3 destination;
        protected Transform target;
        protected EntityState state;

        private void Awake ()
        {
            agent = GetComponentInParent<NavMeshAgent>();
        }

        abstract public void FindTarget (Transform newTarget);

        abstract public void LostTarget (Transform target);

        private void Update ()
        {
            UpdateTarget();
        }

        abstract protected void UpdateTarget ();
    }
}
