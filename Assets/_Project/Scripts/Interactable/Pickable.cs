using ProjectPostapo.InventorySystem;
using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public class Pickable : Interactable
    {
        [SerializeField] private GameObject objectToDestroy; // Object that will be destroyed after interaction

        [Header("Add Item")]
        [SerializeField] protected string itemId; // Id of an item that will be given after interaction
        [SerializeField] private ushort quantity = 1; // Quantity of an item that will be given after interaction

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