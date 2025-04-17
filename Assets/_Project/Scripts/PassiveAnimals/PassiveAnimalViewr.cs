using UnityEngine;

namespace Surviblewilderness
{
    public class PassiveAnimalViewr : MonoBehaviour, IObserver
    {
        [SerializeField] private GameObject maleGraphics;
        [SerializeField] private GameObject femaleGraphics;

        private PassiveAnimalManager passiveAnimalManager;

        private void Awake ()
        {
            passiveAnimalManager = GetComponentInParent<PassiveAnimalManager>();
            passiveAnimalManager.AddObserver(this);
        }


        private void OnDestroy ()
        {
            passiveAnimalManager.RemoveObserver(this);
        }

        public void OnNotify ()
        {
            bool isMale = passiveAnimalManager.Stats.isMale;
            maleGraphics.SetActive(isMale);
            femaleGraphics.SetActive(!isMale);
        }
    }
}
