using UnityEngine;

namespace Nova.Engine.Graphics
{
    [DisallowMultipleComponent]
    public sealed class NovaLightRig : MonoBehaviour
    {
        [SerializeField] private Light directionalLight;
        [SerializeField, Range(0f, 8f)]
        private float intensity = 1.2f;
        [SerializeField] private Color lightColor = new Color(1f, 0.95f, 0.85f);

        private void Awake()
        {
            if (directionalLight == null)
                directionalLight = GetComponentInChildren<Light>();

            Apply();
        }

        public void Apply()
        {
            if (directionalLight == null)
            {
                Debug.LogWarning("[NOVA Graphics] No directional light assigned.");
                return;
            }

            directionalLight.type = LightType.Directional;
            directionalLight.intensity = Mathf.Clamp(intensity, 0f, 8f);
            directionalLight.color = lightColor;
            RenderSettings.sun = directionalLight;
        }
    }
}
