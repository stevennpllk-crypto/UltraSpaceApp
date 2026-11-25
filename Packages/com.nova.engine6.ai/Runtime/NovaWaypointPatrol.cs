using UnityEngine;

namespace Nova.Engine.AI
{
    [RequireComponent(typeof(NovaAIController))]
    public sealed class NovaWaypointPatrol : MonoBehaviour
    {
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float moveSpeed = 2.5f;
        [SerializeField] private float turnSpeed = 8f;
        [SerializeField] private float arriveDistance = 0.25f;
        [SerializeField] private bool loop = true;

        private int currentIndex;
        private NovaAIController ai;

        private void Awake()
        {
            ai = GetComponent<NovaAIController>();
        }

        private void Update()
        {
            if (waypoints == null || waypoints.Length == 0 ||
                ai == null || ai.CurrentState != NovaAIState.Patrol)
                return;

            Transform point = waypoints[currentIndex];
            if (point == null) return;

            Vector3 direction = point.position - transform.position;
            direction.y = 0f;

            if (direction.magnitude <= arriveDistance)
            {
                Advance();
                return;
            }

            Vector3 move = direction.normalized;
            transform.position += move * moveSpeed * Time.deltaTime;

            if (move.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }
        }

        private void Advance()
        {
            if (currentIndex + 1 < waypoints.Length)
                currentIndex++;
            else if (loop)
                currentIndex = 0;
            else
                ai.SetState(NovaAIState.Idle);
        }
    }
}
