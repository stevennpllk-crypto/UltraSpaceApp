using UnityEngine;
using UnityEngine.Audio;

namespace Nova.Engine6.Audio
{
    [DisallowMultipleComponent]
    public sealed class NovaAudioManager : MonoBehaviour
    {
        public static NovaAudioManager Instance { get; private set; }

        [SerializeField] private AudioMixer mixer;
        [SerializeField, Range(0f, 1f)] private float masterVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float musicVolume = 0.8f;
        [SerializeField, Range(0f, 1f)] private float sfxVolume = 1f;
        [SerializeField] private bool persistBetweenScenes = true;

        public AudioMixer Mixer => mixer;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            if (persistBetweenScenes) DontDestroyOnLoad(gameObject);
            ApplyVolumes();
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void SetMasterVolume(float value)
        {
            masterVolume = Mathf.Clamp01(value);
            ApplyVolumes();
        }

        public void SetMusicVolume(float value)
        {
            musicVolume = Mathf.Clamp01(value);
            ApplyVolumes();
        }

        public void SetSfxVolume(float value)
        {
            sfxVolume = Mathf.Clamp01(value);
            ApplyVolumes();
        }

        private void ApplyVolumes()
        {
            SetMixerLevel("MasterVolume", masterVolume);
            SetMixerLevel("MusicVolume", musicVolume);
            SetMixerLevel("SfxVolume", sfxVolume);
        }

        private void SetMixerLevel(string parameter, float linear)
        {
            if (mixer == null) return;
            float decibels = linear <= 0.0001f ? -80f : Mathf.Log10(linear) * 20f;
            mixer.SetFloat(parameter, decibels);
        }
    }
}
