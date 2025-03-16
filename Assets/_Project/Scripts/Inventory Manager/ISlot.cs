using UnityEngine;

namespace Surviblewilderness
{
    public interface ISlot 
    {
        public void SetItem(InventoryItem item);
        public void ClearSlot();    

    }
}
