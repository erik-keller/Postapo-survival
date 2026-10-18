using UnityEngine;

namespace ProjectPostapo.InventorySystem
{
    public interface IAddItem
    {
        public bool AddItem(string itemId, int amount);
    }
}
