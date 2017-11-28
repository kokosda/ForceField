using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ForceField.Domain.GameLogic;

namespace ForceField.Domain.Renderer.Base
{
    public class EmitterInfo
    {
        public Vector2 Location { get; set; }
        public float MaxStartVelocity { get; set; }
        public float MinStartVelocity { get; set; }
        public float MaxEndVelocity { get; set; }
        public float MinEndVelocity { get; set; }
        public int MaxLife { get; set; }
        public int MinLife { get; set; }
        public float MaxSpread { get; set; }
        public float MinSpread { get; set; }
        public Vector4 MinEndColor { get; set; }
        public Vector4 MaxEndColor { get; set; }
        public Vector4 MinStartColor { get; set; }
        public Vector4 MaxStartColor { get; set; }
        public float MinRotation { get; set; }
        public float MaxRotation { get; set; }
        public float MaxEndScale { get; set; }
        public float MinEndScale { get; set; }
        public float MaxStartScale { get; set; }
        public float MinStartScale { get; set; }
        public int MaxTimeSpawn { get; set; }
        public int MinTimeSpawn { get; set; }
        public float MinusTime { get; set; }
        public float MaxStartOffsetVector { get; set; }
        public float MinStartOffsetVector { get; set; }
        public float MaxEndOffsetVector { get; set; }
        public float MinEndOffsetVector { get; set; }
    }
}
