using System.Collections.Generic;
using UnityEngine.Events;

namespace ProjectPostapo.InventorySystem
{
    public class Inventory : IAddItem
    {
        public uint Capacity = 2;
        private List<InventorySlot> slots = new();

        public UnityEvent<int, string, int> OnInventorySlotChanged = new();

        public Inventory(uint capacity)
        {
            Capacity = capacity;

            for (int i = 0; i < Capacity; i++)
            {
                slots.Add(new InventorySlot());
            }
        }

        public bool AddItem(string itemId, int amount)
        {
            int index = FindSlotWithItem(itemId);
            if (index == -1)
            {
                index = FindEmptySlot();
                if (index == -1)
                {
                    return false;
                }

                slots[index].Item = new ItemInstance(itemId);
                slots[index].Quantity = amount;
            }
            else
            {
                slots[index].ChangeQuantity(amount);
            }

            //UIEvents.t_OnInventorySlotChanged?.Invoke(index, slots[index].Item.itemId, slots[index].Quantity);
            OnInventorySlotChanged?.Invoke(index, slots[index].Item.itemId, slots[index].Quantity);
            return true;
        }

        public bool RemoveItem()
        {
            return true;
        }

        public string GetSlotItemId(int index)
        {
            if (slots[index] == null)
            {
                return null;
            }
            if (slots[index].Item == null)
            {
                return null;
            }
            if (slots[index].Item.itemId == null)
            {
                return null;
            }
            return slots[index].Item.itemId;
        }
        public int GetSlotQuantity(int index)
        {
            return slots[index].Quantity;
        }

        private int FindSlotWithItem(string itemId)
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (slots[i].IsEmpty)
                {
                    continue;
                }
                if (slots[i].Item.itemId == itemId)
                {
                    return i;
                }
            }

            return -1;
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (slots[i].IsEmpty)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
