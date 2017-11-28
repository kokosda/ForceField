using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameEditor.Interfaces;
using GameEditor.Domain;

namespace GameEditor.Core
{
    public class ToolPanelService : IToolPanelService
    {
        public Form MainForm
        {
            get
            {
                return (Form)Control.FromHandle(handle);
            }
        }

        public ToolPanelService(IList<ToolStripMenuItem> controls, IntPtr handle)
        {
            this.handle = handle;
            this.controls = controls;
        }

        public void AddComponent(ToolStripMenuItem item)
        {
            component = item.Text;
            controls.Add(item);
        }

        public void SetCurrentComponent(string component)
        {
            this.component = component;
        }

        public void AddMenuItem(ToolStripMenuItem item)
        {
            ToolStripMenuItem menuItem = controls.First(p => p.Text == component);

            item.Click += new EventHandler(DefaultClick);

            menuItem.DropDownItems.AddRange(new ToolStripItem[] { item });
        }

        public void SetEventForItem(ToolEventClick toolEvent)
        {
            ToolStripMenuItem componentItems = controls.First(p => p.Text == component);
            ToolStripItem item = null;

            for (int i = 0; i < componentItems.DropDownItems.Count; ++i)
            {
                if (componentItems.DropDownItems[i].Text == toolEvent.ItemName)
                {
                    item = componentItems.DropDownItems[i];
                    i = componentItems.DropDownItems.Count;
                }
            }

            if (item != null)
            {
                item.Click += new EventHandler(toolEvent.Click);
            }
        }

        public void SetControls()
        {
            Form form = Control.FromHandle(handle) as Form;

            ToolStripMenuItem[] array = controls.ToArray();

            ToolStrip strip = new ToolStrip(array);

            form.Controls.Add(strip);
        
        }

        private void DefaultClick(object sender, EventArgs e)
        {

        }

        private string component;
        private IList<ToolStripMenuItem> controls;
        private IntPtr handle;
    }
}
