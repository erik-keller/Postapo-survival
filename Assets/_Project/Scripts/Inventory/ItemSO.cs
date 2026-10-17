using UnityEngine;

namespace ProjectPostapo.InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class ItemSO : ScriptableObject
    {
        public string id;
        public string itemName;
        //public float weight;
        //public int maxDurability;
        public Texture2D icon;
        //public string description;
        //public bool isStackable;
    }
}