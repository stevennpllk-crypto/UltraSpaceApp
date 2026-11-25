using UnityEngine;

namespace Nova.Engine6.World
{
    [DisallowMultipleComponent]
    public sealed class NovaTerrainGenerator : MonoBehaviour
    {
        [SerializeField, Range(33, 2049)] private int heightmapResolution = 257;
        [SerializeField] private Vector3 terrainSize = new Vector3(500f, 80f, 500f);
        [SerializeField, Min(0.001f)] private float noiseScale = 0.008f;
        [SerializeField, Range(0f, 1f)] private float heightStrength = 0.12f;
        [SerializeField] private int seed = 2026;

        [ContextMenu("Generate Terrain")]
        public void Generate()
        {
            int resolution = Mathf.Clamp(heightmapResolution, 33, 2049);
            // Unity terrain heightmaps use power-of-two resolution plus one.
            int intervals = Mathf.ClosestPowerOfTwo(resolution - 1);
            resolution = intervals + 1;

            TerrainData data = new TerrainData();
            data.heightmapResolution = resolution;
            data.size = new Vector3(
                Mathf.Max(1f, terrainSize.x),
                Mathf.Max(1f, terrainSize.y),
                Mathf.Max(1f, terrainSize.z));

            float[,] heights = new float[resolution, resolution];
            float offsetX = seed * 0.173f;
            float offsetZ = seed * 0.317f;

            for (int z = 0; z < resolution; z++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    float nx = x * noiseScale + offsetX;
                    float nz = z * noiseScale + offsetZ;
                    float noise = Mathf.PerlinNoise(nx, nz);
                    heights[z, x] = Mathf.Clamp01(0.02f + noise * heightStrength);
                }
            }

            data.SetHeights(0, 0, heights);
            GameObject terrainObject = Terrain.CreateTerrainGameObject(data);
            terrainObject.name = "NOVA Generated Terrain";
            terrainObject.transform.position = transform.position;
        }
    }
}
