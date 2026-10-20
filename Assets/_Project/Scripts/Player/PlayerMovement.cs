using ProjectPostapo.Core;
using ProjectPostapo.CreatureSystem;
using ProjectPostapo.Stats;
using UnityEngine;

namespace ProjectPostapo.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : CreatureMovement
    {
        [SerializeField] private CharacterController characterController;

        private IGetStatValue playerStats;

        private float playerSpeed = 5f;
        private float playerSprintMultiplier = 1.5f;
        private float jumpHeight = 2f;

        private float gravityValue = GlobalVariables.t_GravityValue;

        private Vector3 playerVelocity;
        private bool groundedPlayer => characterController.isGrounded;

        private void Start()
        {
            if (!TryGetComponent<IGetStatValue>(out playerStats))
            {
                Debug.LogWarning("IGetStatValue not found");
                return;
            }

            playerSpeed = playerStats.GetStatValue(StatType.MovementSpeed);
            playerSprintMultiplier = playerStats.GetStatValue(StatType.SprintMultiplier);
            jumpHeight = playerStats.GetStatValue(StatType.JumpHeight);
        }

        private void Update()
        {
            // Reset vertical velocity when grounded to prevent gravity buildup
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }

            // Apply gravity over time
            playerVelocity.y += gravityValue * Time.deltaTime;

            // Move the controller using the accumulated velocity
            characterController.Move(playerVelocity * Time.deltaTime);
        }

        public override void Move(Vector2 input, bool isSprinting)
        {
            Vector3 move = (input.x * transform.right) + (transform.forward * input.y);
            move = Vector3.ClampMagnitude(move, 1f);

            float currentSpeed = playerSpeed;
            if (isSprinting)
            {
                currentSpeed *= playerSprintMultiplier;
            }

            // Set horizontal velocity directly instead of accumulating it infinitely
            playerVelocity.x = move.x * currentSpeed;
            playerVelocity.z = move.z * currentSpeed;
        }

        public override void Jump()
        {
            if (groundedPlayer)
            {
                // Standard physics formula for jump velocity: v = sqrt(height * -2 * gravity)
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            }
        }
    }
}