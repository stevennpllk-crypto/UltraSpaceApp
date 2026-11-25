using UnityEngine;

namespace Nova.Engine
{
    public static class NovaDiagnostics
    {
        public static string GetReport()
        {
            return
                $"{NovaVersion.ProductName} {NovaVersion.Version}\n" +
                $"Unity: {Application.unityVersion}\n" +
                $"Platform: {NovaPlatform.Current}\n" +
                $"Initialized: {NovaGameManager.IsInitialized}\n" +
                NovaDeviceProfile.GetSummary();
        }

        public static void LogReport()
        {
            Debug.Log("[NOVA DIAGNOSTICS]\n" + GetReport());
        }
    }
}
