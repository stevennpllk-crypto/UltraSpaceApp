using UnityEngine;

namespace Nova.Engine6.World
{
    [DisallowMultipleComponent]
    public sealed class NovaChunkDistanceActivator : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private GameObject[] chunkRoots;
        [SerializeField, Min(1f)] private float activationDistance = 250f;
        [SerializeField, Min(0.05f)] private float checkInterval = 0.5f;
        [SerializeField] private bool useChunkTransformPosition = true;

        private float nextCheck;

        private void Update()
        {
            if (target == null || Time.time < nextCheck) return;
            nextCheck = Time.time + checkInterval;

            if (chunkRoots == null) return;
            float maxSqr = activationDistance * activationDistance;

            foreach (GameObject chunk in chunkRoots)
            {
                if (chunk == null) continue;
                Vector3 position = useChunkTransformPosition ? chunk.transform.position : transform.position;
                bool shouldBeActive = (position - target.position).sqrMagnitude <= maxSqr;
                if (chunk.activeSelf != shouldBeActive) chunk.SetActive(shouldBeActive);
            }
        }
    }
}
