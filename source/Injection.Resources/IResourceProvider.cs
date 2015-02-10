using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Injection.Resources
{
    /// <summary>
    /// Simple resource interface to handle resource management
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Get a string value given the resource name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetString(string name);

        /// <summary>
        /// Get an icon object given the resource name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Icon GetIcon(string name);
    }
}
