using System;
using System.Collections.Generic;
using System.Text;
using Injection;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Drawing;

namespace Injection.Resources
{
    /// <summary>
    /// This is an example of an injection strategy.  An injection strategy allows you to
    /// define what resources need to injected, and how.  In this case, the injection strategy
    /// searches for controls in order to set the .Text property, and also for forms to set the 
    /// .Icon property.
    /// </summary>
    public class ResourceInjection
        :InjectionStrategy
    {
        #region Declarations
        // the resource provider to use during injection
        private IResourceProvider _provider = null;
        #endregion

        #region Constructors
        public ResourceInjection(IResourceProvider provider)
        {
            _provider = provider;
        }
        #endregion

        #region InjectionStrategy Members
        /// <summary>
        /// This is the main injection method.  It gets invoked when the application
        /// calls the ConstructionYard.Build() method.
        /// </summary>
        /// <param name="o"></param>
        public void Inject(object o)
        {
            if (o is Control && _provider != null)
            {
                InjectCore(o as Control);
            }
        }

        /// <summary>
        /// Inject resources into the control, and recurse through child controls
        /// </summary>
        /// <param name="o"></param>
        protected virtual void InjectCore(Control o)
        {
            BuildResources(o);

            foreach (Control child in o.Controls)
            {
                InjectCore(child);
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Set the text of the control and if possible, set the icon of the form
        /// </summary>
        /// <param name="o"></param>
        protected void BuildResources(Control o)
        {
            string text = _provider.GetString(o.Text);

            if (text != string.Empty && text != o.Text)
            {
                o.Text = text;
            }

            if (o is Form)
            {
                Form f = (Form)o;

                Icon c = _provider.GetIcon(o.Name + ".Icon");

                if (c != null)
                    f.Icon = c;
            }
        }
        #endregion
    }
}
