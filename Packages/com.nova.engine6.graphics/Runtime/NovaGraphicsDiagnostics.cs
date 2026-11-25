using UnityEngine;

namespace Nova.Engine.Graphics
{
    public static class NovaGraphicsDiagnostics
    {
        public static string GetReport()
        {
            return
                $"Graphics API: {SystemInfo.graphicsDeviceType}\n" +
                $"GPU: {SystemInfo.graphicsDeviceName}\n" +
                $"GPU Vendor: {SystemInfo.graphicsDeviceVendor}\n" +
                $"GPU Memory: {SystemInfo.graphicsMemorySize} MB\n" +
                $"Max Texture Size: {SystemInfo.maxTextureSize}\n" +
                $"Screen: {Screen.width} x {Screen.height}\n" +
                $"Refresh Rate: {Screen.currentResolution.refreshRateRatio.value:0.##} Hz\n" +
                $"Target FPS: {Application.targetFrameRate}\n" +
                $"NOVA Render Scale Target: {NovaGraphicsRuntime.RenderScale:0.00}";
        }

        public static void LogReport()
        {
            Debug.Log("[NOVA GRAPHICS DIAGNOSTICS]\n" + GetReport());
        }
    }
}
