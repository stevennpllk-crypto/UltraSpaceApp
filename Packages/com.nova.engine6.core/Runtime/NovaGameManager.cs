using UnityEngine;

namespace Nova.Engine
{
    public static class NovaGameManager
    {
        public static bool IsInitialized { get; private set; }

        public static void Initialize()
        {
            if (IsInitialized) return;

            IsInitialized = true;
            Application.targetFrameRate = 60;
            Debug.Log("[NOVA ENGINE 6] Game manager ready.");
        }

        public static void SetTargetFrameRate(int framesPerSecond)
        {
            Application.targetFrameRate = Mathf.Clamp(framesPerSecond, 30, 240);
        }

        public static void Pause(bool paused)
        {
            Time.timeScale = paused ? 0f : 1f;
        }
    }
}
