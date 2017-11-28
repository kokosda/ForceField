using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Domain.GameLogic;
using Microsoft.Xna.Framework.Graphics;
using ForceField.Domain.Renderer;
using Microsoft.Xna.Framework;

namespace ForceField.Interfaces
{
    public interface ISpriteBatchService
    {
        ICamera2DService Camera2D { get; set; }

        void DrawAll(GameTime gameTime);

        void Draw(Sprite sprite);
        void Draw(SpriteText spriteText);

        void Reset();

        void Initialize(Game game);
    }
}
