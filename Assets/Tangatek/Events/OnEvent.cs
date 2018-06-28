using UnityEngine;
using UnityEngine.Events;

namespace Tangatek
{
    /// <summary>
    /// Invokes UnityEvent when event is raised
    /// </summary>
    public class OnEvent : MonoBehaviour
    {
        public EventManager eventManager;
        public ScriptableEvent eventType;
        public UnityEvent unityEvent;
        private Subscription subscription;

        public void OnEnable()
        {
            if (eventManager == null) return;
            subscription = eventManager.Subscribe(OnRaisedEvent, eventType);
        }

        public void OnDisable()
        {
            subscription.Unsubscribe();
        }

        private void OnRaisedEvent(ScriptableEvent scriptableEvent)
        {
            unityEvent.Invoke();
        }
    }
}