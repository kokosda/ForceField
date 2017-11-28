using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEditor.Interfaces;
using GameEditor.Domain;
using System.Windows.Forms;
using GameEditor.Core.Controls;
using Microsoft.Xna.Framework;

namespace GameEditor.Core
{
    public class ToolMapService : IToolMapService
    {
        public ToolMapService(string itemName, Game game)
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
            MapForm mapForm = new MapForm(game);

            mapForm.Show();
        }

        private Game game;
        private string name;
    }
}

