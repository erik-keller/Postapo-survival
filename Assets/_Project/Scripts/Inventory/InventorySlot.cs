using UnityEngine;

namespace ProjectPostapo.InventorySystem
{
    public class InventorySlot
    {
        public ItemInstance Item;
        public int Quantity;

        public bool IsEmpty => Item == null || Quantity <= 0;

        public void ChangeQuantity(int amount)
        {
            if (amount == 0)
            {
                Debug.LogWarning("Attempted to change quantity by 0. No action taken.");
                return;
            }

            Quantity = Mathf.Clamp(Quantity + amount, 0, 100); // Warning
            
            if (Quantity == 0)
            {
                Clear();
            }
        }

        public void Clear()
        {
            Item = null;
            Quantity = 0;
        }
    }
}
