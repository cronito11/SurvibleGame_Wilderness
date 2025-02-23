using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image itemIcon;
    public TMP_Text quantityText;
    public GameObject itemUi;
    private InventoryItem currentItem;
    //public bool IsEmpty { get; set => value = currentItem == null; }

[SerializeField] private GameObject itemUiPrefab;   
    public void SetItem(InventoryItem item)
    {
        if(item.gameItem == null)
        {
            Debug.Log("Item is null");
            return;
        }
        currentItem = item;
        itemUi.SetActive(true);
        itemUi.GetComponentInChildren<Image>().sprite = item.gameItem.icon;
        itemUi.GetComponentInChildren<TMP_Text>().text = item.gameItem.maxStackSize > 1 ? item.quantity.ToString() : "";
        gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        currentItem = null;
        itemUi.GetComponentInChildren<Image>().sprite = null;
        itemUi.GetComponentInChildren<TMP_Text>().text = "";    
        itemUi.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        DraggableItem draggableItem = droppedItem.GetComponent<DraggableItem>();
        draggableItem.parentAfterDrag = transform;
    }
}
