using UnityEngine;
using UnityEngine.Events;

namespace Nova.Engine.AI
{
    public enum NovaAIState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Flee,
        Disabled
    }

    [DisallowMultipleComponent]
    public sealed class NovaAIController : MonoBehaviour
    {
        [SerializeField] private NovaAIState initialState = NovaAIState.Idle;
        [SerializeField] private UnityEvent<NovaAIState> onStateChanged;

        public NovaAIState CurrentState { get; private set; }

        private void Awake()
        {
            CurrentState = initialState;
        }

        public void SetState(NovaAIState next)
        {
            if (CurrentState == next) return;
            NovaAIState previous = CurrentState;
            CurrentState = next;
            onStateChanged?.Invoke(next);

            NovaEventBus.Publish(new NovaAIStateChanged
            {
                Agent = gameObject,
                Previous = previous,
                Current = next
            });
        }
    }

    public struct NovaAIStateChanged
    {
        public GameObject Agent;
        public NovaAIState Previous;
        public NovaAIState Current;
    }
}
