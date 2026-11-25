using System;
using System.Collections.Generic;

namespace Nova.Engine.AI
{
    public sealed class NovaFiniteStateMachine<TState> where TState : struct, Enum
    {
        private readonly Dictionary<TState, Action> enter = new Dictionary<TState, Action>();
        private readonly Dictionary<TState, Action> update = new Dictionary<TState, Action>();
        private readonly Dictionary<TState, Action> exit = new Dictionary<TState, Action>();

        public TState Current { get; private set; }
        public bool IsInitialized { get; private set; }

        public void AddState(TState state, Action onEnter = null, Action onUpdate = null, Action onExit = null)
        {
            enter[state] = onEnter;
            update[state] = onUpdate;
            exit[state] = onExit;
        }

        public void Start(TState state)
        {
            if (IsInitialized) ChangeState(state);
            else
            {
                Current = state;
                IsInitialized = true;
                Invoke(enter, Current);
            }
        }

        public void ChangeState(TState next)
        {
            if (IsInitialized && EqualityComparer<TState>.Default.Equals(Current, next))
                return;

            if (IsInitialized) Invoke(exit, Current);
            Current = next;
            IsInitialized = true;
            Invoke(enter, Current);
        }

        public void Tick()
        {
            if (IsInitialized) Invoke(update, Current);
        }

        private static void Invoke(Dictionary<TState, Action> map, TState state)
        {
            if (map.TryGetValue(state, out Action callback))
                callback?.Invoke();
        }
    }
}
