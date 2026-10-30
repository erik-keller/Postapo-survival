using ProjectPostapo.InventorySystem;
using ProjectPostapo.ScriptableObjects;
using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public class Pickable : Interactable
    {
        [SerializeField] private GameObject objectToDestroy;

        [Header("Add Item")]
        [SerializeField] protected string itemId;
        [SerializeField] protected ItemDatabase itemDatabase;
        [SerializeField] private ushort quantity = 1;

        public override void Interact(GameObject interactor, float interactorRange = 2f)
        {
            base.Interact(interactor);

            if (itemId == null || itemId == string.Empty || itemId == "")
            {
                Debug.LogWarning($"itemId is {itemId}(null or empty)");
                return;
            }

            if (!interactor.TryGetComponent(out IAddItem inventory))
            {
                Debug.LogError("Not found IAddItem at interactor.");
                return;
            }

            if (!inventory.AddItem(itemId, quantity))
            {
                Debug.LogWarning("Cannot add item to inventory");
                return;
            }

            if (objectToDestroy == null)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(objectToDestroy);
            }
        }
    }
}