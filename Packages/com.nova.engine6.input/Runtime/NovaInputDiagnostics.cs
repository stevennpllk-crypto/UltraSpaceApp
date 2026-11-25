using UnityEngine;

namespace Nova.Engine6.Input
{
    public sealed class NovaInputDiagnostics : MonoBehaviour
    {
        [SerializeField] private NovaInputReader inputReader;

        [ContextMenu("Log Input Diagnostics")]
        public void LogDiagnostics()
        {
            if (inputReader == null)
            {
                Debug.LogWarning("[NOVA INPUT] No input reader assigned.");
                return;
            }

            Debug.Log("[NOVA INPUT] Move=" + inputReader.Move +
                " | JumpPressed=" + inputReader.JumpPressed +
                " | Platform=" + Application.platform);
        }
    }
}
