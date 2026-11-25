using UnityEngine;

namespace Nova.Engine6.Audio
{
    public sealed class NovaAudioDiagnostics : MonoBehaviour
    {
        [ContextMenu("Log Audio Diagnostics")]
        public void LogDiagnostics()
        {
            AudioSource[] sources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            int playing = 0;
            foreach (AudioSource source in sources)
                if (source != null && source.isPlaying) playing++;

            Debug.Log("[NOVA AUDIO] Sources: " + sources.Length +
                " | Playing: " + playing +
                " | Listener pause: " + AudioListener.pause);
        }
    }
}
