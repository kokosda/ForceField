using System;
using Microsoft.Xna.Framework;

namespace ForceField.Domain.GameLogic
{
    public static class DomainMath
    {
        public static HexagonBound CreateBound(Unit unit, int tileSizeX, int tileSizeY)
        {
            Vector2[] points = new Vector2[6];

            int area = unit.LocationArea;

            Tile left = unit.TilesArea[0];
            Tile right = unit.TilesArea[1];
            Tile bottom = unit.TilesArea[2];
            Tile top = unit.TilesArea[3];

            unit.Sprite.Location = left.Sprite.Location;
            unit.Sprite.Location = new Vector2(unit.Sprite.Location.X,unit.Sprite.Location.Y - tileSizeY/2);
   
            Vector2 point = new Vector2();


            int halfTileSizeX = tileSizeX >> 1;
            int halfTileSizeY = tileSizeY >> 1;

            point.X = top.Position.X + halfTileSizeX;
            point.Y = top.Position.Y - unit.Sprite.CurrentAnimation.FrameSize.Y;

            points[0] = point;

            point.X = left.Position.X;
            point.Y = left.Position.Y - unit.Sprite.CurrentAnimation.FrameSize.Y;

            points[1] = point;

            point.X = left.Position.X;
            point.Y = left.Position.Y + halfTileSizeY ;

            points[2] = point;

            point.X = bottom.Position.X + halfTileSizeX;
            point.Y = bottom.Position.Y + tileSizeY ;

            points[3] = point;

            point.X = right.Position.X + tileSizeX;
            point.Y = right.Position.Y + halfTileSizeY ;

            points[4] = point;

            point.X = right.Position.X + tileSizeX;
            point.Y = right.Position.Y - unit.Sprite.CurrentAnimation.FrameSize.Y;

            points[5] = point;



            HexagonBound hexagonBound = new HexagonBound(points);
            return hexagonBound;
        }
    }
}
