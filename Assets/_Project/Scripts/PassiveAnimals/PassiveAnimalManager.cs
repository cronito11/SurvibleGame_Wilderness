using System;
using System.Reflection;
using UnityEngine;

namespace Surviblewilderness
{
    public class PassiveAnimalManager : EntityManager<IObserver>
    {
        private const float GESTATION_TIME = 500f;
        [SerializeField] private PassiveAnimalStats stats;

        private PassiveAnimalSpawner PassiveAnimalSpawner;
        private Vector3 initialPosition;
        public PassiveAnimalStats Stats => stats;

        override protected void Awake ()
        {
            base.Awake();
            PassiveAnimalSpawner = GetComponentInParent<PassiveAnimalSpawner>();
            float radius = 2f;
            Vector2 circle = UnityEngine.Random.insideUnitCircle * radius;
            initialPosition = new Vector3(circle.x, 0, circle.y) + transform.position;
        }

        private void Start ()
        {
            NotifyObservers();
            agent.destination = initialPosition;
        }

        public void SetStats (PassiveAnimalStats stats)
        {
            this.stats = stats;
            NotifyObservers();
        }

        public void ResetStats (bool isMale, PassiveAnimalState state = PassiveAnimalState.None)
        {
            this.stats = new PassiveAnimalStats()
            {
                isMale= isMale,
                secundaryState = state,
                health = 100,
            };
            NotifyObservers();
        }

        public void Pregnanted ()
        {
            if (stats.isMale || stats.secundaryState != PassiveAnimalState.None)
                return;
            StartPregnantProcess ();
        }

        private void StartPregnantProcess ()
        {
            stats.secundaryState = PassiveAnimalState.Pregnant;
            stats.timer = GESTATION_TIME;
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
                        Grow();
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
            PassiveAnimalSpawner.InstantiateChild();
        }
    }
}
