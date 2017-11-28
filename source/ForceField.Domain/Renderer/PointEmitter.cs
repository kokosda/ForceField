using Microsoft.Xna.Framework;
using ForceField.Domain.Interface;

namespace ForceField.Domain.Renderer
{
    public class PointEmitter : EmitterType
    {
        public PointEmitter()
        {
            GetLocation = new GenerateLocation(generateLocation);
        }

        public Vector2 generateLocation(Vector2 Location)
        {
            return new Vector2(Location.X,Location.Y);
        }
    }
}
