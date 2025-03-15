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
            itemUi.SetActive(true);
            itemUi.GetComponentInChildren<Image>().sprite = item.gameItem.icon;
            itemUi.GetComponentInChildren<DraggableItem>().item = item;
            gameObject.SetActive(true);
        }

        currentInventoryItem = item;
        currentInventoryItem.isAssignedToSlot = true;
        //just updating the quantity
        itemUi.GetComponentInChildren<TMP_Text>().text = item.quantity.ToString();
    }

    public void EmptySlot()
    {
        if (itemUi != null)
        {
            Destroy(itemUi);
        }
        currentInventoryItem = null;
    }

    public void ClearSlot()
    {
        currentInventoryItem = null;
        if (itemUi == null)
            return;
        
        itemUi = null;
    }

    public void UpdateQuantityText()
    {
        itemUi.GetComponentInChildren<TMP_Text>().text = currentInventoryItem.quantity.ToString();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (!IsEmpty)
            return;
        GameObject droppedItem = eventData.pointerDrag;
        DraggableItem draggableItem;
        if (!droppedItem.TryGetComponent<DraggableItem>(out draggableItem))
        {
            Debug.Log("Pointer drag is null");
            return;
        }
        draggableItem.parentAfterDrag = transform;
        itemUi = droppedItem;
        
        SetItem(draggableItem.item);
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);   
    }
}
