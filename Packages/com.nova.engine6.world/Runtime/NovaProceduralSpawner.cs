using System;
using UnityEngine;

namespace Nova.Engine6.World
{
    [DisallowMultipleComponent]
    public sealed class NovaProceduralSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs;
        [SerializeField, Min(0)] private int spawnCount = 40;
        [SerializeField] private Vector3 areaSize = new Vector3(50f, 0f, 50f);
        [SerializeField] private int seed = 12345;
        [SerializeField] private LayerMask groundMask = ~0;
        [SerializeField] private float raycastHeight = 200f;
        [SerializeField] private Transform spawnedParent;
        [SerializeField] private bool spawnOnStart;

        private void Start()
        {
            if (spawnOnStart) Generate();
        }

        [ContextMenu("Generate Objects")]
        public void Generate()
        {
            if (prefabs == null || prefabs.Length == 0) return;
            var random = new System.Random(seed);

            for (int i = 0; i < spawnCount; i++)
            {
                GameObject prefab = prefabs[random.Next(prefabs.Length)];
                if (prefab == null) continue;

                float x = transform.position.x + ((float)random.NextDouble() - 0.5f) * areaSize.x;
                float z = transform.position.z + ((float)random.NextDouble() - 0.5f) * areaSize.z;
                Vector3 origin = new Vector3(x, transform.position.y + raycastHeight, z);

                if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit,
                    raycastHeight * 2f, groundMask, QueryTriggerInteraction.Ignore))
                {
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    Instantiate(prefab, hit.point, rotation, spawnedParent);
                }
            }
        }
    }
}
