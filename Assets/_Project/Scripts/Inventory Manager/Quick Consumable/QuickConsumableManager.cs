using System;
using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
    public class QuickConsumableManager : MonoBehaviour
    {
        //create a queue
        public static event Action OnConsumableChanged; // trigger this event on item consume this will updat the consumable item in the UI


        Queue<DraggableItem> consumables = new Queue<DraggableItem>();
        InventoryItem currentItem;
        [SerializeField] GameItemSO item;
        [SerializeField] PlayerInventory playerInventory;
        //public void TestConsume()
        //{
        //    currentItem = new InventoryItem(item, 1);
        //    playerInventory.AddItem(item, 1);
        //    currentItem.UseItem();
        //}
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.R))
        //    {
        //        TestConsume();
        //    }
        //}
        public void ConsumeItem() 
        { 

            DraggableItem consumableItem = consumables.Peek();
            
            if(consumableItem.item.quantity <= 1)
                consumables.Dequeue();

            if (consumableItem == null)
            {
                Debug.Log("No consumable item in the queue");
                return;
            }
            consumableItem.item.UseItem();
            OnConsumableChanged?.Invoke();
        }
    }
}
