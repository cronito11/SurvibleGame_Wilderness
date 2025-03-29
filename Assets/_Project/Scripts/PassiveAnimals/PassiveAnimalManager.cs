using UnityEngine;

namespace Surviblewilderness
{
    public class PassiveAnimalManager : EntityManager
    {
        private Vector3 initialPosition;
        [SerializeField] private PassiveAnimalStats stats;

        public void SetStats (PassiveAnimalStats stats)
        {
            this.stats = stats;
        }

        public void Pregnanted ()
        {
            if (stats.isMale)
                return;
            StartPregnantProcess ();
        }

        private void StartPregnantProcess ()
        {
            stats.secundaryState = PassiveAnimalState.Pregnant;
            destination = transform.position;
            agent.destination = destination;
        }


        public override void FindTarget (Transform newTarget)
        {
            target = newTarget;
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
            switch (stats.secundaryState)
            {
                case PassiveAnimalState.Pregnant:
                    stats.timer -= Time.deltaTime;
                    if (stats.timer < 0)
                    {
                        GiveBirth();
                    }
                break;
                case PassiveAnimalState.Growing:
                    stats.timer -= Time.deltaTime;
                    if (stats.timer < 0)
                    {
                        GiveBirth();
                    }
                break;
                default:
                break;

            }
        }

        public void Grow ()
        {
            stats.secundaryState = PassiveAnimalState.None;
            state = EntityState.None;

        }

        private void GiveBirth ()
        {
            stats.secundaryState = PassiveAnimalState.None;
            state = EntityState.None;
        }
    }
}
