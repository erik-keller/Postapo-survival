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
        [SerializeField] private PlayerEye playerEye;

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
            HandleInventoryOpen();
        }

        private void FixedUpdate()
        {
            HandleMouse();
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
            if (interactionAction.action.WasPressedThisFrame())
            {
                playerEye.TryInteract();
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
                mouseMovement.MoveCamera(input);
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