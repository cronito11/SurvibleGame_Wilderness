using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DraggableItem : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image itemIcon;
    public Transform previousParent;
    public Transform parentAfterDrag;
    public InventoryItem item;
    private InventorySlot currentInventorySlot;
    [SerializeField] private int clonedItemQuantity; 
    public event System.Action OnItemDragged;

    public void OnBeginDrag(PointerEventData eventData)
    { 
        //Debug.Log("OnBeginDrag");
        parentAfterDrag = transform.parent;
        previousParent = transform.parent;

        //implement something other method beacuse it clears the item even if it's quantity is greater than 1
        currentInventorySlot = transform.parent.GetComponent<InventorySlot>();
        //currentInventorySlot.ClearSlot();

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        //currentInventorySlot.CreateCopyOnDrag(item, ref clonedItemQuantity);
        OnItemDragged?.Invoke();    
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        transform.position = Input.mousePosition;
        itemIcon.raycastTarget = false;
        //currentInventorySlot.itemUi = null;
    }
    public void OnEndDrag(PointerEventData eventData)
    {   
        currentInventorySlot.EmptySlot();
        transform.SetParent(parentAfterDrag);
        
        parentAfterDrag.GetComponent<InventorySlot>().SetItem(item);
        
        itemIcon.raycastTarget = true;
    }
    public void AssignToPreviousSlot()
    {
        this.previousParent.GetComponent<InventorySlot>().SetItem(item);
    }
}
