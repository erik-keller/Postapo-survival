using ProjectPostapo.InventorySystem;
using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public class Pickable : Interactable
    {
        [SerializeField] private string itemId;
        [SerializeField] private ushort quantity = 1;
        [SerializeField] private GameObject toDestroy;

        public override void Interact(GameObject interactor)
        {
            if (itemId == null || itemId == "")
            {
                Debug.LogWarning($"itemId is {itemId}(null or empty)");
                return;
            }
            base.Interact(interactor);
            
            if (interactor.TryGetComponent(out Inventory inventory))
            {
                Debug.Log($"Interacted with {gameObject.name} and is adding {itemId} to inventory.");
                if (inventory.AddItem(itemId, quantity))
                {
                    Debug.Log($"{itemId} added to inventory. Destroying {gameObject.name}.");
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
            else
            {
                Debug.LogError("Interactor don't have inventory.");
            }
        }
    }
}