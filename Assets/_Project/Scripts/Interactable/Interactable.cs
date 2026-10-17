using UnityEngine;
using ProjectPostapo.Stats;

namespace ProjectPostapo.Interaction
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        public virtual void Interact(GameObject interactor)
        {
            if (interactor == null)
            {
                Debug.LogError("Interactor object is null. Cannot interact.");
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

        public virtual string GetName()
        {
            return string.Empty;
        }
    }
}