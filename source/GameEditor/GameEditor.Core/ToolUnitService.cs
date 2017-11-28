using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEditor.Interfaces;
using GameEditor.Domain;
using System.Windows.Forms;
using GameEditor.Core.Controls;
using ForceField.Core.Managers;
using Microsoft.Xna.Framework;
using ForceField.Interfaces;
using ForceField.Domain.GameLogic;
using ForceField.Domain.Renderer;

namespace GameEditor.Core
{
    public class ToolUnitService : IToolUnitService
    {

        public ToolUnitService(string itemName, Game game)
        {
            name = itemName;
            this.game = game;
            spriteService = game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            input = game.Services.GetService(typeof(IUserInputService)) as IUserInputService;
            tileMapService = game.Services.GetService(typeof(ITileMapService)) as ITileMapService;
            camera = game.Services.GetService(typeof(ICamera2DService)) as ICamera2DService;
            heroService = game.Services.GetService(typeof(IHeroService)) as IHeroService;
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

        public ToolEventCreate GetCreateFunction()
        {
            ToolEventCreate Event = new ToolEventCreate(new EventHandler(CreateFunction), "CreateUnit");
            return Event;
        }

        private void ClickFunction(object sender, EventArgs e)
        {
            UnitForm unitform = new UnitForm(game);

            unitform.Show();
        }


        private void CreateFunction(object sender, EventArgs e)
        {
            if(tileMapService.CurrentTileMap != null)
            {
                TileMap map = tileMapService.CurrentTileMap;
                Point mousePos = input.IsMousePosition();

                Point tilePosition = map.GetTilePosition(new Vector2(mousePos.X, mousePos.Y), camera.Data.Translation);
                heroService.AddHero(heroService.CreateDefaultHero(tilePosition, "Hero", "Hero", Color.White, map));
            }
       }

        private IHeroService heroService;
        private IUserInputService input;
        private ITileMapService tileMapService;
        private ISpriteService spriteService;
        private ICamera2DService camera;
        private Label label;
        private Game game;
        private string name;
    }
}

