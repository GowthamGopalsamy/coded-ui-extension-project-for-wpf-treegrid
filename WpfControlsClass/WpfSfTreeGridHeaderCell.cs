﻿using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.VisualStudio.TestTools.UITest.SfGridExtension
{
    /// <summary>
    /// Provides ability to locate TreeGridHeaderCell in UI 
    /// </summary>
    public class WpfSfTreeGridHeaderCell : WpfControl
    {
        #region abstract class
        public new abstract class PropertyNames
        {
            public static readonly string ColumnName = "ColumnName";
            public static readonly string HeaderText = "HeaderText";
            public static readonly string SortDirection = "SortDirection";
            public static readonly string SortNumberVisibility = "SortNumberVisibility";
        }
        #endregion

        #region ctor

        public WpfSfTreeGridHeaderCell()
        {
        }
        public WpfSfTreeGridHeaderCell(UITestControl control)
            : base(control)
        {
        }

        #endregion

        #region private fields

        private string columnName;
        private string headerText;
        private string sortDirection;
        private string sortNumberVisibility;
        private bool isFound = false;

        #endregion

        #region public properties

        /// <summary>
        /// Gets or Sets Column Name
        /// </summary>
        public virtual string ColumnName
        {
            get
            {
                if (!this.isFound)
                    this.Find();
                return columnName;
            }
            set
            {
                columnName = value;
            }

        }

        /// <summary>
        /// Gets or Sets Header Text of TreeGridColumn
        /// </summary>
        public virtual string HeaderText
        {
            get
            {
                if (!this.isFound)
                    this.Find();
                return headerText;
            }
            set
            {
                headerText = value;
            }

        }

        /// <summary>
        /// Gets or Sets sort Direction
        /// </summary>
        public virtual string SortDirection
        {
            get
            {
                if (!this.isFound)
                    this.Find();
                return sortDirection;
            }
            set
            {
                sortDirection = value;
            }

        }

        /// <summary>
        /// Gets or Sets sort Direction
        /// </summary>
        public virtual string SortNumberVisibility
        {
            get
            {
                if (!this.isFound)
                    this.Find();
                return sortDirection;
            }
            set
            {
                sortDirection = value;
            }

        }

        #endregion

        #region override methods
        /// <summary>
        /// To Find TreeGridHeaderCell.
        /// </summary>
        public override void Find()
        {
            //create UITestControl from current instance
            UITestControl control = new UITestControl();
            control.CopyFrom(this);
            //get the search properties from grid cell instance
            var properties = control.SearchProperties.ToList();

            var headerCellProperties = new List<string>();
            headerCellProperties.Add("ColumnName");
            headerCellProperties.Add("HeaderText");
            headerCellProperties.Add("SortDirection");
            headerCellProperties.Add("SortNumberVisibility");

            bool isContainProperty = false;
            foreach (var property in properties)
            {
                if (headerCellProperties.Contains(property.PropertyName))
                {
                    isContainProperty = true;
                    break;
                }
            }

            if (isContainProperty)
            {
                // throw exception if properties are not enough to search a control
                if (properties.Count < 1 || properties.Where(p => p.PropertyName == "ColumnName").Count() < 1)
                    throw new UITestException("Not Enough Properties to find a control");

                // get the collection of child elements from  sftreeGrid container
                UITestControlCollection rowCollection = control.Container.GetChildren();
                //bool flag = false;
                foreach (UITestControl row in rowCollection)
                {
                    if (row.ClassName == "Uia.TreeGridHeaderRowControl")
                    {
                        UITestControlCollection collection = row.GetChildren();

                        foreach (UITestControl child in collection)
                        {
                            // check child is grid cell
                            if (child.ClassName == "Uia.TreeGridHeaderCell")
                            {
                                Dictionary<string, string> childPropertiesDictonary = new Dictionary<string, string>();
                                System.Windows.Automation.AutomationElement automationElement = child.NativeElement as System.Windows.Automation.AutomationElement;
                                if (automationElement == null)
                                    continue;
                                System.Windows.Automation.AutomationElement.AutomationElementInformation current2 = automationElement.Current;
                                string[] array = current2.ItemStatus.Split(new string[]
                                            {
                                                "#"
                                            }, StringSplitOptions.None);

                                // assign the properties values to corresponding variable for condition checking
                                childPropertiesDictonary.Add("ColumnName", array[0]);
                                childPropertiesDictonary.Add("HeaderText", array[1]);
                                childPropertiesDictonary.Add("SortDirection", array[2]);
                                childPropertiesDictonary.Add("SortNumberVisibility", array[3]);

                                // set flag to true if  all property values are match to  child control                      
                                foreach (var item in properties)
                                {
                                    if (childPropertiesDictonary.Keys.Contains(item.PropertyName))
                                    {
                                        if (childPropertiesDictonary[item.PropertyName] == item.PropertyValue)
                                            isFound = true;
                                        else
                                        {
                                            isFound = false;
                                            break;
                                        }
                                    }

                                }

                                // here this condition check is  used  for finding exact gridcell from its given proerty values
                                if (isFound)
                                {
                                    //set the  corresponding cell values to its properties.
                                    this.columnName = array[0];
                                    this.headerText = array[1];
                                    this.sortDirection = array[2];
                                    this.sortNumberVisibility = array[3];
                                    // copy the  found cell as this  instance
                                    this.CopyFrom(child);

                                    childPropertiesDictonary = null;
                                    break;
                                }
                            }
                        }
                        if (isFound)
                            break;
                    }
                }
                if (!isFound)
                    throw new UITestException("Not Maching control for this sepecific property");
            }
            else
            {
                base.Find();
                System.Windows.Automation.AutomationElement automationElement = this.NativeElement as System.Windows.Automation.AutomationElement;
                if (automationElement == null)
                    throw new UITestException("Not Maching control for this sepecific property");
                System.Windows.Automation.AutomationElement.AutomationElementInformation current2 = automationElement.Current;
                string[] array = current2.ItemStatus.Split(new string[]
                                            {
                                                "#"
                                            }, StringSplitOptions.None);
                if (array.Count() != 0)
                {
                    this.columnName = array[0];
                    this.headerText = array[1];
                    this.sortDirection = array[2];
                    this.sortNumberVisibility = array[3];
                }
                else
                {
                    throw new UITestException("Not Maching control for this sepecific property");
                }
            }
        }
        #endregion

        /// <summary>
        /// Method to find specific TreeGridHeaderCell.
        /// </summary>
        /// <returns>true, if cell is found, otherwise false.</returns>
        public new bool TryFind()
        {
            try
            {
                this.Find();
                return true;
            }
            catch (UITestException)
            {
                return false;
            }

        }
    }
}
