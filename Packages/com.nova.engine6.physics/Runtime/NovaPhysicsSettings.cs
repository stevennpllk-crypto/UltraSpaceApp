using UnityEngine;

namespace Nova.Engine.Physics
{
    public static class NovaPhysicsSettings
    {
        public static void SetFixedTimestep(float seconds)
        {
            Time.fixedDeltaTime = Mathf.Clamp(seconds, 0.005f, 0.05f);
        }

        public static void SetGravity(Vector3 gravity)
        {
            Physics.gravity = gravity;
        }

        public static void SetSolverIterations(int positionIterations, int velocityIterations)
        {
            Physics.defaultSolverIterations = Mathf.Clamp(positionIterations, 1, 20);
            Physics.defaultSolverVelocityIterations = Mathf.Clamp(velocityIterations, 1, 20);
        }

        public static void SetAutoSimulation(bool enabled)
        {
            Physics.simulationMode = enabled
                ? SimulationMode.FixedUpdate
                : SimulationMode.Script;
        }
    }
}
