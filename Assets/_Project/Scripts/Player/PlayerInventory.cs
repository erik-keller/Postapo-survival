using UnityEngine;

namespace ProjectPostapo.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        const ushort InventorySize = 2;
        private InventorySlot[] items = new InventorySlot[InventorySize];

        void Start()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new InventorySlot(null, 0);
            }
        }

        public bool AddItem(ItemScriptableObject item, ushort quantity)
        {
            for (ushort i = 0; i < items.Length; i++)
            {
                if (!items[i].IsEmpty() && items[i].Item == item)
                {
                    items[i].ChangeQuantity(quantity);
                    PlayerUI.OnInventorySlotChanged?.Invoke(items);

                    Debug.Log($"Added {quantity} of {item.name} to existing stack in inventory.");
                    return true;
                }
                else if (items[i].IsEmpty())
                {
                    items[i] = new InventorySlot(item, quantity);
                    PlayerUI.OnInventorySlotChanged?.Invoke(items);

                    Debug.Log($"Added {item.name} to inventory.");
                    return true;
                }
            }
            Debug.LogWarning($"Could not add {item.name} to inventory. Inventory is full.");
            return false;
        }

        public void RemoveItem(int index)
        {
            if (index < 0 || index >= items.Length)
            {
                Debug.LogWarning($"Invalid inventory index: {index}. Cannot remove item.");
                return;
            }

            InventorySlot itemSlot = items[index];

            if (itemSlot.IsEmpty())
            {
                Debug.LogWarning($"Inventory slot at index {index} is already empty. No item to remove.");
                return;
            }

            if (itemSlot.Quantity <= 0)
            {
                Debug.LogWarning($"Inventory slot at index {index} has non-positive quantity. Clearing slot.");
                return;
            }

            Debug.Log($"Item slot at index {index} has quantity {itemSlot.Quantity}. Removing one item.");

            itemSlot.ChangeQuantity(-1);
            PlayerUI.OnInventorySlotChanged?.Invoke(items);
        }
    }
}