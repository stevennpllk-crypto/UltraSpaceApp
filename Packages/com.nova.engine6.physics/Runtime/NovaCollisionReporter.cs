using UnityEngine;

namespace Nova.Engine.Physics
{
    public struct NovaCollisionEvent
    {
        public GameObject Self;
        public GameObject Other;
        public Vector3 Point;
        public Vector3 Normal;
        public float RelativeSpeed;
    }

    public sealed class NovaCollisionReporter : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Publish(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            // Intentionally not published every physics tick to avoid event flooding.
        }

        private void Publish(Collision collision)
        {
            if (collision == null || collision.gameObject == null) return;

            ContactPoint contact = collision.contactCount > 0
                ? collision.GetContact(0)
                : default;

            NovaEventBus.Publish(new NovaCollisionEvent
            {
                Self = gameObject,
                Other = collision.gameObject,
                Point = contact.point,
                Normal = contact.normal,
                RelativeSpeed = collision.relativeVelocity.magnitude
            });
        }
    }
}
