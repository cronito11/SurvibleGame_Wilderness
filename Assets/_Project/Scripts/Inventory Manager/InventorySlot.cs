using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using Surviblewilderness;
public class InventorySlot : Slot, IDropHandler,IPointerClickHandler
{
    //private void OnEnable()
    //{
    //    DraggableItem.OnItemDragged += CreateCopyOnDrag;
    //}
    //private void OnDisable()
    //{
    //    DraggableItem.OnItemDragged -= CreateCopyOnDrag;
    //}
   
    //public void CreateCopyOnDrag(InventoryItem item, ref int clonedItemQuantity)
    //{
    //    if (item.gameItem == null)
    //    {
    //        Debug.Log("Inventory Item is null");
    //        return;
    //    }



    //    //if there is no item assigned to the slot and the slot is empty then instantiate the item prefab
    //    if (itemUi == null && isEmpty)
    //    {
    //        Debug.Log("Creating copy on drag bu the item ui is null of the slot is empty");
    //    }

    //    //if there is just one item no need to create a copy 
    //    if (item.quantity <= 1) { return; }

    //    itemUi = GameObject.Instantiate(itemUiPrefab, transform);
    //    itemUi.SetActive(true);
    //    itemUi.GetComponentInChildren<Image>().sprite = item.gameItem.icon;
    //    itemUi.GetComponentInChildren<DraggableItem>().item = item;
    //    gameObject.SetActive(true);

        
    //    currentInventoryItem = item;
    //    currentInventoryItem.isAssignedToSlot = true;
    //    item.quantity -= 1; 
    //    itemUi.GetComponentInChildren<TMP_Text>().text = (item.quantity).ToString();
    //}

    //public void MeargeWithCreatedCopy(GameObject item)
    //{
    //    if (itemUi == null)
    //    {
    //        Debug.Log("Item ui is null");
    //        return;
    //    }
    //    currentInventoryItem.quantity += 1;
    //    itemUi.GetComponentInChildren<TMP_Text>().text = currentInventoryItem.quantity.ToString();
    //    Destroy(item);
    //}
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
            currentInventoryItem.UseItem();
            //OnDoubleClick();
        }

        lastClickTime = Time.time;
    }
}
