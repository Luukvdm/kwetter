using System;
using System.Collections.Generic;
using System.Linq;
using Kwetter.BuildingBlocks.EventBus.EventBus.Events;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;

namespace Kwetter.BuildingBlocks.EventBus.EventBus
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        // private readonly ICollection<string> _events;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubscriptionsManager()
        {
            // _events = new List<string>();
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public bool IsEmpty => !_handlers.Keys.Any();
        public void Clear() => _handlers.Clear();

        public IEnumerable<Type> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            string key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }

        public IEnumerable<Type> GetHandlersForEvent(string eventName) => _handlers[eventName];

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            string eventName = GetEventKey<T>();
            var handlerType = typeof(TH);

            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }
            
            if (_handlers[eventName].Any(s => s == handlerType))
            {
                throw new ArgumentException($"Event Type {eventName} already registered", nameof(eventName));
            }
            
            _handlers[eventName].Add(handlerType);
            
            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }

        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            string eventName = GetEventKey<T>();
            var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
            if (eventType != null)
            {
                _eventTypes.Remove(eventType);
            }

            RaiseOnEventRemoved(eventName);
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            string key = GetEventKey<T>();
            return HasSubscriptionsForEvent(key);
        }

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }
    }
}