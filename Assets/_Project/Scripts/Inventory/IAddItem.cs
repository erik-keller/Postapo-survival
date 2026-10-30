using UnityEngine;

namespace ProjectPostapo.InventorySystem
{
    public interface IAddItem
    {
        bool AddItem(string itemId, int amount);
    }
}
