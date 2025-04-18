using System;
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
        [SerializeField] private Animator anim;

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
                UpdateWalkingAnimation(false); // Stop walking when target is lost
        }

        protected override void UpdateTarget ()
        {
            bool shouldWalk = false;
            
            switch (state)
            {
                case EntityState.Evaluating:
                    // evaluation logic
                    
                    shouldWalk = true;
                    break;
                case EntityState.Chasing:
                case EntityState.Hunting:
                    shouldWalk = true;
                    break;
                case EntityState.Attacking:
                    shouldWalk = false; // Stop walking when attacking
                    break;
                default:
                    shouldWalk = false;
                    break;
            }

            if (target)
            {
                destination = target.position;
                shouldWalk = true;
            } else if (passiveAnimal)
            {
                destination = passiveAnimal.transform.position;
                shouldWalk = true;
            } else 
            {
                if (passiveAnimalSpawner.activeElementCount == 0)
                    return;
                passiveAnimal = passiveAnimalSpawner.GetRandomPassiveAnimal();
            }
            
            // Update walking animation based on movement
            if (agent.velocity.magnitude > 0.1f && shouldWalk)
            {
                UpdateWalkingAnimation(true);
            }
            else
            {
                UpdateWalkingAnimation(false);
            }

            agent.destination = destination;  
        }
        private void UpdateWalkingAnimation(bool isWalking)
        {
            if (anim != null)
            {
                anim.SetBool("WalkForward", isWalking);
            }
        }
    }
}
