using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ForceField.Domain.GameLogic
{
    public class Tile : Unit
    {
        public Tile()
        {

        }

        #region Properties

        public Point MapCoordinate { get; set; }

        public static Point DefaultSize { get; set; }

        public bool IsOccupy { get; set; }
        #endregion
    }
}
