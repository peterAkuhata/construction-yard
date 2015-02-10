using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Injection
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Property)]
    public class InjectionServiceRequestAttribute
        :BaseAttribute
    {
        #region Static
        public static bool Contains(ConstructorInfo o)
        {
            return (Get(o) != null);
        }

        public static bool Contains(PropertyInfo o)
        {
            return (Get(o) != null);
        }

        public static InjectionServiceRequestAttribute Get(ConstructorInfo o)
        {
            return BaseAttribute.Get<InjectionServiceRequestAttribute>(o);
        }

        public static InjectionServiceRequestAttribute Get(PropertyInfo o)
        {
            return BaseAttribute.Get<InjectionServiceRequestAttribute>(o);
        }
        #endregion
    }
}
