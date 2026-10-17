using UnityEngine;

namespace ProjectPostapo.InventorySystem
{
    [System.Serializable]
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

            int newQuantity = Quantity + amount;
            newQuantity = Mathf.Clamp(newQuantity, 0, 100); // Warning

            Quantity = newQuantity;
            
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
