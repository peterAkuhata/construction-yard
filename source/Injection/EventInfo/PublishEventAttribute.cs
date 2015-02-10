using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Injection
{
    [AttributeUsage(AttributeTargets.Event)]
    public class PublishEventAttribute
        :BaseAttribute
    {
        #region Declarations
        private string _eventName = string.Empty;
        #endregion

        #region Properties
        public string EventName
        {
            get { return _eventName; }
        }
        #endregion

        #region Constructors
        public PublishEventAttribute(string eventName)
        {
            _eventName = eventName;
        }
        #endregion

        #region Static
        public static PublishEventAttribute Get(EventInfo o)
        {
            return BaseAttribute.Get<PublishEventAttribute>(o);
        }

        public static bool Contains(EventInfo o)
        {
            return (Get(o) != null);
        }
        #endregion
    }
}
