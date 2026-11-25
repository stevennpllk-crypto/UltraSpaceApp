using UnityEngine;

namespace Nova.Engine.Graphics
{
    public sealed class NovaQualityAutoConfigurator : MonoBehaviour
    {
        public enum DeviceTier { Low, Mid, High }

        [SerializeField] private bool applyOnStart = true;
        [SerializeField] private bool preferBatteryOnMobile = false;

        private void Start()
        {
            if (applyOnStart)
                Apply(DetectTier());
        }

        public DeviceTier DetectTier()
        {
            int ram = SystemInfo.systemMemorySize;
            int gpuMemory = SystemInfo.graphicsMemorySize;
            int cores = SystemInfo.processorCount;

            if (ram <= 3072 || (gpuMemory > 0 && gpuMemory <= 512) || cores <= 4)
                return DeviceTier.Low;

            if (ram >= 8192 && gpuMemory >= 2048 && cores >= 8)
                return DeviceTier.High;

            return DeviceTier.Mid;
        }

        public void Apply(DeviceTier tier)
        {
            bool mobile = Application.isMobilePlatform;

            switch (tier)
            {
                case DeviceTier.Low:
                    NovaGraphicsSettings.SetTargetFrameRate(30);
                    NovaGraphicsSettings.SetRenderScale(0.7f);
                    NovaGraphicsSettings.SetTextureMipmapLimit(1);
                    NovaGraphicsSettings.SetAnisotropicFiltering(false);
                    QualitySettings.shadows = ShadowQuality.Disable;
                    break;

                case DeviceTier.Mid:
                    NovaGraphicsSettings.SetTargetFrameRate(60);
                    NovaGraphicsSettings.SetRenderScale(mobile ? 0.85f : 1f);
                    NovaGraphicsSettings.SetTextureMipmapLimit(0);
                    NovaGraphicsSettings.SetAnisotropicFiltering(true);
                    QualitySettings.shadows = ShadowQuality.HardOnly;
                    break;

                case DeviceTier.High:
                    NovaGraphicsSettings.SetTargetFrameRate(60);
                    NovaGraphicsSettings.SetRenderScale(1f);
                    NovaGraphicsSettings.SetTextureMipmapLimit(0);
                    NovaGraphicsSettings.SetAnisotropicFiltering(true);
                    QualitySettings.shadows = ShadowQuality.All;
                    break;
            }

            if (mobile && preferBatteryOnMobile)
                NovaGraphicsSettings.SetTargetFrameRate(30);

            Debug.Log($"[NOVA Graphics] Applied {tier} preset.");
        }
    }
}
