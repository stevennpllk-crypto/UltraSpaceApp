using UnityEngine;

namespace Nova.Engine6.World
{
    [DisallowMultipleComponent]
    public sealed class NovaEnvironmentManager : MonoBehaviour
    {
        [SerializeField] private Color ambientColor = new Color(0.45f, 0.5f, 0.58f);
        [SerializeField] private bool fogEnabled;
        [SerializeField] private Color fogColor = new Color(0.6f, 0.65f, 0.7f);
        [SerializeField, Range(0f, 0.1f)] private float fogDensity = 0.005f;

        private void OnEnable() { Apply(); }

        [ContextMenu("Apply Environment")]
        public void Apply()
        {
            RenderSettings.ambientLight = ambientColor;
            RenderSettings.fog = fogEnabled;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
        }
    }
}
