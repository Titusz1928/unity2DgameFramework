using System;
using System.Collections.Generic;
using UnityEngine;

namespace TitusGames.Framework
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        // Global access point to the locator itself
        public static ServiceLocator Current { get; private set; }

        public static void Initialize()
        {
            Current = new ServiceLocator();
        }

        /// <summary>
        /// Registers a service implementation against its interface type.
        /// </summary>
        public void Register<T>(T service)
        {
            Type type = typeof(T);
            if (_services.ContainsKey(type))
            {
                Debug.LogWarning($"[ServiceLocator] Service of type {type.Name} is already registered.");
                return;
            }
            _services.Add(type, service);
        }

        /// <summary>
        /// Resolves and returns the requested service.
        /// </summary>
        public T Get<T>()
        {
            Type type = typeof(T);
            if (!_services.TryGetValue(type, out var service))
            {
                throw new Exception($"[ServiceLocator] Service of type {type.Name} is not registered!");
            }
            return (T)service;
        }

        public void Unregister<T>()
        {
            Type type = typeof(T);
            if (_services.ContainsKey(type))
            {
                _services.Remove(type);
            }
        }
    }
}