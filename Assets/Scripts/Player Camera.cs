// Unused due to deciding to use the lab to learn about Unity's new input system and being an idiot by not telling Unity to actually detect mouse movement

using UnityEngine;
using UnityEngine.InputSystem;

namespace game
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform playerBody;
        [SerializeField] private float sensitivity = 100f;

        private float xRotation = 0f;
        private Vector2 lookInput;

        private void Awake()
        {
            Debug.Log("Camera script is alive");
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Debug.Log("LOOK WORKS: " + context.ReadValue<Vector2>());
        }

        /*public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                lookInput = context.ReadValue<Vector2>();
                Debug.Log("LOOK: " + lookInput);
            }
            else if (context.canceled)
            {
                lookInput = Vector2.zero;
            }
        } */

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float mouseX = lookInput.x * sensitivity * Time.deltaTime;
            float mouseY = lookInput.y * sensitivity * Time.deltaTime;

            // Vertical rotation (camera only)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Horizontal rotation (player body)
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}