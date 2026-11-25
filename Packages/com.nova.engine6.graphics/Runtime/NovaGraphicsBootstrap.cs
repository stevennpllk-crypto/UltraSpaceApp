using UnityEngine;

namespace Nova.Engine.Graphics
{
    public sealed class NovaGraphicsBootstrap : MonoBehaviour
    {
        [SerializeField] private bool logDiagnostics = true;
        [SerializeField] private NovaQualityAutoConfigurator qualityConfigurator;

        private void Awake()
        {
            if (qualityConfigurator == null)
                qualityConfigurator = GetComponent<NovaQualityAutoConfigurator>();

            if (logDiagnostics)
                NovaGraphicsDiagnostics.LogReport();
        }
    }
}
