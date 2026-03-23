using UnityEngine;
using UnityEngine.InputSystem;

namespace game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterController cc;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float gravity = -9.8f;
        [SerializeField] private float speedMultipler = 1.5f;
        [SerializeField] private float boostDuration = 3f;

        private float originalSpeed;
        private Coroutine speedBoostCoroutine;

        [Header("Camera")]
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float sensitivity = 100f;
        [SerializeField] private InputActionReference lookAction;

        private Vector2 moveInput;
        private Vector3 velocity;
        private float xRotation = 0f;

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
            Debug.Log($"Move Input: {moveInput}");
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            Debug.Log($"Jumping {context.performed} - Is Grounded: {cc.isGrounded}");
            if (context.performed && cc.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            originalSpeed = speed;
        }

        private void OnEnable()
        {
            lookAction.action.Enable();
        }

        private void OnDisable()
        {
            lookAction.action.Disable();
        }

        private void Update()
        {
            // --- MOVEMENT ---
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            cc.Move(move * speed * Time.deltaTime);

            if (cc.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // small downward force to keep grounded
            }

            velocity.y += gravity * Time.deltaTime;
            cc.Move(velocity * Time.deltaTime);
        }

        private void LateUpdate()
        {
            Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
            // Debug.Log("LOOK: " + lookInput);

            // Look
            float mouseX = lookInput.x * sensitivity * Time.deltaTime;
            float mouseY = lookInput.y * sensitivity * Time.deltaTime;

            // Vertical camera
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Horizontal camera
            transform.Rotate(Vector3.up * mouseX);
        }

        public void SpeedBoost()
        {
            // If already boosting → reset timer
            if (speedBoostCoroutine != null)
            {
                StopCoroutine(speedBoostCoroutine);
            }

            speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine());
        }

        // Increases speed and reverts it after the speed boost ends
        private System.Collections.IEnumerator SpeedBoostRoutine()
        {
            speed = originalSpeed * speedMultipler;

            yield return new WaitForSeconds(boostDuration);

            speed = originalSpeed;
            speedBoostCoroutine = null;
        }
    }
}