using UnityEngine;

namespace Nova.Engine.Physics
{
    public sealed class NovaGroundProbe : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private float radius = 0.2f;
        [SerializeField] private float distance = 0.35f;
        [SerializeField] private LayerMask groundLayers = ~0;

        public bool IsGrounded { get; private set; }
        public RaycastHit GroundHit { get; private set; }

        private void FixedUpdate()
        {
            Transform probe = origin != null ? origin : transform;
            IsGrounded = Physics.SphereCast(
                probe.position,
                Mathf.Max(0.01f, radius),
                Vector3.down,
                out RaycastHit hit,
                Mathf.Max(0.01f, distance),
                groundLayers,
                QueryTriggerInteraction.Ignore);

            GroundHit = IsGrounded ? hit : default;
        }

        private void OnDrawGizmosSelected()
        {
            Transform probe = origin != null ? origin : transform;
            Gizmos.DrawWireSphere(probe.position + Vector3.down * distance, radius);
        }
    }
}
