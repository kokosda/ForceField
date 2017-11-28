using ForceField.Domain.GameLogic;
using ForceField.Domain.Renderer;
using Microsoft.Xna.Framework.Graphics;

namespace ForceField.Domain
{
    public class Cursor : Unit
    {
        public Cursor(string name, Sprite sprite)
        {
            Sprite = Sprite.Clone(sprite);
            Sprite.Name = name;
        }
    }
}
