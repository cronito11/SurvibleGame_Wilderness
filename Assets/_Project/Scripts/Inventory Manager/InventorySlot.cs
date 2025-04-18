using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using Surviblewilderness;
public class InventorySlot : Slot, IDropHandler,IPointerClickHandler
{
    private PlayerInventory inventory;

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
            //draggableItem.AssignToPreviousSlot();
            return;
        }

        if (draggableItem.item.isEqupped)
        {
            if (draggableItem.item.gameItem.itemType == ItemType.Weapon
                || draggableItem.item.gameItem.itemType == ItemType.Clothing)
            {
                draggableItem.item.isEqupped = false;
                inventory.AddItem(draggableItem.item.gameItem, draggableItem.item.quantity);
            }
        }
       

        //assign the item to this slot  
        draggableItem.parentAfterDrag = transform;
        //itemUi = droppedItem;
        
       // SetItem(draggableItem.item);

        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);   
    }

    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f; // seconds
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(isEmpty)
        {
            Debug.Log("Slot is empty");
            return;
        }

        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= doubleClickThreshold)
        {
            // Double click detected
            Debug.Log($"Double Click! on item {currentInventoryItem.gameItem.name}");
            if (currentInventoryItem.gameItem.itemType != ItemType.Weapon
                && currentInventoryItem.gameItem.itemType != ItemType.Clothing)
            {
                //double click use item only works for food and material    
                currentInventoryItem.UseItem();
            }
        }

        lastClickTime = Time.time;
    }
}
