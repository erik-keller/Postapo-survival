using UnityEngine;

namespace ProjectPostapo.Player
{
    [RequireComponent(typeof(Transform))]
    public class MouseMovement : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 100f; // TODO: Make this adjustable in the settings menu

        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Camera playerCamera;

        private float xRotation = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void MoveCamera(Vector2 input)
        {
            Vector2 mouse = new Vector2(input.x, input.y) * sensitivity * Time.fixedDeltaTime;

            xRotation -= mouse.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            transform.Rotate(0f, mouse.x, 0f);
        }

        public void OpenInventoryMouseBehaviour()
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Inventory opened. Cursor unlocked.");
                return;

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("Inventory closed. Cursor locked.");
                return;
            }
        }
    }
}