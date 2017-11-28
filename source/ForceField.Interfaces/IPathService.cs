using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForceField.Domain.GameLogic;

namespace ForceField.Interfaces
{
    public interface IPathService
    {
        List<Point> GetPath(TileMap tileMap, int area, Point start, Point end);
    }
}
