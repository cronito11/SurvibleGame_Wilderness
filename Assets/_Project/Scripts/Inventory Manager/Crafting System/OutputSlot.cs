using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Surviblewilderness
{
    public class OutputSlot : Slot, IDropHandler, IPointerClickHandler
    {
        PlayerInventory inventory;

        private void Start()
        {
            inventory = GameObject.FindAnyObjectByType<PlayerInventory>();
            //inventory.OnInventoryChanged += UpdateSlot;
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (!isEmpty || itemUi != null)
                return;
            GameObject droppedItem = eventData.pointerDrag;
            DraggableItem draggableItem;

            //check if the pointer drag is null or not 
            if (!droppedItem.TryGetComponent<DraggableItem>(out draggableItem))
            {
                //item will be returned to the previous parent/slot and get assigned to that previous slot  
                Debug.Log("Pointer drag is null");

                return;
            }

            //assign the item to this slot  
            draggableItem.parentAfterDrag = transform;
            //itemUi = droppedItem;

            //currentInventoryItem.UseItem();

            Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        }

        private float lastClickTime = 0f;
        private const float doubleClickThreshold = 0.3f; // seconds

        public override void EmptySlot()
        {
            //currentInventoryItem.isEqupped = false;
            //OnRemoveMaterialFromCraftingPanel?.Invoke(currentInventoryItem.gameItem);
            base.EmptySlot();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (isEmpty)
            {
                Debug.Log("Slot is empty");
                return;
            }

            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickThreshold)
            {
                // Double click detected
                Debug.Log($"Double Click! on item {currentInventoryItem.gameItem.name}");

                //restore item to inventory 
                inventory.AddItem(currentInventoryItem.gameItem, currentInventoryItem.quantity);

                //OnUnequipLower?.Invoke(currentInventoryItem.gameItem);

                //OnDoubleClick();
                this.EmptySlot();
            }

            lastClickTime = Time.time;
        }
    }
}
