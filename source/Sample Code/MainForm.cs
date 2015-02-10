using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Injection;
using Injection.Resources;

namespace SampleCode
{
    // this attribute tells the construction yard to inject the form
    [RequiresConstruction] 
    public partial class MainForm : Form
    {
        #region declarations
        // an example of an injected dependancy
        private IResourceProvider _provider = null;
        #endregion

        #region ctor
        /// <summary>
        /// this attribute tells the construction yard to use this constructor
        /// to create the object.  the construction yard looks in it's list of
        /// services for each parameter.
        /// </summary>
        /// <param name="provider"></param>
        [InjectionServiceRequest]
        public MainForm(IResourceProvider provider)
            : this()
        {
            _provider = provider;
        }

        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region functions
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // default the combobox to the current culture
            cultureCombobox.Text = Properties.Settings.Default.culture;
        }
        #endregion

        #region events
        private void saveButton_Click(object sender, EventArgs e)
        {
            // save the properties
            Properties.Settings.Default.culture = cultureCombobox.Text;
            Properties.Settings.Default.Save();

            // use the resource provider to display a localized string
            MessageBox.Show(_provider.GetString("Culture saved."));
        }
        #endregion
    }
}