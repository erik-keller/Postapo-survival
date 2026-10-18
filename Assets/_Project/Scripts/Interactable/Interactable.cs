using ProjectPostapo.InventorySystem;
using ProjectPostapo.Stats;
using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected string itemId;
        [SerializeField] protected ItemDatabase itemDatabase;

        public virtual void Interact(GameObject interactor)
        {
            if (interactor == null)
            {
                Debug.LogError("Interactor object is null. Cannot interact.");
                return;
            }

            float interactorRange = 2f; // Default value

            if (interactor.TryGetComponent<IGetStatValue>(out IGetStatValue stats))
            {
                interactorRange = stats.GetStatValue(StatType.InteractionRange);
            }

            if (Vector3.Distance(interactor.transform.position, transform.position) > interactorRange)
            {
                Debug.LogWarning("Interactor is too far away to interact with this object.");
                return;
            }

            Debug.Log("Interacted with " + gameObject.name);
        }

        public string GetName()
        {
            if (itemDatabase == null)
            {
                Debug.LogWarning("itemDatabase is null");
                return string.Empty;
            }
            if (itemDatabase.Instance.GetItem(itemId).itemName == null)
            {
                Debug.LogWarning("itemName not found");
                return string.Empty;
            }
            return itemDatabase.Instance.GetItem(itemId).itemName;
        }
    }
}