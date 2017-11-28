using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ForceField.WorkflowControls
{
    public partial class ServicePage : TabPage
    {
        public virtual void OnFormLoad(object sender, EventArgs e)
        {
        }

        public T GetChildControl<T>(string key, bool searchAllChildren = true) where T: Control
        {
            var co = Controls.Find(key, searchAllChildren).First() as T;

            return co;
        }

        public void AssignChildControlText<T>(string key, string text, bool searchAllChildren = true) where T : Control
        {
            var co = Controls.Find(key, searchAllChildren).First() as T;

            co.Text = text;
        }

        public void AssignDataSourceToComboBox(string[] values, string comboBoxName)
        {
            var comboBox = GetChildControl<ComboBox>(comboBoxName);

            comboBox.DataSource = null;
            comboBox.DataSource = values;
        }
    }
}
