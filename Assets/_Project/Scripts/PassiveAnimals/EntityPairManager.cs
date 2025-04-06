using UnityEngine;

namespace Surviblewilderness
{
    public class EntityPairManager : MonoBehaviour
    {
        private const float TIME_TO_PREGNANT = 240f;
        private PassiveAnimalManager manager;
        private PassiveAnimalManager target;
        
        private float timer;

        private void Awake ()
        {
            manager = GetComponentInParent<PassiveAnimalManager>();
        }

        private void Update ()
        {

            if (timer == 0)
                return;

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer =0;
                target.Pregnanted();
                target = null;
            }
        }
        private void OnTriggerEnter (Collider other)
        {
            if (!manager.Stats.isMale || target != null|| !other.gameObject.CompareTag("Prey"))
                return;

            target = other.GetComponentInParent<PassiveAnimalManager>();
            if (target.Stats.isMale)
            {
                target = null;
            }else
                timer = TIME_TO_PREGNANT;
        }

        private void OnTriggerExit (Collider other)
        {
            if (!manager.Stats.isMale || target == null || !other.gameObject.CompareTag("Prey"))
                return;

            PassiveAnimalManager passiveAnimalManager = other.GetComponentInParent<PassiveAnimalManager>();
            if (passiveAnimalManager != target)
                return;
            target = null;
            timer = 0;
        }
    }
}
