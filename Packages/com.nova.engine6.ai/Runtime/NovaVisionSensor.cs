using UnityEngine;

namespace Nova.Engine.AI
{
    public sealed class NovaVisionSensor : MonoBehaviour
    {
        [SerializeField] private Transform eye;
        [SerializeField] private float viewDistance = 12f;
        [SerializeField, Range(1f, 180f)] private float fieldOfView = 90f;
        [SerializeField] private LayerMask targetLayers;
        [SerializeField] private LayerMask obstructionLayers = ~0;

        public bool CanSee(Transform target)
        {
            if (target == null) return false;

            Transform origin = eye != null ? eye : transform;
            Vector3 offset = target.position - origin.position;
            float distance = offset.magnitude;

            if (distance > viewDistance || distance <= 0.001f)
                return false;

            if (Vector3.Angle(origin.forward, offset) > fieldOfView * 0.5f)
                return false;

            if (targetLayers.value != 0 &&
                (targetLayers.value & (1 << target.gameObject.layer)) == 0)
                return false;

            if (Physics.Raycast(origin.position, offset.normalized, out RaycastHit hit,
                distance, obstructionLayers, QueryTriggerInteraction.Ignore))
            {
                return hit.transform == target || hit.transform.IsChildOf(target);
            }

            return true;
        }

        private void OnDrawGizmosSelected()
        {
            Transform origin = eye != null ? eye : transform;
            Gizmos.DrawWireSphere(origin.position, viewDistance);
        }
    }
}
