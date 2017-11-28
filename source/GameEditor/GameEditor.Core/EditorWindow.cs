using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForceField.Interfaces;
using ForceField.Domain.GameLogic;
using GameEditor.Core.Controls;

using ForceField.UI.UIComponentServices;
using ForceField.UI.UIElements;
using System.Windows.Forms;
using ForceField.Domain.Input;
using GameEditor.Interfaces;

namespace GameEditor.Core
{
    public class EditorWindow : DrawableGameComponent
    {
        public EditorWindow(Game game) : base(game)
        {
            game.Components.Add(this);
            tileMapService = game.Services.GetService(typeof(ITileMapService)) as ITileMapService;
            input = game.Services.GetService(typeof(IUserInputService)) as IUserInputService;
            camera = game.Services.GetService(typeof(ICamera2DService)) as ICamera2DService;
            guiService = game.Services.GetService(typeof(IGUIService)) as IGUIService;
            spriteService = game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            heroService = game.Services.GetService(typeof(IHeroService)) as IHeroService;
            toolUnitService = game.Services.GetService(typeof(IToolUnitService)) as IToolUnitService;
            contextMenu = new ContextMenu();

            contextMenu.MenuItems.Add(new MenuItem("Создать юнита",toolUnitService.GetCreateFunction().Event));
            contextMenu.MenuItems.Add(new MenuItem("Установить триггер"));
            contextMenu.MenuItems.Add(new MenuItem("Установить текстуру"));
        }

        protected override void LoadContent()
        {
            labelService = new UILabelService(base.Game);
            labelService.AddComponent(tilePos = new ForceField.UI.UIElements.Label(spriteService.GetSpriteText("menu"), new Vector2(10, 30), ""));
            tilePos.SpriteText.Scale = 0.6f;
            guiService.AddComponentService(labelService);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {          
            input.Update();
            Point mousePos = input.IsMousePosition();
            #region Движение камеры
            if (input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.W))
            {
                camera.MoveToTop();
            }
            else
            if (input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.S))
            {
                camera.MoveToBottom();
            }
            else
            if (input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.D))
            {
                camera.MoveToRight();
            }
            else
            if (input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.A))
            {
                camera.MoveToLeft();
            }
            #endregion
            #region Выделение текущего тайла
            

            Vector2 p = new Vector2(mousePos.X, mousePos.Y);

            if (tileMapService.CurrentTileMap != null)
            {
                #region Контекстное меню
                if (input.IsMouseButtonPressed(MouseButton.RightButton))
                {
                    contextMenu.Show(Control.FromHandle(base.Game.Window.Handle), new System.Drawing.Point(input.IsMousePosition().X, input.IsMousePosition().Y));
                }
                #endregion
                lastTile = tile;

                Point tileCoord = tileMapService.CurrentTileMap.GetTilePosition(p, camera.Data.Translation);
                tilePos.Text = "X: " + tileCoord.X + " Y:" + tileCoord.Y;

                tile = tileMapService.CurrentTileMap.GetTile(p, camera.Data.Translation);

                if (tile == null)
                {
                    if (lastTile != null)
                    {
                        lastTile.Sprite.Color = Color.White;
                    }
                }

                if (tile != null)
                {
                    if (lastTile != null)
                    {
                        lastTile.Sprite.Color = Color.White;
                    }
                    tile.Sprite.Color = Color.Coral;
                }
            }
            else
            {
                tilePos.Text = "";
            }
            #endregion
            tileMapService.Update(gameTime);
            heroService.Update(gameTime);
            guiService.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            tileMapService.Draw(gameTime);
            heroService.Draw(gameTime);
            guiService.Draw(gameTime);
            base.Draw(gameTime);
        }

        IToolUnitService toolUnitService;
        IHeroService heroService;
        UILabelService labelService;
        ForceField.UI.UIElements.Label tilePos; 
        ICamera2DService camera;
        IUserInputService input;
        ITileMapService tileMapService;
        IGUIService guiService;
        ISpriteService spriteService;
        Tile tile;
        Tile lastTile;
        ContextMenu contextMenu;
    }
}
