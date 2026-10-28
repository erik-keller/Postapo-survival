using UnityEngine;

namespace ProjectPostapo.Interaction
{
    public interface IInteractable
    {
        /// <summary>
        /// Interact with the object.
        /// </summary>
        public void Interact(GameObject interactor, float interactorRange = 2f);
        public string GetId();
    }
}