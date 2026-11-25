using UnityEngine;

namespace Nova.Engine
{
    public static class NovaPlatform
    {
        public static RuntimePlatform Current => Application.platform;
        public static bool IsMobile => Application.isMobilePlatform;
        public static bool IsAndroid => Application.platform == RuntimePlatform.Android;
        public static bool IsIOS => Application.platform == RuntimePlatform.IPhonePlayer;
        public static bool IsDesktop =>
            Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer;
    }
}
