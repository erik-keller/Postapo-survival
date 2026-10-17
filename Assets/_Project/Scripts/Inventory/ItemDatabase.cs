using System.Collections.Generic;
using UnityEngine;

namespace ProjectPostapo.InventorySystem
{
    public class ItemDatabase : MonoBehaviour
    {
        private static ItemDatabase instance;
        public ItemDatabase Instance => instance;

        [SerializeField] private List<ItemSO> allItems = new();
        private Dictionary<string, ItemSO> itemLookupTable = new();

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            foreach(var item in allItems)
            {
                if (!itemLookupTable.ContainsKey(item.id))
                {
                    itemLookupTable.Add(item.id, item);
                }
                else
                {
                    Debug.LogWarning($"Duplicated ID of item in base: {item.id}");
                }
            }
        }

        public ItemSO GetItem(string itemId)
        {
            if (itemLookupTable.TryGetValue(itemId, out var item))
            {
                return item;
            }

            Debug.LogError($"No item with ID: {itemId} in database!");
            return null;
        }
    }
}
