using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Injection
{
    /// <summary>
    /// This attribute is used by the injection framework to check whether it needs
    /// to take the time to inject a given type object or not.  Add this attribute
    /// to your classes.
    /// </summary>
    public class RequiresConstructionAttribute
        :BaseAttribute
    {
        #region Static
        public static RequiresConstructionAttribute Get(MethodInfo o)
        {
            return BaseAttribute.Get<RequiresConstructionAttribute>(o);
        }

        public static bool Contains(Type t)
        {
            object[] attributes = t.GetCustomAttributes(
                typeof(RequiresConstructionAttribute), false);

            return (attributes != null && attributes.Length == 1);
        }
        #endregion
    }
}
