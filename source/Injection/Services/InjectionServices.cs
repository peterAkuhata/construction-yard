using System;
using System.Collections.Generic;
using System.Text;

namespace Injection
{
    /// <summary>
    /// This class represents a collection of objects that are available for injection
    /// into any dependant objects.  It is used by the construction yard.
    /// </summary>
    public class InjectionServices
    {
        #region Declarations
        // list of injected objects
        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        // the construction yard is used to create object if they don't exist in the collection
        private ConstructionYard _yard = null;
        #endregion

        #region Constructors
        internal InjectionServices(ConstructionYard yard)
        {
            _yard = yard;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Return an object given it's type.  If it doesn't exist, then create it and add it first.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        /// <summary>
        /// Return an object given it's type.  If it doesn't exist, then create it and add it first.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public object Get(Type t)
        {
            if (!_services.ContainsKey(t))
                Add(t);

            return _services[t];
        }

        /// <summary>
        /// Create an object given it's type, and add it to the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Add<T>()
        {
            return (T)Add(typeof(T));
        }

        /// <summary>
        /// Create an object given it's type, and add it to the list
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public object Add(Type t)
        {
            object service = _yard.Build(t);

            Add(service);

            return service;
        }

        /// <summary>
        /// Add the object to the list of objects
        /// </summary>
        /// <param name="service"></param>
        public void Add(object service)
        {
            Add(service, service.GetType());
        }

        /// <summary>
        /// Add the object to the list of objects
        /// </summary>
        /// <param name="service"></param>
        /// <param name="type"></param>
        public void Add(object service, Type type)
        {
            if (!_services.ContainsKey(type))
                _services.Add(type, service);
        }
        #endregion
    }
}
