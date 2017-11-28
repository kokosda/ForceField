using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameEditor.Interfaces;
using System.Windows.Forms;
using ForceField.Interfaces;
namespace GameEditor.Core.Manager
{
    public class ToolPanelManager : IGameComponent
    {
        public ToolPanelManager(Game game) 
        {
            this.game = game;


            game.Components.Add(this);


            toolPanelService = new ToolPanelService(new List<ToolStripMenuItem>(),game.Window.Handle);
            toolUnitService = new ToolUnitService("Юнит",game);
            game.Services.AddService(typeof(IToolUnitService), toolUnitService);
            toolMapService = new ToolMapService("Карта", game);
            game.Services.AddService(typeof(IToolMapService), toolMapService);
            toolCameraService = new ToolCameraService("Камера", game);
            game.Services.AddService(typeof(IToolCameraService), toolCameraService);
            toolScriptService = new ToolScriptService(game,"Скрипт");
            #region Инициализация элементов панели инструментов
            toolPanelService.AddComponent(new ToolStripMenuItem("Инструменты"));
            toolPanelService.AddMenuItem(toolUnitService.GetItem());
            toolPanelService.SetEventForItem(toolUnitService.GetClickFunction());
            toolPanelService.AddMenuItem(toolMapService.GetItem());
            toolPanelService.SetEventForItem(toolMapService.GetClickFunction());
            toolPanelService.AddMenuItem(toolCameraService.GetItem());
            toolPanelService.SetEventForItem(toolCameraService.GetClickFunction());
            toolPanelService.AddMenuItem(toolScriptService.GetItem());
            toolPanelService.SetEventForItem(toolScriptService.GetClickFunction());
            toolPanelService.SetControls();
            #endregion
        }


        public void Initialize()
        {

        }

        private IToolScriptService toolScriptService;
        private IToolMapService toolMapService;
        private IToolUnitService toolUnitService;
        private IToolCameraService toolCameraService;
        private IToolPanelService toolPanelService;
        private Game game;
    }
}
