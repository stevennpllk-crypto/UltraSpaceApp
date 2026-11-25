using UnityEngine;

namespace Nova.Engine.AI
{
    public sealed class NovaAIDiagnostics : MonoBehaviour
    {
        [SerializeField] private NovaAIController controller;
        [SerializeField] private bool logOnStart;

        private void Start()
        {
            if (controller == null)
                controller = GetComponent<NovaAIController>();

            if (logOnStart)
                Debug.Log(GetReport());
        }

        public string GetReport()
        {
            string state = controller != null
                ? controller.CurrentState.ToString()
                : "No AI Controller";

            return $"[NOVA AI]\nAgent: {name}\nState: {state}";
        }
    }
}
