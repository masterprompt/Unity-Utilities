using System;

namespace Tangatek
{
    /// <summary>
    /// Subscription is a shortcut to a named event
    /// </summary>
    public class Subscription
    {
        private EventPublisher publisher;
        private Action<ScriptableEvent> action;

        /// <summary>
        /// Removes the subscription
        /// </summary>
        public void Unsubscribe()
        {
            if (publisher == null) return;
            publisher.Unsubscribe(action);
            publisher = null;
        }

        internal static Subscription Create(EventPublisher publisher, Action<ScriptableEvent> action)
        {
            return new Subscription()
            {
                publisher = publisher,
                action = action
            };
        }
    }
}