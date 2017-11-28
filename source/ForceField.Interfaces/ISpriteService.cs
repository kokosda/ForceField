using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Domain.Renderer;
using Microsoft.Xna.Framework.Graphics;
using ForceField.Domain.Renderer.Base;

namespace ForceField.Interfaces
{
    public interface ISpriteService
    {
        Sprite GetByName(string name);

        SpriteText GetSpriteText(string name);

        Texture2D GetTexture(Sprite sprite, string textureName);

        Texture2D GetTexture(Sprite sprite, int index);

        IList<Sprite> Sprites { get; set; }

        void SetAnimatedAction(AnimatedAction action, Sprite onSprite);
    }
}
