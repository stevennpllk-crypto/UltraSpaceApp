using UnityEngine;

namespace Nova.Engine.Physics
{
    public sealed class NovaPhysicsBootstrap : MonoBehaviour
    {
        [SerializeField] private float fixedTimestep = 0.02f;
        [SerializeField] private bool applyOnAwake = true;

        private void Awake()
        {
            if (!applyOnAwake) return;

            NovaPhysicsSettings.SetFixedTimestep(fixedTimestep);
            NovaPhysicsSettings.SetSolverIterations(8, 2);
        }
    }
}
