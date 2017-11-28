using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameEditor.Domain;
using GameEditor.Interfaces;
using System.Windows.Forms;
using GameEditor.Core.Controls;

namespace GameEditor.Core
{
    public class ToolCameraService : IToolCameraService
    {
        public ToolCameraService(string itemName, Game game)
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
            CameraForm cameraForm = new CameraForm(game);

            cameraForm.Show();
        }

        private Game game;
        private string name;
    }
}
