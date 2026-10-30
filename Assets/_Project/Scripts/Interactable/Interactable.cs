using ProjectPostapo.ScriptableObjects;
using ProjectPostapo.Stats;
using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string objectId; // Id of an interactable object

        /// <summary>
        /// This method validates if the interaction was correct and let overrides an implementation
        /// In fact it should be named TryInteract
        /// </summary>
        /// <param name="interactor">Object that try to interact</param>
        /// <param name="interactorRange">Range of an interactor</param>
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

            if (Vector3.Distance(interactor.transform.position, transform.position) > interactorRange * 2f)
            {
                Debug.LogWarning("Interactor is too far away to interact with this object.");
                return;
            }
        }

        public string GetId()
        {
            return objectId;
        }
    }
}