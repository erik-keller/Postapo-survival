using UnityEngine;

namespace ProjectPostapo.Player
{
    public class InventorySlot
    {
        public ItemScriptableObject Item { get; private set; }
        public ushort Quantity { get; private set; }

        public InventorySlot(ItemScriptableObject item, ushort quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public void ChangeQuantity(int amount)
        {
            if (amount == 0)
            {
                Debug.LogWarning("Attempted to change quantity by 0. No action taken.");
                return;
            }

            int newQuantity = Quantity + amount;
            newQuantity = Mathf.Clamp(newQuantity, 0, ushort.MaxValue);

            Quantity = (ushort)newQuantity;
        }

        public bool IsEmpty()
        {
            return Item == null || Quantity <= 0;
        }

        public void Clear()
        {
            Item = null;
            Quantity = 0;
        }
    }
}