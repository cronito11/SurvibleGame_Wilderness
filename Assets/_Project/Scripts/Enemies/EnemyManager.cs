using UnityEngine;

namespace Surviblewilderness
{
    public enum EntityState 
    {
        None = 0,
        Evaluating = 4,
        Chasing = 1,
        Hunting  = 2,
        Attacking = 3,
    }
    public class EnemyManager : EntityManager
    {
        public override void FindTarget (Transform newTarget)
        {
                target = newTarget;
                state = EntityState.Evaluating;
                Debug.Log(target.gameObject, target);

        }

        public override void LostTarget (Transform target)
        {
                if (this.target == null || this.target != target)
                    return;

                this.target = null;
                state = EntityState.None;

        }

        protected override void UpdateTarget ()
        {
            switch (state)
            {
                case EntityState.Evaluating:

                break;
            }
            agent.destination = destination;
            if (!target)
                return;
            destination = target.position;
        }
    }
}
