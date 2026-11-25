using UnityEngine;

namespace Nova.Engine
{
    public static class NovaDeviceProfile
    {
        public static string DeviceName => SystemInfo.deviceName;
        public static string OperatingSystem => SystemInfo.operatingSystem;
        public static string GraphicsDevice => SystemInfo.graphicsDeviceName;
        public static int ProcessorCount => SystemInfo.processorCount;
        public static int SystemMemoryMb => SystemInfo.systemMemorySize;
        public static int GraphicsMemoryMb => SystemInfo.graphicsMemorySize;
        public static bool IsLowMemoryDevice => SystemInfo.systemMemorySize > 0 &&
                                                SystemInfo.systemMemorySize <= 3072;

        public static string GetSummary()
        {
            return
                $"Device: {DeviceName}\n" +
                $"OS: {OperatingSystem}\n" +
                $"GPU: {GraphicsDevice}\n" +
                $"CPU Cores: {ProcessorCount}\n" +
                $"RAM: {SystemMemoryMb} MB\n" +
                $"GPU Memory: {GraphicsMemoryMb} MB";
        }
    }
}
