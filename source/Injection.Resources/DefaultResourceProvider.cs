using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Drawing;

namespace Injection.Resources
{
    /// <summary>
    /// This class is a default resource provider that implements the <see cref="IResourceProvider">IResourceProvider</see>
    /// interface.  It uses the default .Net Framework ResourceManager object to manage resources./>
    /// </summary>
    public class DefaultResourceProvider
        : IResourceProvider
    {
        #region declarations
        // .net framework resource manager object
        private ResourceManager _resources = null;
        #endregion

        #region ctor
        public DefaultResourceProvider(string baseName, Assembly assembly)
        {
            _resources = new ResourceManager(baseName, assembly);
        }
        #endregion

        #region IResourceProvider Members
        /// <summary>
        /// Return a string value given the resource name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetString(string name)
        {
            string value = _resources.GetString(name);

            if (value == null)
                value = name;

            return value;
        }

        /// <summary>
        /// Return an icon object given the resource name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Icon GetIcon(string name)
        {
            return (Icon)_resources.GetObject(name);
        }
        #endregion
    }
}
