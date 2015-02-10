using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Injection.Resources;
using Injection;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Globalization;
using SampleCode.Properties;

namespace SampleCode
{
    static class Program
    {
        #region declarations
        // the core object that creates all other objects
        private static ConstructionYard _yard = null;
        #endregion

        #region functions
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // switch to the culture stored in the config file
            ChangeCulture();

            // create the construction yard
            _yard = new ConstructionYard();

            // create the resource injection strategy
            ResourceInjection _injection = new ResourceInjection(GetProvider());

            // add the strategy to injection strategy list
            _yard.AddStrategy(_injection);

            // create an instance of the mainform
            MainForm main = _yard.Services.Add<MainForm>();

            Application.Run(main);
        }

        /// <summary>
        /// Switch to the current culture that is stored in the configuration file.
        /// </summary>
        private static void ChangeCulture()
        {
            try
            {
                // switch to the culture stored in configuration
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.culture);
            }
            catch (Exception)
            {
                // invalid culture, so save the default culture to configuration
                string invalidCulture = Settings.Default.culture;
                Settings.Default.culture = Thread.CurrentThread.CurrentUICulture.Name;
                Settings.Default.Save();

                MessageBox.Show(string.Format("Invalid culture '{0}' has been changed to '{1}'.",
                    invalidCulture, Settings.Default.culture));
            }
        }

        /// <summary>
        /// Return a default resource provider.  This object is added to the list of 
        /// services that the construction yard exposes.
        /// </summary>
        /// <returns></returns>
        private static IResourceProvider GetProvider()
        {
            IResourceProvider provider = new DefaultResourceProvider("SampleResources.Resources",
                Assembly.LoadFile(GetResourceDllFilename()));

            _yard.Services.Add(provider, typeof(IResourceProvider));

            return provider;
        }

        /// <summary>
        /// return the absolute path to the resources dll
        /// </summary>
        /// <returns></returns>
        private static string GetResourceDllFilename()
        {
            return Path.Combine(Application.StartupPath, "SampleResources.dll");
        }
        #endregion
    }
}