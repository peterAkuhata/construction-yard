using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Injection
{
    /// <summary>
    /// This is an example of an injection strategy.  This strategy checks for properties
    /// with the <see cref="InjectionServiceRequest">InjectionServiceRequest</see> tagged
    /// onto the property.  If it is found, then that property gets injected with the datatype
    /// that was found./>
    /// </summary>
    [RequiresConstruction]
    public class PropertyInjection
        :InjectionStrategy
    {
        #region Declarations
        // the construction yard is needed to build objects if not found in the list of serviced objects
        private ConstructionYard _yard = null;
        #endregion

        #region Constructors
        [InjectionServiceRequest]
        public PropertyInjection(ConstructionYard yard)
        {
            _yard = yard;
        }
        #endregion

        #region IConstructionStrategy Members
        /// <summary>
        /// Use reflection to get the list of properties, and then check each one for the
        /// InjectionServiceRequest interface
        /// </summary>
        /// <param name="o"></param>
        public void Inject(object o)
        {
            PropertyInfo[] properties = o.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (InjectionServiceRequestAttribute.Contains(prop))
                {
                    // a property has requested injection, so find the object
                    object propertyValue = _yard.Services.Get(prop.PropertyType);

                    if (o != null)
                    {
                        // the object was found, so set it's value
                        prop.SetValue(o, propertyValue, null);
                    }
                }
            }
        }
        #endregion
    }
}
