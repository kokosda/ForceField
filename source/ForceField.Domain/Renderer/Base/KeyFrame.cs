using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ForceField.Domain.Renderer.Base
{
    public struct KeyFrame
    {
        public Point Location;
		public Texture2D Texture;
        public string TextureName;
		public int OrderNum;

        public override string ToString()
        {
            return string.Format("Location ({0}, {1}), Order {2}", Location.X, Location.Y, OrderNum);
        }

        public static KeyFrame operator +(KeyFrame kf, int value)
        {
            kf.OrderNum = kf.OrderNum + value;
            return kf;
        }

        public static bool operator ==(int value, KeyFrame kf)
        {
            return kf.OrderNum == value;
        }

        public static bool operator !=(int value, KeyFrame kf)
        {
            return kf.OrderNum != value;
        }

        public static bool operator ==(KeyFrame value1, KeyFrame value2)
        {
            return value1.OrderNum == value2.OrderNum;
        }

        public static bool operator !=(KeyFrame value1, KeyFrame value2)
        {
            return value1.OrderNum != value2.OrderNum;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}