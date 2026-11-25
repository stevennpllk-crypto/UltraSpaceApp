using UnityEngine;

namespace Nova.Engine.Physics
{
    public sealed class NovaPhysicsDebugger : MonoBehaviour
    {
        [SerializeField] private bool logOnStart = true;

        private void Start()
        {
            if (logOnStart)
                Debug.Log(GetReport());
        }

        public string GetReport()
        {
            return
                $"[NOVA PHYSICS]\n" +
                $"Gravity: {Physics.gravity}\n" +
                $"Fixed Timestep: {Time.fixedDeltaTime:0.000}\n" +
                $"Solver Iterations: {Physics.defaultSolverIterations}\n" +
                $"Velocity Iterations: {Physics.defaultSolverVelocityIterations}\n" +
                $"Auto Simulation: {Physics.simulationMode}";
        }
    }
}
