using ProjectPostapo.Interaction;
using ProjectPostapo.ScriptableObjects;
using ProjectPostapo.Stats;
using ProjectPostapo.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectPostapo.Player
{
    public class PlayerEye : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;

        private float distance = 2f;

        private IGetStatValue playerStats;

        public void Start()
        {
            if (!TryGetComponent<IGetStatValue>(out playerStats))
            {
                Debug.LogWarning("IGetStatValue not found");
                return;
            }

            distance = playerStats.GetStatValue(StatType.InteractionRange);
        }

        public void FixedUpdate()
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(playerCamera.scaledPixelWidth / 2, playerCamera.scaledPixelHeight / 2, transform.position.z));
            //Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                UIEvents.t_OnHideCenteredText?.Invoke();
                return;
            }

            if (!hit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                return;
            }

            UIEvents.t_OnShowCenteredText?.Invoke($"Press E to interact with: {interactable.GetName()}");
        }

        public void TryInteract()
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(playerCamera.scaledPixelWidth / 2, playerCamera.scaledPixelHeight / 2, transform.position.z));
            //Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                return;
            }

            if (!hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                UIEvents.t_OnHideCenteredText?.Invoke();
                return;
            }

            if (Vector3.Distance(hit.collider.transform.position, transform.position) > distance)
            {
                return;
            }

            UIEvents.t_OnShowCenteredText?.Invoke($"Press E to interact with {interactable.GetName()}");

            interactable.Interact(gameObject, distance);
            UIEvents.t_OnHideCenteredText?.Invoke();
        }
    }
}
