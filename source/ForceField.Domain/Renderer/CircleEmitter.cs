using Microsoft.Xna.Framework;
using System;
using ForceField.Domain.Interface;

namespace ForceField.Domain.Renderer
{
    public class CircleEmitter : EmitterType
    {
        public float Radius { get; set; }
        public bool FillCircle { get; set; }
        public float MaxAngleGenerate { get; set; }
        public float MinAngleGenerate { get; set; }

        private Random Random = new Random();

        public CircleEmitter(
                   float Radius,
                   bool FillCircle,
                   float MaxAngleGenerate,
                   float MinAngleGenerate
                   )
        {
            this.Radius = Radius;
            this.FillCircle = FillCircle;
            this.MaxAngleGenerate = MaxAngleGenerate;
            this.MinAngleGenerate = MinAngleGenerate;
            GetLocation = new GenerateLocation(generateLocation);
        }

        public Vector2 generateLocation(Vector2 Location)
        {
            float radian = MathHelper.Lerp(MinAngleGenerate, MaxAngleGenerate, (float)Random.NextDouble()) / 180.0f * 3.14159f;
            return new Vector2(Location.X + (float)Math.Cos(radian)*(FillCircle ? Radius : Radius * (float)Random.NextDouble()),
                                           Location.Y + (float)Math.Sin(radian)*(FillCircle ? Radius : Radius * (float)Random.NextDouble()));
        }
    }
}
