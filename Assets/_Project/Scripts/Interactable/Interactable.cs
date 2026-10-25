using ProjectPostapo.ScriptableObjects;
using ProjectPostapo.Stats;
using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string objectName;

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
            return objectName;
        }
    }
}