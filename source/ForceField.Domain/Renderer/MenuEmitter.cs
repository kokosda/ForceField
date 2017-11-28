using Microsoft.Xna.Framework.Graphics;

namespace ForceField.Domain.Renderer
{
    public class MenuEmitter : Emitter
    {
        public bool Spawn;
        public MenuEmitter(Sprite sprite, int Count, string name)
            : base(sprite, Count, name)
        {
            Spawn = false;
        }
    }
}
