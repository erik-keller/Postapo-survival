// using ProjectPostapo.Player;
using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public class Pickable : Interactable
    {
        //[SerializeField] private ItemScriptableObject item;
        [SerializeField] private GameObject objectToDestroy;
        [SerializeField] private ushort quantity = 1;

        public override void Interact(GameObject interactor)
        {
            base.Interact(interactor);
            /*
            if (interactor.TryGetComponent(out PlayerInventory p))
            {
                Debug.Log($"Player interacted with {gameObject.name} and is adding {item.itemName} to inventory.");
                if (p.Inventory.AddItem(item, quantity))
                {
                    Debug.Log($"{item.itemName} added to inventory. Destroying {gameObject.name}.");
                    Destroy(objectToDestroy);
                }
            }
            else
            {
                Debug.LogError("Player doesn't have a PlayerInventory component.");
            }*/
        }
    }
}