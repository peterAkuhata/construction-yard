using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Injection
{
    public class BaseAttribute
        :Attribute
    {
        #region Static
        public static T Get<T>(MethodInfo o)
            where T : BaseAttribute
        {
            object[] attributes = o.GetCustomAttributes(typeof(T), false);

            if (attributes != null && attributes.Length == 1)
                return (T)attributes[0];

            return null;
        }

        public static T Get<T>(PropertyInfo o)
            where T : BaseAttribute
        {
            object[] attributes = o.GetCustomAttributes(typeof(T), false);

            if (attributes != null && attributes.Length == 1)
                return (T)attributes[0];

            return null;
        }

        public static T Get<T>(EventInfo o)
            where T : BaseAttribute
        {
            object[] attributes = o.GetCustomAttributes(typeof(T), false);

            if (attributes != null && attributes.Length == 1)
                return (T)attributes[0];

            return null;
        }

        public static T Get<T>(ConstructorInfo o)
            where T : BaseAttribute
        {
            object[] attributes = o.GetCustomAttributes(typeof(T), false);

            if (attributes != null && attributes.Length == 1)
                return (T)attributes[0];

            return null;
        }

        public static T Get<T>(object o)
            where T : BaseAttribute
        {
            object[] attributes = o.GetType().GetCustomAttributes(typeof(T), false);

            if (attributes != null && attributes.Length == 1)
                return (T)attributes[0];

            return null;
        }
        #endregion
    }
}
