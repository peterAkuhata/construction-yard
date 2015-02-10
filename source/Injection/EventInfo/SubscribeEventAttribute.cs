using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Injection
{
    public class SubscribeEventAttribute
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
        public SubscribeEventAttribute(string eventName)
        {
            _eventName = eventName;
        }
        #endregion

        #region Static
        public static SubscribeEventAttribute Get(MethodInfo o)
        {
            return BaseAttribute.Get<SubscribeEventAttribute>(o);
        }

        public static bool Contains(MethodInfo o)
        {
            return (Get(o) != null);
        }
        #endregion
    }
}
