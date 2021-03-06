﻿using Microsoft.VisualStudio.TestTools.UITesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace Syncfusion.VisualStudio.TestTools.UITest.SfGridExtension
{
    /// <summary>
    /// To provide custom properties for TreeGridCell.
    /// </summary>
    public class SfTreeGridCellPropertyProvider : IUIControlPropertyProvider
    {
        #region Get the custom Properties

        private Dictionary<string, UITestPropertyDescriptor> treeGridCellProperties;
        public Dictionary<string, UITestPropertyDescriptor> TreeGridCellProperties
        {
            get
            {
                if (treeGridCellProperties == null)
                {
                    treeGridCellProperties = GetUIControlPropertiesMap();
                }
                return treeGridCellProperties;
            }
        }

        #endregion

        /// <summary>
        /// Get the type of control
        /// </summary>
        public Type SpecializedType
        {
            get
            {
                return typeof(WpfSfTreeGridCell);
            }
        }

        /// <summary>
        /// Invokes to get the list of UIControl proeprties
        /// </summary>
        /// <returns>Dictionary<string, UITestPropertyDescriptor></returns>
        public Dictionary<string, UITestPropertyDescriptor> GetUIControlPropertiesMap()
        {
            Dictionary<string, UITestPropertyDescriptor> dictionary = new Dictionary<string, UITestPropertyDescriptor>();
            dictionary.Add(WpfSfTreeGridCell.PropertyNames.CellValue, new UITestPropertyDescriptor(typeof(string), UITestPropertyAttributes.Readable));
            dictionary.Add(WpfSfTreeGridCell.PropertyNames.FormattedValue, new UITestPropertyDescriptor(typeof(string), UITestPropertyAttributes.Readable));
            dictionary.Add(WpfSfTreeGridCell.PropertyNames.RowIndex, new UITestPropertyDescriptor(typeof(int), UITestPropertyAttributes.Readable));
            dictionary.Add(WpfSfTreeGridCell.PropertyNames.ColumnIndex, new UITestPropertyDescriptor(typeof(int), UITestPropertyAttributes.Readable));
            dictionary.Add(WpfSfTreeGridCell.PropertyNames.ColumnName, new UITestPropertyDescriptor(typeof(string), UITestPropertyAttributes.Readable));
            dictionary.Add(WpfSfTreeGridCell.PropertyNames.HeaderText, new UITestPropertyDescriptor(typeof(string), UITestPropertyAttributes.Readable));
            return dictionary;
        }

        /// <summary>
        /// Get the Property value
        /// </summary>
        /// <param name="uiTestControl">UITestControl</param>
        /// <param name="propertyName">PropertyName</param>
        /// <returns>object</returns>
        public object GetUIControlPropertyValue(UITestControl uiTestControl, string propertyName)
        {
            if (propertyName == "FriendlyName")
            {
                AutomationElement automationElement = uiTestControl.NativeElement as AutomationElement;
                AutomationElement.AutomationElementInformation current = automationElement.Current;
                if (current.ItemStatus.Equals(string.Empty))
                {
                    throw new NotSupportedException();
                }
                AutomationElement.AutomationElementInformation current2 = automationElement.Current;
                string[] array = current2.ItemStatus.Split(new string[]
                {
                    "#"
                }, StringSplitOptions.RemoveEmptyEntries);

                return array[0];
            }
            else if (propertyName == "Name")
                return null;
            else
                return PropertyProviderHelper.GetUIControlPropertyValue(uiTestControl, propertyName, TreeGridCellProperties);

        }

        /// <summary>
        /// Set the PropertyValue
        /// </summary>
        /// <param name="uiTestControl">UiTestControl</param>
        /// <param name="propertyName">PropertyName</param>
        /// <param name="value">value</param>
        public void SetUIControlPropertyValue(UITestControl uiTestControl, string propertyName, object value)
        {
            if (uiTestControl != null)
            {
                uiTestControl.SetProperty(propertyName, value);
            }
        }
    }
}
