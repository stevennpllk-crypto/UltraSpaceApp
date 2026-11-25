using System;
using System.Collections.Generic;

namespace Nova.Engine
{
    /// <summary>Simple typed service registry for runtime systems.</summary>
    public static class NovaServiceRegistry
    {
        private static readonly Dictionary<Type, object> Services =
            new Dictionary<Type, object>();

        public static void Register<T>(T service) where T : class
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            Services[typeof(T)] = service;
        }

        public static bool TryGet<T>(out T service) where T : class
        {
            if (Services.TryGetValue(typeof(T), out object value))
            {
                service = value as T;
                return service != null;
            }

            service = null;
            return false;
        }

        public static T Get<T>() where T : class
        {
            if (TryGet<T>(out T service))
                return service;

            throw new InvalidOperationException(
                $"[NOVA] Service not registered: {typeof(T).FullName}");
        }

        public static bool Unregister<T>() where T : class
        {
            return Services.Remove(typeof(T));
        }

        public static void Clear()
        {
            Services.Clear();
        }
    }
}
