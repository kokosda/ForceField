using Microsoft.Xna.Framework;
using System;
using ForceField.Domain.GameLogic;

namespace ForceField.Domain.Renderer.Base
{
    public struct Particle
    {
        public Sprite Sprite { get; set; }
        public float Age { get; set; }
        public Vector2 Vector { get; set; }
        public float Velocity { get; set; }
        public float MinusVelocity { get; set; }
        public Vector4 Color { get; set; }
        public float TimeSpawn { get; set; }
        public float MinusScale { get; set; }
        public Vector4 MinusColor { get; set; }
        public float AngleVector { get; set; }
        public float AgeDifference { get; set; }
        public float OffsetVector { get; set; }
        public float OffsetVelocity { get; set; }
    }
}
