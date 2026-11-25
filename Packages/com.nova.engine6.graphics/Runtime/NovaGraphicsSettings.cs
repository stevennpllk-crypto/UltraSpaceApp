using UnityEngine;

namespace Nova.Engine.Graphics
{
    public static class NovaGraphicsSettings
    {
        public static void SetRenderScale(float scale)
        {
            // URP render scale is pipeline-asset specific; this setting is a safe
            // project-level target for systems that consume it.
            NovaGraphicsRuntime.RenderScale = Mathf.Clamp(scale, 0.5f, 1.5f);
        }

        public static void SetAnisotropicFiltering(bool enabled)
        {
            QualitySettings.anisotropicFiltering = enabled
                ? AnisotropicFiltering.Enable
                : AnisotropicFiltering.Disable;
        }

        public static void SetTextureMipmapLimit(int limit)
        {
            QualitySettings.globalTextureMipmapLimit = Mathf.Clamp(limit, 0, 3);
        }

        public static void SetVSync(bool enabled)
        {
            QualitySettings.vSyncCount = enabled ? 1 : 0;
        }

        public static void SetTargetFrameRate(int fps)
        {
            Application.targetFrameRate = Mathf.Clamp(fps, 30, 240);
        }
    }

    public static class NovaGraphicsRuntime
    {
        public static float RenderScale { get; internal set; } = 1f;
    }
}
