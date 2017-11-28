using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameEditor.Domain;
 

namespace GameEditor.Interfaces
{
    public interface IToolPanelService
    {
        Form MainForm { get; }

        void AddComponent(ToolStripMenuItem item);
        void AddMenuItem(ToolStripMenuItem item);
        void SetEventForItem(ToolEventClick toolEvent);
        void SetControls();
        void SetCurrentComponent(string component);
    }
}
