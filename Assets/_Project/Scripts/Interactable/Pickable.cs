using ProjectPostapo.InventorySystem;
using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public class Pickable : Interactable
    {
        [SerializeField] private string itemId;
        [SerializeField] private ushort quantity = 1;

        public override void Interact(GameObject interactor)
        {
            base.Interact(interactor);
            
            if (interactor.TryGetComponent(out Inventory inventory))
            {
                Debug.Log($"Player interacted with {gameObject.name} and is adding {itemId} to inventory.");
                if (inventory.AddItem(itemId, quantity))
                {
                    Debug.Log($"{itemId} added to inventory. Destroying {gameObject.name}.");
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogError("Player doesn't have a PlayerInventory component.");
            }
        }
    }
}