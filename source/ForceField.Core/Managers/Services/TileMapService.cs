using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForceField.Domain.GameLogic;
using ForceField.Interfaces;
using ForceField.Domain.Renderer;
using ForceField.Core.Services;

namespace ForceField.Core.Managers.Services
{
    public class TileMapService : ITileMapService
    {
        public TileMapService(IList<TileMap> units, UnitsManager unitsManager)
        {
            Units = units;
            this.unitsManager = unitsManager;
            spriteBatchService = unitsManager.Game.Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
            spriteService = unitsManager.Game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            cameraService = unitsManager.Game.Services.GetService(typeof(ICamera2DService)) as ICamera2DService;
            animationService = unitsManager.Game.Services.GetService(typeof(IAnimationService)) as IAnimationService;
            CurrentTileMap = null;
        }

        #region Properties

        public IList<TileMap> Units { get; set; }

        public TileMap CurrentTileMap { get { return currentTileMap; } set { currentTileMap = value; } }         // предположение: в любой момент времени, будет рисоваться только одна тайловая карта

        #endregion

        public void CreateUnitsFromServiceByResourcePath(string templatePath)
        {
        }

        public void ConstructInitialTileMaps()
        {
           // TileMap defaultBattleTileMap = new TileMap();

            //defaultBattleTileMap.Name = "Default_battle_tilemap";
            //defaultBattleTileMap.CellSize = new Point(64, 32);
            //defaultBattleTileMap.Position = new Vector2(0, 0);
            //defaultBattleTileMap = CreateTileMap(
            //new Point(64,32), 
            //new Vector2(0,0), 
            //new Point(64,32), 
            //spriteService.GetByName("Default_battle_tile"), 
            //"Default_battle_tilemap");
            //defaultBattleTileMap = CreateTileMap(new Point(64, 32), new Vector2(0, 0), new Point(15, 30), spriteService.GetByName("Tile"), "Default_battle_tilemap");
            //Units.Add(defaultBattleTileMap);

            //CurrentTileMap = CreateTileMap(new Point(64, 32), new Vector2(0, 0), new Point(1,1), spriteService.GetByName("Tile"), "GlobalMap");
        }

        public int GetCurrentTile(Vector2 location)
        {

            return 0;
        }

        public Unit GetRandom()
        {
            throw new NotImplementedException();
        }

        public Texture2D GetFirstTexture(TileMap unit)
        {
            throw new NotImplementedException();
        }

        public void SetSpriteService(ISpriteService spriteService)
        {

        }

        public TileMap CreateTileMap(Point cellSize, Vector2 position, Point size, Sprite sprite, string name)
        {
            TileMap tileMap = new TileMap();
            tileMap.Name = name;
            tileMap.Position = position;
            tileMap.Size = size;
            tileMap.CellSize = cellSize;
            float X, Y;
            int HalfOffsetX = -cellSize.X / 2;
            int HalfOffsetY = cellSize.Y / 2;
            for (int j = 0; j < size.X; ++j)
            {
                int LineOffsetX = -j * HalfOffsetX, LineOffsetY = j * HalfOffsetY;
                for (int i = 0; i < size.Y; ++i)
                {
                    X = position.X + LineOffsetX + i * HalfOffsetX;
                    Y = position.Y + LineOffsetY + i * HalfOffsetY;
                    var unit = new Tile();
                    unit.Sprite = Sprite.Clone(sprite);
                    unit.Sprite.Color = new Color(255, 255, 255, 255);
                    unit.Sprite.Scale = 1f;
                    unit.Sprite.CanDraw = true;
                    unit.Sprite.Unit = unit;
                    unit.Sprite.Location = new Vector2(X, Y);
                    unit.SetLayer(0f);
                    tileMap.Tiles.Add(unit);
                }
            }
            return tileMap;
        }

        public void SetGameplayServices()
        {
            tileService = unitsManager.Game.Services.GetService(typeof(IUnitsService<Tile>)) as IUnitsService<Tile>;
        }

        

        public void Update(GameTime gameTime)
        {
            var millisecondsPassed = gameTime.ElapsedGameTime.Milliseconds;
            if (CurrentTileMap != null)
            {
                IList<Tile> tiles = CurrentTileMap.Tiles;

                    Vector2 cameraLocation = cameraService.Data.Translation;
                    Sprite sprite = null;
                    for (int i = 0; i < tiles.Count; ++i)
                    {
                        sprite = tiles[i].Sprite;
                        sprite.TranslationPosition = sprite.Location + cameraLocation;
                        animationService.Update(sprite.CurrentAnimation, millisecondsPassed);
                    }
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (CurrentTileMap != null)
            {
                foreach (Tile tile in CurrentTileMap.Tiles)
                {
                    spriteBatchService.Draw(tile.Sprite);
                }
            }
        }

        public Vector2 GetVectorToNewTile(Hero hero)
        {
            Vector2 position;

            if (hero.CurrentTilePosition.X < 0 || hero.CurrentTilePosition.Y < 0 || hero.CurrentTilePosition.X >= currentTileMap.Size.X - 1 || hero.CurrentTilePosition.Y >= currentTileMap.Size.Y - 1)
            {
                return new Vector2(0, 0);
            }

            Tile tile = currentTileMap.Tiles[hero.CurrentTilePosition.X + hero.CurrentTilePosition.Y * currentTileMap.Size.X];

            position = new Vector2(tile.Sprite.Location.X + tile.Sprite.Origin.X, tile.Sprite.Location.Y + tile.Sprite.Origin.Y);
            position.X = position.X - hero.Sprite.Location.X;
            position.Y = position.Y - hero.Sprite.Location.Y;

            return position;
        }

        #region private

        private ISpriteBatchService spriteBatchService;
        private ISpriteService spriteService;
        private IUnitsService<Tile> tileService;
        private UnitsManager unitsManager;
        private ICamera2DService cameraService;
        private TileMap currentTileMap;
        private IAnimationService animationService;
        #endregion
    }
}
