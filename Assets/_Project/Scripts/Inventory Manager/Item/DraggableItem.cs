using Surviblewilderness;
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
    private Slot previousSlotRef;
    [SerializeField] private int clonedItemQuantity;
    
    public event System.Action OnItemDragged;

    public void OnBeginDrag(PointerEventData eventData)
    { 
        //Debug.Log("OnBeginDrag");
        parentAfterDrag = transform.parent;
        previousParent = transform.parent;

        //implement something other method beacuse it clears the item even if it's quantity is greater than 1
        previousSlotRef = transform.parent.GetComponent<Slot>();
        //previousSlotRef.ClearSlot();

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        //previousSlotRef.CreateCopyOnDrag(item, ref clonedItemQuantity);
        OnItemDragged?.Invoke();    
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        transform.position = Input.mousePosition;
        itemIcon.raycastTarget = false;
        //previousSlotRef.itemUi = null;
    }
    public void OnEndDrag(PointerEventData eventData)
    {   
        previousSlotRef.EmptySlot();
        transform.SetParent(parentAfterDrag);
        
        parentAfterDrag.GetComponent<Slot>().SetItem(item);

        //NOTE CREATE A CLOTH SLOT AND ADD THE CONDITION FOR CLOTHING AS WELL 
        //if it is a weapon slit then equipt the weapon 
        if(parentAfterDrag.GetComponent<Slot>() as WeaponSlot)
        {
            item.UseItem();
        }
        if (parentAfterDrag.GetComponent<Slot>() as OutfitSlot)
        {
            item.UseItem();
        }

        itemIcon.raycastTarget = true;
    }
    public void AssignToPreviousSlot()
    {
        previousParent.GetComponent<Slot>().SetItem(item);
    }
}
