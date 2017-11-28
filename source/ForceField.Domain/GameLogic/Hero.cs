using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Domain.Renderer;
using Microsoft.Xna.Framework;

// todo: слоты для артефактов, положение в мире

namespace ForceField.Domain.GameLogic
{
    public class Hero : BattleUnit
    {
        public Hero()
        {
            Library = new SpellLibrary();
            LocationArea = 1;
            Inventory = new List<Item>();
            TilesArea = new List<Tile>();
        }

        #region Properties

        public float TimePerMove { get; set; }      // для чего это?

        public bool IsMove { get; set; }            // почему не в Unit?

        public Point CurrentTilePosition;           // нужно убрать

        public int MagicPower { get; set; }

        public SpellLibrary Library { get; set; }

        public List<Item> Inventory { get; set; }

        public bool IsPlayerHero { get; set; }

        public override void SetBound(TileMap tileMap)
        {
            if (LocationArea <= 0)
            {
                return;
            }

            TilesArea.Clear();

            int offset = this.LocationArea - 1;

            int top = CurrentTilePosition.X + CurrentTilePosition.Y * tileMap.Size.X;
            int bottom = CurrentTilePosition.X + offset + (CurrentTilePosition.Y + offset) * tileMap.Size.X;
            int left = CurrentTilePosition.X + (CurrentTilePosition.Y + offset) * tileMap.Size.X;
            int right = CurrentTilePosition.X + offset + CurrentTilePosition.Y * tileMap.Size.X;

            TilesArea.Add(tileMap.Tiles[left]);
            TilesArea.Add(tileMap.Tiles[right]);
            TilesArea.Add(tileMap.Tiles[top]);
            TilesArea.Add(tileMap.Tiles[bottom]);
            BoundingBox = DomainMath.CreateBound(this, tileMap.CellSize.X, tileMap.CellSize.Y);
        }

        #endregion
    }
}
