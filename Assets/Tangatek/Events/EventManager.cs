using System;
using UnityEngine;

namespace Tangatek
{
    /// <summary>
    /// General event manager that can raise events, be scribed to general events, or targetted events
    /// </summary>
    [CreateAssetMenu(menuName="Scriptables/Events/Manager")]
    public class EventManager : ScriptableObject
    {
        private readonly EventPublisher eventPublisher = new EventPublisher();

        private readonly EventPublisher.Catalog eventPublisherCatalog = new EventPublisher.Catalog();
        
        public void RaiseEvent(ScriptableEvent eventType)
        {
            //    Raise general events
            eventPublisher.RaiseEvent(eventType);
            //    Raise subscribed events
            eventPublisherCatalog.Find(eventType).RaiseEvent(eventType);
        }

        /// <summary>
        /// Subscribe to every event (let implementer decide how to handle the event)
        /// </summary>
        /// <param name="action"></param>
        public void Subscribe(Action<ScriptableEvent> action)
        {
            eventPublisher.Subscribe(action);
        }

        /// <summary>
        /// Subscribe to a specific event type (only be notified of a specific event type)
        /// </summary>
        public Subscription Subscribe(Action<ScriptableEvent> action, ScriptableEvent eventType)
        {
            var publisher = eventPublisherCatalog.Find(eventType);
            publisher.Subscribe(action);
            return Subscription.Create(publisher, action);
        }

        /// <summary>
        /// Unsubscribe to every event (doesn't work for subscriptions)
        /// </summary>
        public void Unsubscribe(Action<ScriptableEvent> action)
        {
            eventPublisher.Unsubscribe(action);
        }
    }
}