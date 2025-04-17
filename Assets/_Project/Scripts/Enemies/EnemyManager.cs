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
    public class EnemyManager : EntityManager<IObserver>
    {
        private PassiveAnimalManager passiveAnimal;
        private PassiveAnimalSpawner passiveAnimalSpawner;

        protected override void Awake ()
        {
            base.Awake();
            passiveAnimalSpawner = FindAnyObjectByType<PassiveAnimalSpawner>();
            passiveAnimal = passiveAnimalSpawner.GetRandomPassiveAnimal();
        }

        public override void FindTarget (Transform newTarget)
        {
                target = newTarget;
                state = EntityState.Evaluating;
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

            if (target)
            {
                destination = target.position;
            } else if (passiveAnimal)
            {
                destination = passiveAnimal.transform.position;
            } else 
            {
                if (passiveAnimalSpawner.activeElementCount == 0)
                    return;
                passiveAnimal = passiveAnimalSpawner.GetRandomPassiveAnimal();
            }

            agent.destination = destination;            
        }
    }
}
