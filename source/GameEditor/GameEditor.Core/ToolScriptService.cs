using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEditor.Interfaces;
using System.Windows.Forms;
using GameEditor.Domain;
using GameEditor.Core.Controls;
using Microsoft.Xna.Framework;

namespace GameEditor.Core
{
    public class ToolScriptService : IToolScriptService
    {
        public ToolScriptService(Game game, string itemName)
        {
            name = itemName;
            this.game = game;
        }

        public ToolEventClick GetClickFunction()
        {
            ToolEventClick toolEvent = new ToolEventClick();
            toolEvent.Click = new ToolEventClick.ClickEvent(ClickFunction);
            toolEvent.ItemName = name;
            return toolEvent;
        }

        public ToolStripMenuItem GetItem()
        {
            return new ToolStripMenuItem(name);
        }

        private void ClickFunction(object sender, EventArgs e)
        {
            ScriptForm scriptForm = new ScriptForm(game);

            scriptForm.Show();
        }

        private Game game;
        private string name;
    }
}

