using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ForceField.Domain.GameLogic
{
    public class TileMap : Unit
    {
        public TileMap()
        {
            tiles = new List<Tile>();
        }

        #region Properties

        public List<Tile> Tiles
        {
            get
            {
                return tiles;
            }
        }

        public Point Size { get; set; }

        public Point CellSize { get; set; }

        public new Vector2 Position { get; set; }

        public float LayerX
        {
            get
            {
                return 0.000001f;
            }
        }

        public float LayerY
        {
            get
            {
                return 0.000002f;
            }
        }

        #endregion

        public object Clone()
        {
            // todo wtf??

            return null;
        }

        public Point GetTilePosition(Vector2 position, Vector2 cam)
        {
            position.X -= cam.X;
            position.Y -= cam.Y;
            int x = (int)Math.Floor((position.Y - position.X * 0.5f + (CellSize.X / 2)) / (float)CellSize.Y);
            int y = (int)Math.Floor((position.Y + position.X * 0.5f) / (float)CellSize.Y);

            return new Point(x,y);
        }

        public  Tile GetTile(int x, int y)
        {
            if (x >= 0 && x < Size.X && y >= 0 && y < Size.Y)
            {
                return Tiles[x + y * Size.X];
            }
            else
            {
                return null;
            }
        }

        public Tile GetTile(Vector2 position, Vector2 cam)
        {
            Point pos = GetTilePosition(position,cam);

            return GetTile(pos.X,pos.Y);
        }

        #region private

        private List<Tile> tiles;

        #endregion
    }
}
