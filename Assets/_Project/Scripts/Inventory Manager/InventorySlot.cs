using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    //public static event Action OnItemDropOnSlot;  // 🔥 Event for inventory updates

    public GameObject itemUi;

    public InventoryItem currentInventoryItem { get; private set; }
    [SerializeField] public bool IsEmpty => currentInventoryItem == null;    
    [SerializeField] private GameObject itemUiPrefab;   
    public bool isEmpty { get { return currentInventoryItem == null; } }   
    public void SetItem(InventoryItem item)
    {
        if(item.gameItem == null)
        {
            Debug.Log("Item is null");
            return;
        }

        
        
        if (itemUi == null && isEmpty)
        {
            itemUi = GameObject.Instantiate(itemUiPrefab, transform);
            currentInventoryItem = item;
            currentInventoryItem.isAssignedToSlot = true;
        }

        //just updating the quantity
        itemUi.SetActive(true);
        itemUi.GetComponentInChildren<Image>().sprite = item.gameItem.icon;
        itemUi.GetComponentInChildren<TMP_Text>().text = item.quantity.ToString();
        itemUi.GetComponentInChildren<DraggableItem>().item = item;
        gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        currentInventoryItem = null;
        if (itemUi == null)
            return;
        
        itemUi = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        DraggableItem draggableItem = droppedItem.GetComponent<DraggableItem>();
        draggableItem.parentAfterDrag = transform;
        itemUi = droppedItem;
        
        SetItem(draggableItem.item);
    }
}
