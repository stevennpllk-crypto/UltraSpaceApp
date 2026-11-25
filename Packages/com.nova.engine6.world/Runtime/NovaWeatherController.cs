using UnityEngine;

namespace Nova.Engine6.World
{
    public enum NovaWeatherType { Clear, Cloudy, Rain, Storm, Snow }

    [DisallowMultipleComponent]
    public sealed class NovaWeatherController : MonoBehaviour
    {
        [SerializeField] private NovaWeatherType currentWeather = NovaWeatherType.Clear;
        [SerializeField] private ParticleSystem precipitation;
        [SerializeField] private Light sun;
        [SerializeField] private Color clearAmbient = new Color(0.55f, 0.62f, 0.72f);
        [SerializeField] private Color cloudyAmbient = new Color(0.32f, 0.36f, 0.42f);
        [SerializeField] private Color stormAmbient = new Color(0.18f, 0.20f, 0.25f);
        [SerializeField, Range(0f, 1f)] private float fogForCloudy = 0.008f;
        [SerializeField, Range(0f, 1f)] private float fogForStorm = 0.025f;

        public NovaWeatherType CurrentWeather => currentWeather;

        private void Start() { ApplyWeather(currentWeather); }

        public void ApplyWeather(NovaWeatherType weather)
        {
            currentWeather = weather;
            bool wet = weather == NovaWeatherType.Rain || weather == NovaWeatherType.Storm || weather == NovaWeatherType.Snow;
            if (precipitation != null)
            {
                if (wet && !precipitation.isPlaying) precipitation.Play();
                if (!wet && precipitation.isPlaying) precipitation.Stop();
            }

            RenderSettings.ambientLight = weather == NovaWeatherType.Clear ? clearAmbient :
                (weather == NovaWeatherType.Storm ? stormAmbient : cloudyAmbient);

            RenderSettings.fog = weather != NovaWeatherType.Clear;
            RenderSettings.fogDensity = weather == NovaWeatherType.Storm ? fogForStorm :
                (weather == NovaWeatherType.Cloudy ? fogForCloudy : fogForCloudy * 0.5f);

            if (sun != null)
                sun.intensity = weather == NovaWeatherType.Storm ? 0.35f :
                    (weather == NovaWeatherType.Cloudy ? 0.65f : 1f);
        }
    }
}
