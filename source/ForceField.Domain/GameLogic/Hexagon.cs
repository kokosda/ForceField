using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ForceField.Domain.GameLogic
{
    public struct HexagonBound
    {
        public HexagonBound(Vector2[] points)
        {
            #region Initialize lines
            lines = new Line2D[6];
            lines[0].Begin = points[0];
            lines[0].End = points[1];

            lines[1].Begin = points[1];
            lines[1].End = points[2];
            lines[2].Begin = points[2];
            lines[2].End = points[3];
            lines[3].Begin = points[3];
            lines[3].End = points[4];
            lines[4].Begin = points[4];
            lines[4].End = points[5];
            lines[5].Begin = points[5];
            lines[5].End = points[0];
            #endregion
        }

        public bool Contains(Vector2 location)
        {
            bool isInside = false;

            for (int i = 0; i < 6; ++i)
            {
                Line2D line = lines[i];

                if (location.X <= Math.Max(line.Begin.X, line.End.X))
                {
                    if (location.Y <= Math.Max(line.Begin.Y, line.End.Y))
                    {
                        if (location.Y > Math.Min(line.Begin.Y, line.End.Y))
                        {
                            if (line.Begin.Y != line.End.Y)
                            {
                                float X = line.Begin.X + (location.Y - line.Begin.Y) * (line.End.X - line.Begin.X) / (line.End.Y - line.Begin.Y);

                                if (line.Begin.X == line.End.X || location.X <= X)
                                {
                                    isInside = !isInside;
                                }
                            }
                        }
                    }
                }
            }

            return isInside;
        }

        private Line2D[] lines;
    }
}