using UnityEngine;

namespace Nova.Engine6.Audio
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public sealed class NovaAudioEmitter : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private bool playOnStart;
        [SerializeField] private bool randomClip;
        [SerializeField, Range(0f, 1f)] private float volume = 1f;
        [SerializeField] private Vector2 pitchRange = new Vector2(0.95f, 1.05f);

        private AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.volume = Mathf.Clamp01(volume);
            source.spatialBlend = 1f;
        }

        private void Start()
        {
            if (playOnStart) Play();
        }

        public void Play()
        {
            if (clips == null || clips.Length == 0 || source == null) return;

            AudioClip clip = clips[Random.Range(0, clips.Length)];
            if (clip == null) return;

            source.clip = clip;
            source.pitch = Random.Range(
                Mathf.Min(pitchRange.x, pitchRange.y),
                Mathf.Max(pitchRange.x, pitchRange.y));
            source.Play();
        }

        public void PlayClip(AudioClip clip)
        {
            if (clip == null || source == null) return;
            source.PlayOneShot(clip, Mathf.Clamp01(volume));
        }

        public void Stop()
        {
            if (source != null) source.Stop();
        }
    }
}
