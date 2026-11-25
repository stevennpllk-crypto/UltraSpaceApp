using UnityEngine;

namespace Nova.Engine
{
    public sealed class NovaQualityController : MonoBehaviour
    {
        public enum Preset { BatterySaver, Balanced, High }

        [SerializeField] private Preset startupPreset = Preset.Balanced;

        private void Start()
        {
            Apply(startupPreset);
        }

        public void Apply(Preset preset)
        {
            switch (preset)
            {
                case Preset.BatterySaver:
                    Application.targetFrameRate = 30;
                    QualitySettings.shadows = ShadowQuality.Disable;
                    QualitySettings.antiAliasing = 0;
                    break;

                case Preset.Balanced:
                    Application.targetFrameRate = 60;
                    QualitySettings.shadows = ShadowQuality.HardOnly;
                    QualitySettings.antiAliasing = 2;
                    break;

                case Preset.High:
                    Application.targetFrameRate = 60;
                    QualitySettings.shadows = ShadowQuality.All;
                    QualitySettings.antiAliasing = 4;
                    break;
            }

            Debug.Log($"[NOVA ENGINE 6] Quality preset applied: {preset}");
        }
    }
}
