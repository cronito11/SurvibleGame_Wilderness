using UnityEngine;

namespace Surviblewilderness
{
    public class CollectableItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private Drops dropType;
        [SerializeField] private int amount;

        public void Interact ()
        {
            //Collect item in inventary
            Debug.Log("Interactioin call "+gameObject.name, gameObject);
        }
    }
}
