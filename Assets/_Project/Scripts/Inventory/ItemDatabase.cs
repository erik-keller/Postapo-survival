using ProjectPostapo.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPostapo.InventorySystem
{
    public class ItemDatabase : MonoBehaviour
    {
        public static ItemDatabase Instance;

        [SerializeField] private List<ItemSO> allItems = new();
        private Dictionary<string, ItemSO> itemLookupTable = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            foreach(ItemSO item in allItems)
            {
                IGetComponent comp = item;
                string id = comp.GetComponentValue(ItemComponent.Id);

                if (!itemLookupTable.ContainsKey(id))
                {
                    itemLookupTable.Add(id, item);
                }
                else
                {
                    Debug.LogWarning($"Duplicated ID of item in base: {id}");
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
