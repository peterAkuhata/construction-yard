using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Injection
{
    [RequiresConstruction]
    public class EventInjection
        :InjectionStrategy
    {
        #region Declarations
        private ConstructionYard _yard = null;
        #endregion

        #region Constructors
        [InjectionServiceRequest]
        public EventInjection(ConstructionYard yard)
        {
            _yard = yard;
        }
        #endregion

        #region InjectionStrategy Members

        public void Inject(object o)
        {
            PublishedEvents events = _yard.Services.Get<PublishedEvents>();

            foreach (EventInfo eventInfo in o.GetType().GetEvents())
            {
                Trace.WriteLine("Event: " + eventInfo.Name);

                if (PublishEventAttribute.Contains(eventInfo))
                    events.AddEvent(o, eventInfo.Name);
            }

            foreach (MethodInfo methodInfo in o.GetType().GetMethods())
            {
                Trace.WriteLine("Method: " + methodInfo.Name);

                SubscribeEventAttribute s = SubscribeEventAttribute.Get(methodInfo);

                if (s != null)
                    events.AddInvoker(o, s.EventName, methodInfo); 
            }
        }

        #endregion
    }
}
