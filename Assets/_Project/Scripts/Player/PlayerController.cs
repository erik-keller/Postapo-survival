using ProjectPostapo.CreatureSystem;
using ProjectPostapo.Interaction;
using ProjectPostapo.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectPostapo.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CreatureMovement playerMovement;
        [SerializeField] private MouseMovement mouseMovement;
        [SerializeField] private Camera playerCamera;

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

            MoveCommand moveCommand = new(input, sprintAction.action.IsPressed());
            moveCommand.Execute(ref playerMovement);

            if (jumpAction.action.WasPerformedThisFrame())
            {
                JumpCommand jumpCommand = new();
                jumpCommand.Execute(ref playerMovement);
            }
        }

        private void HandleInteraction()
        {
            float distance = 2f;

            Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                UIEvents.t_OnHideCenteredText?.Invoke();
                return;
            }

            if (!hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                UIEvents.t_OnHideCenteredText?.Invoke();
                return;
            }

            UIEvents.t_OnShowCenteredText?.Invoke($"Press E to interact with {interactable.GetName()}");

            if (interactionAction.action.WasPressedThisFrame())
            {
                interactable.Interact(gameObject, distance);
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