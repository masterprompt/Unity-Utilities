using System;
using System.Collections.Generic;

namespace Tangatek
{
    public class EventPublisher
    {
        private event Action<ScriptableEvent> onEvent = delegate(ScriptableEvent scriptableEvent) { };
        
        public void RaiseEvent(ScriptableEvent eventType)
        {
            onEvent(eventType);
        }

        public void Subscribe(Action<ScriptableEvent> action)
        {
            onEvent += action;
        }

        public void Unsubscribe(Action<ScriptableEvent> action)
        {
            onEvent -= action;
        }

        public class Catalog : Dictionary<ScriptableEvent, EventPublisher>
        {
            public EventPublisher Find(ScriptableEvent eventType)
            {
                EventPublisher publisher;
                this.TryGetValue(eventType, out publisher);
                if (publisher != null) return publisher;
                publisher = new EventPublisher();
                this.Add(eventType, publisher);
                return publisher;
            }
        }
    }
}