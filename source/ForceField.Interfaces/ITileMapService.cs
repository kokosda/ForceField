using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Domain.GameLogic;
using ForceField.Interfaces;
using Microsoft.Xna.Framework;
using ForceField.Domain.Renderer;

namespace ForceField.Interfaces
{
    public interface ITileMapService : IUnitsService<TileMap>
    {
        void SetGameplayServices();

        void ConstructInitialTileMaps();

        TileMap CurrentTileMap { get; set; }

        Vector2 GetVectorToNewTile(Hero hero);

        TileMap CreateTileMap(Point cellSize, Vector2 position, Point size, Sprite sprite, string name);
    }
}
