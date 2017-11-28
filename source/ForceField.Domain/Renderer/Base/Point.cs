using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forcefield.Domain.Renderer.Base
{
    public struct Point
    {
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X;

        public float Y;
    }
}
