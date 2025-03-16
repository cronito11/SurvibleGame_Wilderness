using UnityEngine;

namespace Surviblewilderness
{
    public class CollectableItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private Drops dropType;
        [SerializeField] private int amount = 1;
        [SerializeField] private GameItemSO item;

        public void Interact ()
        {
            InteractableObject.OnItemPickedUp?.Invoke(item, amount);
            //Collect item in inventary
            //Debug.Log("Interactioin call "+gameObject.name, gameObject);
            Destroy(gameObject);
        }
    }
}
