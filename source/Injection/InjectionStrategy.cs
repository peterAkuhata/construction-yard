using System;
using System.Collections.Generic;
using System.Text;

namespace Injection
{
    /// <summary>
    /// The core injection strategy interface.  The ConstructionYard holds a collection
    /// of injection strategies which get invoked after the initial creation.
    /// </summary>
    public interface InjectionStrategy
    {
        /// <summary>
        /// Inject an inject
        /// </summary>
        /// <param name="o"></param>
        void Inject(object o);
    }
}
