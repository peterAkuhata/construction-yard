using System;
using System.Collections.Generic;
using System.Text;

namespace Injection
{
    /// <summary>
    /// This is main class in the injection framework.  It's purpose to create ALL the objects
    /// that your application requires, and to inject those objects with any dependancies that
    /// it comes across.
    /// </summary>
    public class ConstructionYard
    {
        #region Declarations
        // the list of injection strategies
        private List<InjectionStrategy> _strategies = new List<InjectionStrategy>();

        // the list of objects that can be injected into other objects
        private InjectionServices _services = null;

        // the object that checks for constructor injection
        private ConstructionInjection _ctor = null;
        #endregion

        #region Properties
        public InjectionServices Services
        {
            get { return _services; }
        }
        #endregion

        #region Constructors
        public ConstructionYard()
        {
            // create the main objects
            _ctor = new ConstructionInjection(this);
            _services = new InjectionServices(this);

            // add the default services
            _services.Add(this);

            // add the default construction strategies
            AddStrategy<PropertyInjection>();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Add an injection strategy to the list of strategies
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public InjectionStrategy AddStrategy<T>()
        {
            return AddStrategy(typeof(T));
        }

        /// <summary>
        /// Add an injection strategy to the list of strategies
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public InjectionStrategy AddStrategy(Type t)
        {
            InjectionStrategy s = (InjectionStrategy)Build(t);

            if (s != null)
                AddStrategy(s);

            return s;
        }

        /// <summary>
        /// Add an injection strategy to the list of strategies
        /// </summary>
        /// <param name="s"></param>
        public void AddStrategy(InjectionStrategy s)
        {
            _strategies.Add(s);
        }

        /// <summary>
        /// Build an object given a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Build<T>()
        {
            return (T)Build(typeof(T));
        }

        /// <summary>
        /// Build an object given a type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public object Build(Type t)
        {
            // use constructor injection to create the object
            object o = _ctor.Construct(t);

            foreach (InjectionStrategy strategy in _strategies)
            {
                // inject each strategy into the object
                strategy.Inject(o);
            }

            // return the fully-injected object to the caller
            return o;
        }
        #endregion
    }
}
