using UnityEngine;

namespace Nova.Engine6.Input
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]
    public sealed class NovaCharacterMotor : MonoBehaviour
    {
        [SerializeField] private NovaInputReader inputReader;
        [SerializeField] private Transform movementReference;
        [SerializeField, Min(0f)] private float moveSpeed = 5f;
        [SerializeField, Min(0f)] private float jumpHeight = 1.5f;
        [SerializeField] private float gravity = -20f;
        [SerializeField] private bool rotateTowardsMovement = true;
        [SerializeField, Min(0f)] private float rotationSpeed = 12f;

        private CharacterController controller;
        private float verticalVelocity;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            if (inputReader == null) inputReader = GetComponent<NovaInputReader>();
        }

        private void Update()
        {
            if (inputReader == null || controller == null) return;

            Vector2 raw = inputReader.Move;
            Vector3 forward = movementReference != null ? movementReference.forward : transform.forward;
            Vector3 right = movementReference != null ? movementReference.right : transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 move = Vector3.ClampMagnitude(forward * raw.y + right * raw.x, 1f);
            controller.Move(move * moveSpeed * Time.deltaTime);

            if (controller.isGrounded && verticalVelocity < 0f)
                verticalVelocity = -2f;

            if (inputReader.JumpPressed && controller.isGrounded)
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

            verticalVelocity += gravity * Time.deltaTime;
            controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);

            if (rotateTowardsMovement && move.sqrMagnitude > 0.001f)
            {
                Quaternion target = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
