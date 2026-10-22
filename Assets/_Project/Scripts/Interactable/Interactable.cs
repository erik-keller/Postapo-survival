using ProjectPostapo.InventorySystem;
using ProjectPostapo.ScriptableObjects;
using ProjectPostapo.Stats;
using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected string itemId;
        [SerializeField] protected ItemDatabase itemDatabase;

        public virtual void Interact(GameObject interactor, float interactorRange = 2f)
        {
            if (interactor == null)
            {
                Debug.LogError("Interactor object is null. Cannot interact.");
                return;
            }

            if (interactor.TryGetComponent<IGetStatValue>(out IGetStatValue stats))
            {
                interactorRange = stats.GetStatValue(StatType.InteractionRange);
            }

            if (Vector3.Distance(interactor.transform.position, transform.position) > interactorRange)
            {
                Debug.LogWarning("Interactor is too far away to interact with this object.");
                return;
            }
        }

        public string GetName()
        {
            if (itemDatabase == null)
            {
                Debug.LogWarning("itemDatabase is null");
                return string.Empty;
            }

            IGetComponent name = itemDatabase.Instance.GetItem(itemId);

            if (name == null)
            {
                Debug.LogWarning("itemName not found");
                return string.Empty;
            }

            return name.GetComponentValue(ItemComponent.Name);
        }
    }
}