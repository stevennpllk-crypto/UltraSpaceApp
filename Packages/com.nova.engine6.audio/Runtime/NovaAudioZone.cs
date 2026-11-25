using UnityEngine;

namespace Nova.Engine6.Audio
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider))]
    public sealed class NovaAudioZone : MonoBehaviour
    {
        [SerializeField] private AudioClip ambientClip;
        [SerializeField, Range(0f, 1f)] private float zoneVolume = 0.6f;
        [SerializeField, Min(0f)] private float fadeSeconds = 1f;

        private AudioSource source;
        private float targetVolume;

        private void Awake()
        {
            Collider zone = GetComponent<Collider>();
            zone.isTrigger = true;

            source = gameObject.AddComponent<AudioSource>();
            source.clip = ambientClip;
            source.loop = true;
            source.playOnAwake = false;
            source.spatialBlend = 0f;
            source.volume = 0f;
        }

        private void Update()
        {
            if (source == null) return;
            float speed = fadeSeconds <= 0f ? 100f : 1f / fadeSeconds;
            source.volume = Mathf.MoveTowards(source.volume, targetVolume, Time.deltaTime * speed);

            if (source.volume > 0.001f && !source.isPlaying && ambientClip != null)
                source.Play();
            if (targetVolume <= 0f && source.volume <= 0.001f && source.isPlaying)
                source.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                targetVolume = zoneVolume;
                if (ambientClip != null && !source.isPlaying) source.Play();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) targetVolume = 0f;
        }
    }
}
