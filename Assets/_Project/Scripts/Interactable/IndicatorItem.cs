using UnityEngine;

namespace Surviblewilderness
{
    public class IndicatorItem : MonoBehaviour
    {
        [SerializeField] private GameObject indicator;

        private void OnTriggerEnter (Collider other)
        {
            indicator.SetActive (true);
        }

        private void OnTriggerExit (Collider other)
        {
            indicator.SetActive (false);
        }
    }
}
