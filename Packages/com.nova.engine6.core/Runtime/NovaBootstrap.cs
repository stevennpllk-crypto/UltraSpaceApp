using UnityEngine;

namespace Nova.Engine
{
    [DisallowMultipleComponent]
    public sealed class NovaBootstrap : MonoBehaviour
    {
        [SerializeField] private bool dontDestroyOnLoad = true;
        [SerializeField] private bool logStartup = true;
        [SerializeField] private bool logDeviceReport = false;

        private void Awake()
        {
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            NovaGameManager.Initialize();

            if (logStartup)
                Debug.Log($"[NOVA ENGINE 6] Core initialized. Platform: {NovaPlatform.Current}");

            if (logDeviceReport)
                NovaDiagnostics.LogReport();
        }
    }
}
