using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Injection
{
    public class ConstructionInjection
    {
        #region Declarations
        private ConstructionYard _yard = null;
        #endregion

        #region Constructors
        internal ConstructionInjection(ConstructionYard yard)
        {
            _yard = yard;
        }
        #endregion

        #region Functions
        public object Construct(Type t)
        {
            object value = null;

            if (RequiresConstructionAttribute.Contains(t))
            {
                ConstructorInfo[] constructors = t.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

                foreach (ConstructorInfo info in constructors)
                {
                    if (InjectionServiceRequestAttribute.Contains(info))
                    {
                        value = Construct(t, info);

                        if (value != null)
                            break;
                    }
                }
            }

            return Construct(t, value);
        }

        private object Construct(Type t, object value)
        {
            if (value == null)
                return Activator.CreateInstance(t);

            else
                return value;
        }

        private object Construct(Type t, ConstructorInfo info)
        {
            List<object> realParams = new List<object>();
            ParameterInfo[] parameters = info.GetParameters();
            bool canConstruct = true;

            foreach (ParameterInfo parameter in parameters)
            {
                object value = _yard.Services.Get(parameter.ParameterType);

                if (value == null)
                {
                    canConstruct = false;
                    break;
                }
                else
                {
                    realParams.Add(value);
                }
            }

            if (canConstruct)
                return info.Invoke(realParams.ToArray());

            return null;
        }
        #endregion
    }
}
