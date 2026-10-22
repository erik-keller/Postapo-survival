using ProjectPostapo.InventorySystem;
using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public class Pickable : Interactable
    {
        [SerializeField] private ushort quantity = 1;
        [SerializeField] private GameObject toDestroy;

        public override void Interact(GameObject interactor, float interactorRange = 2f)
        {
            if (itemId == null || itemId == string.Empty || itemId == "")
            {
                Debug.LogWarning($"itemId is {itemId}(null or empty)");
                return;
            }

            base.Interact(interactor);

            if (!interactor.TryGetComponent(out IAddItem inventory))
            {
                Debug.LogError("Interactor don't have inventory.");
                return;
            }

            if (!inventory.AddItem(itemId, quantity))
            {
                Debug.LogWarning("Cannot add item to inventory");
                return;
            }

            if (toDestroy == null)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(toDestroy);
            }
        }
    }
}