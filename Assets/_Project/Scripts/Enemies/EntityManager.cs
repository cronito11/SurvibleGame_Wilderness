using UnityEngine;
using UnityEngine.AI;

namespace Surviblewilderness
{
    public abstract class EntityManager<T> : Subject<T>
    {
        protected NavMeshAgent agent;
        protected Vector3 destination;
        protected Transform target;
        protected EntityState state;
        public bool isTargetInRange => target != null;

        virtual protected void Awake ()
        {
            agent = gameObject.GetComponentInParent<NavMeshAgent>();
        }

        abstract public void FindTarget (Transform newTarget);

        abstract public void LostTarget (Transform target);

        private void Update ()
        {
            UpdateTarget();
        }

        abstract protected void UpdateTarget ();

        public override void NotifyObservers ()
        {
            for (int idx = 0; idx < observers.Count; idx++)
            {
                ((IObserver)observers [idx]).OnNotify();
            }
        }
    }
}
