using System;
using Microsoft.Xna.Framework;
using ForceField.Domain.Interface;
namespace ForceField.Domain.Renderer
{
    public class RectEmitter : EmitterType
    {
        public float Width { get; set; }
        public float Height { get; set; }
        
        private Random Random = new Random();

        public RectEmitter(float width,float height)
        {
            Width = width;
            Height = height;
            GetLocation = new GenerateLocation(generateLocation);
        }

        public Vector2 generateLocation(Vector2 Location)
        {
            float seed = (float)Random.NextDouble();
            return new Vector2(MathHelper.Lerp(Location.X, Location.X + Width, seed), MathHelper.Lerp(Location.Y,Location.Y + Height,seed));
        }
    }
}
