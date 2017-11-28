using Microsoft.Xna.Framework;

namespace ForceField.Domain.Interface
{
    public class EmitterType
    {
        public delegate Vector2 GenerateLocation(Vector2 Location);
        public GenerateLocation GetLocation;
    }
}
