using UnityEngine;

namespace Nova.Engine6.World
{
    [DisallowMultipleComponent]
    public sealed class NovaDayNightCycle : MonoBehaviour
    {
        [SerializeField] private Light sun;
        [SerializeField, Range(0f, 24f)] private float timeOfDay = 12f;
        [SerializeField, Min(0f)] private float dayLengthMinutes = 10f;
        [SerializeField] private bool runOnStart = true;
        [SerializeField] private Gradient ambientColor;
        [SerializeField] private AnimationCurve sunIntensity = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public float TimeOfDay => timeOfDay;

        private void Update()
        {
            if (runOnStart && dayLengthMinutes > 0f)
            {
                timeOfDay = (timeOfDay + 24f * Time.deltaTime / (dayLengthMinutes * 60f)) % 24f;
            }

            float normalized = timeOfDay / 24f;
            if (sun != null)
            {
                sun.transform.rotation = Quaternion.Euler((normalized * 360f) - 90f, 170f, 0f);
                sun.intensity = Mathf.Max(0f, sunIntensity.Evaluate(Mathf.Clamp01(Mathf.Sin(normalized * Mathf.PI * 2f) * 0.5f + 0.5f)));
            }

            if (ambientColor != null)
                RenderSettings.ambientLight = ambientColor.Evaluate(normalized);
        }

        public void SetTime(float hours)
        {
            timeOfDay = Mathf.Repeat(hours, 24f);
        }

        public void SetRunning(bool running)
        {
            runOnStart = running;
        }
    }
}
