using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Surviblewilderness
{
    public abstract class Slot : MonoBehaviour, ISlot
    {
        //public static event Action OnItemDropOnSlot;  

        //holds the reference of curret assigned item of type game object on the slot 
        public GameObject itemUi;

        //holds the reference of the current inventory item assigned to in the slot
        [field: SerializeField] public InventoryItem currentInventoryItem { get; protected set; }
        //[SerializeField] public bool IsEmpty => currentInventoryItem == null;
        [SerializeField] protected GameObject itemUiPrefab;
        [field: SerializeField] public bool isEmpty => currentInventoryItem == null && itemUi == null;

        //setting the item to the slot
        public virtual void SetItem(InventoryItem item)
        {
            if (item.gameItem == null)
            {
                Debug.Log("Item is null");
                return;
            }

            //if there is no item assigned to the slot and the slot is empty then instantiate the item prefab
            if (itemUi == null && isEmpty)
            {
                itemUi = GameObject.Instantiate(itemUiPrefab, transform);
                itemUi.SetActive(true);
                itemUi.GetComponentInChildren<Image>().sprite = item.gameItem.icon;
                itemUi.GetComponentInChildren<DraggableItem>().item = item;
                gameObject.SetActive(true);
            }

            //if the item is already assigned to the slot then just update the quantity
            //just updating the quantity
            currentInventoryItem = item;
            //currentInventoryItem = item;
            currentInventoryItem.isAssignedToSlot = true;
            itemUi.GetComponentInChildren<TMP_Text>().text = item.quantity.ToString();
            
            //if the item there is no item of this type in inventory 
            if(item.quantity <= 0)
            {
                Debug.Log("Item quantity is 0");    
                EmptySlot();
            }
        }

        //remove the item from the slot destroy the draggable game object
        public virtual void EmptySlot()
        {
            if (itemUi != null)
            {
                GameObject gameObject = itemUi;
                itemUi = null;
                Destroy(gameObject);
            }
            currentInventoryItem = null;
        }

        //only remove reference of the item from the slot
        public virtual void ClearSlot()
        {
            currentInventoryItem = null;
            if (itemUi == null)
                return;

            itemUi = null;
        }


        public virtual void UpdateQuantityText()
        {
            itemUi.GetComponentInChildren<TMP_Text>().text = currentInventoryItem.quantity.ToString();
        }
    }
}
