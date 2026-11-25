using System;
using System.Collections.Generic;

namespace Nova.Engine
{
    /// <summary>Lightweight typed event bus. Unsubscribe when listeners are destroyed.</summary>
    public static class NovaEventBus
    {
        private static readonly Dictionary<Type, Delegate> Listeners =
            new Dictionary<Type, Delegate>();

        public static void Subscribe<T>(Action<T> listener)
        {
            if (listener == null) return;

            Type eventType = typeof(T);
            if (Listeners.TryGetValue(eventType, out Delegate existing))
                Listeners[eventType] = Delegate.Combine(existing, listener);
            else
                Listeners[eventType] = listener;
        }

        public static void Unsubscribe<T>(Action<T> listener)
        {
            if (listener == null) return;
            Type eventType = typeof(T);

            if (!Listeners.TryGetValue(eventType, out Delegate existing))
                return;

            Delegate updated = Delegate.Remove(existing, listener);
            if (updated == null)
                Listeners.Remove(eventType);
            else
                Listeners[eventType] = updated;
        }

        public static void Publish<T>(T gameEvent)
        {
            if (!Listeners.TryGetValue(typeof(T), out Delegate callbacks))
                return;

            if (callbacks is Action<T> typedCallbacks)
                typedCallbacks.Invoke(gameEvent);
        }

        public static void Clear()
        {
            Listeners.Clear();
        }
    }
}
