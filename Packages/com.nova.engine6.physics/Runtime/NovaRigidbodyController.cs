using UnityEngine;

namespace Nova.Engine.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class NovaRigidbodyController : MonoBehaviour
    {
        [SerializeField] private Rigidbody body;
        [SerializeField] private float maxPlanarSpeed = 8f;
        [SerializeField] private float acceleration = 20f;

        public Rigidbody Body => body != null ? body : (body = GetComponent<Rigidbody>());

        private void Awake()
        {
            if (body == null) body = GetComponent<Rigidbody>();
        }

        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force)
        {
            if (Body != null && !Body.isKinematic)
                Body.AddForce(force, mode);
        }

        public void AddImpulse(Vector3 impulse)
        {
            AddForce(impulse, ForceMode.Impulse);
        }

        public void MovePlanar(Vector3 worldDirection)
        {
            if (Body == null || Body.isKinematic) return;

            Vector3 direction = Vector3.ProjectOnPlane(worldDirection, Vector3.up);
            if (direction.sqrMagnitude > 1f) direction.Normalize();

            Vector3 currentPlanar = Vector3.ProjectOnPlane(Body.linearVelocity, Vector3.up);
            Vector3 targetPlanar = direction * maxPlanarSpeed;
            Vector3 delta = targetPlanar - currentPlanar;
            Vector3 accelerationStep = Vector3.ClampMagnitude(delta, acceleration * Time.fixedDeltaTime);

            Body.AddForce(accelerationStep, ForceMode.VelocityChange);
        }

        public void StopPlanar()
        {
            if (Body == null || Body.isKinematic) return;
            Vector3 velocity = Body.linearVelocity;
            Body.linearVelocity = new Vector3(0f, velocity.y, 0f);
        }
    }
}
