using UnityEngine;

namespace Nova.Engine6.Audio
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public sealed class NovaMusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] tracks;
        [SerializeField] private bool loopTrack = true;
        [SerializeField, Min(0f)] private float fadeDuration = 1.5f;
        [SerializeField, Range(0f, 1f)] private float targetVolume = 0.8f;

        private AudioSource source;
        private AudioClip pendingTrack;
        private float fadeSpeed;
        private bool fadingOut;
        private bool fadingIn;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = loopTrack;
            source.volume = targetVolume;
        }

        private void Update()
        {
            if (fadeDuration <= 0f)
            {
                if (fadingOut) FinishFadeOut();
                if (fadingIn) { source.volume = targetVolume; fadingIn = false; }
                return;
            }

            if (fadingOut)
            {
                source.volume = Mathf.MoveTowards(source.volume, 0f, Time.deltaTime / fadeDuration * targetVolume);
                if (source.volume <= 0.001f) FinishFadeOut();
            }
            else if (fadingIn)
            {
                source.volume = Mathf.MoveTowards(source.volume, targetVolume, Time.deltaTime / fadeDuration * targetVolume);
                if (source.volume >= targetVolume - 0.001f) fadingIn = false;
            }
        }

        public void PlayTrack(int index)
        {
            if (tracks == null || index < 0 || index >= tracks.Length || tracks[index] == null) return;
            pendingTrack = tracks[index];

            if (source.isPlaying && fadeDuration > 0f)
            {
                fadingIn = false;
                fadingOut = true;
            }
            else
            {
                StartPendingTrack();
            }
        }

        public void PlayRandomTrack()
        {
            if (tracks == null || tracks.Length == 0) return;
            PlayTrack(Random.Range(0, tracks.Length));
        }

        public void StopMusic()
        {
            pendingTrack = null;
            fadingIn = false;
            if (fadeDuration <= 0f) source.Stop();
            else fadingOut = true;
        }

        private void FinishFadeOut()
        {
            source.volume = 0f;
            source.Stop();
            fadingOut = false;
            if (pendingTrack != null) StartPendingTrack();
        }

        private void StartPendingTrack()
        {
            if (pendingTrack == null) return;
            source.clip = pendingTrack;
            source.loop = loopTrack;
            source.volume = fadeDuration > 0f ? 0f : targetVolume;
            source.Play();
            pendingTrack = null;
            fadingIn = fadeDuration > 0f;
        }
    }
}
