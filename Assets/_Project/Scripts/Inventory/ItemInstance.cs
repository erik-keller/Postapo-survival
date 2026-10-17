using UnityEngine;

namespace ProjectPostapo.InventorySystem
{
    public class ItemInstance
    {
        public string itemId;
        //public int currentDurability;

        public ItemInstance(string itemId)//, int durability)
        {
            this.itemId = itemId;
            //this.currentDurability = durability;
        }
    }
}
