using UnityEngine;

namespace Nova.Engine6.Input
{
    [DisallowMultipleComponent]
    public sealed class NovaCameraLook : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float sensitivity = 0.15f;
        [SerializeField] private float minPitch = -35f;
        [SerializeField] private float maxPitch = 70f;
        [SerializeField] private bool useMouse = true;

        private float yaw;
        private float pitch;

        private void Start()
        {
            if (target == null) target = transform;
            Vector3 angles = target.eulerAngles;
            yaw = angles.y;
            pitch = angles.x;
        }

        private void Update()
        {
            Vector2 delta = Vector2.zero;
            if (useMouse && UnityEngine.Input.GetMouseButton(1))
            {
                delta = new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y")) * 5f;
            }

            yaw += delta.x * sensitivity;
            pitch = Mathf.Clamp(pitch - delta.y * sensitivity, minPitch, maxPitch);

            if (target != null)
                target.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        public void AddLookDelta(Vector2 delta)
        {
            yaw += delta.x * sensitivity;
            pitch = Mathf.Clamp(pitch - delta.y * sensitivity, minPitch, maxPitch);
        }
    }
}
