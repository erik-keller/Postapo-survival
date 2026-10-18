using ProjectPostapo.Interaction;
using ProjectPostapo.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectPostapo.Player
{
    [RequireComponent(typeof(Transform))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] PlayerMovement playerMovement;
        [SerializeField] MouseMovement mouseMovement;
        [SerializeField] Camera playerCamera;

        [Header("Input Actions")]
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference jumpAction;
        [SerializeField] private InputActionReference interactionAction;
        [SerializeField] private InputActionReference sprintAction;
        [SerializeField] private InputActionReference lookAction;
        [SerializeField] private InputActionReference openInventoryAction;

        private void OnEnable()
        {
            moveAction.action.Enable();
            jumpAction.action.Enable();
            interactionAction.action.Enable();
            sprintAction.action.Enable();
            lookAction.action.Enable();
            openInventoryAction.action.Enable();
        }

        private void OnDisable()
        {
            moveAction.action.Disable();
            jumpAction.action.Disable();
            interactionAction.action.Disable();
            sprintAction.action.Disable();
            lookAction.action.Disable();
            openInventoryAction.action.Disable();
        }

        private void Update()
        {
            HandleMovement();
            HandleInteraction();
        }

        private void FixedUpdate()
        {
            HandleMouse();
            HandleInventoryOpen();
        }

        private void HandleMovement()
        {
            Vector2 input = moveAction.action.ReadValue<Vector2>();

            if (input != Vector2.zero)
            {
                playerMovement.Move(input, sprintAction.action.IsPressed());
            }
            else
            {
                playerMovement.Move(Vector2.zero, sprintAction.action.IsPressed());
            }

            if (jumpAction.action.WasPerformedThisFrame())
            {
                playerMovement.Jump();
            }
        }

        private void HandleInteraction()
        {
            Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, 2f))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    UIEvents.t_OnShowCenteredText?.Invoke("Press E to interact with " + interactable.GetName());
                    if (interactionAction.action.WasPressedThisFrame())
                    {
                        interactable.Interact(gameObject);
                    }
                }
            }
            else
            {
                UIEvents.t_OnHideCenteredText?.Invoke();
            }
        }

        private void HandleMouse()
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                return;
            }

            Vector2 input = lookAction.action.ReadValue<Vector2>();

            if (input != Vector2.zero)
            {
                mouseMovement.Look(input);
            }
        }

        private void HandleInventoryOpen()
        {
            if (openInventoryAction.action.WasPressedThisFrame())
            {
                mouseMovement.OpenInventoryMouseBehaviour();
            }
        }
    }
}