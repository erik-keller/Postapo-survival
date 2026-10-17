using UnityEngine;

namespace ProjectPostapo.Interaction
{
    interface IInteractable
    {
        /// <summary>
        /// Interact with the object.
        /// </summary>
        public void Interact(GameObject interactor);
        public string GetName();
    }
}