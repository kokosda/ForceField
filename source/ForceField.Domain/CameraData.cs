using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ForceField.Domain
{
    public class CameraData
    {
        public Vector2 Offset;
        public Vector2 OldTranslation;
        public Vector2 Translation;
        public float Angle;
        public bool IsUpdate;
        public bool IsNewPosition;
    }
}
