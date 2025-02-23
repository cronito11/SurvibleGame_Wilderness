using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DraggableItem : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image itemIcon;
    public Transform prviousParent;
    public Transform parentAfterDrag;
    public InventoryItem item;
    private InventorySlot currentInventorySlot;

    public void OnBeginDrag(PointerEventData eventData)
    { 
        //Debug.Log("OnBeginDrag");
        parentAfterDrag = transform.parent;
        prviousParent = transform.parent;   
        currentInventorySlot = transform.parent.GetComponent<InventorySlot>();
        //currentInventorySlot.ClearSlot();

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
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
        //Debug.Log("OnEndDrag");
        transform.SetParent(parentAfterDrag);
        if (transform.parent != prviousParent)
        {
            currentInventorySlot.ClearSlot();
        }
        itemIcon.raycastTarget = true;
    }
}
