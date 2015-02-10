using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;

namespace Injection
{
    public class PublishedEvents
    {
        #region Declarations
        private Dictionary<string, object> _events = new Dictionary<string, object>();
        #endregion

        #region Functions
        public void AddEvent(object source, string eventName)
        {
            if (source != null)
                _events.Add(eventName, source);
        }

        public void AddInvoker(object target, string eventName, string methodName)
        {
            EventInfo eventInfo = GetEventInfo(eventName);
            MethodInfo methodInfo = GetMethodInfo(target, methodName);

            AddInvoker(target, eventInfo, methodInfo);
        }

        public void AddInvoker(object target, string eventName, MethodInfo methodInfo)
        {
            EventInfo eventInfo = GetEventInfo(eventName);

            AddInvoker(target, eventInfo, methodInfo);
        }

        private void AddInvoker(object target, EventInfo eventInfo, MethodInfo methodInfo)
        {
            if (eventInfo != null && methodInfo != null)
            {
                Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType,
                    target, methodInfo);

                eventInfo.AddEventHandler(GetSource(eventInfo.Name), handler);
            }
        }

        private MethodInfo GetMethodInfo(object source, string methodName)
        {
            return source.GetType().GetMethod(methodName);
        }

        private EventInfo GetEventInfo(string eventName)
        {
            if (_events.ContainsKey(eventName))
                return _events[eventName].GetType().GetEvent(eventName);

            return null;
        }

        private EventInfo GetEventInfo(object source, string eventName)
        {
            return source.GetType().GetEvent(eventName);
        }

        private object GetSource(string eventName)
        {
            return _events[eventName];
        }
        #endregion
    }
}
