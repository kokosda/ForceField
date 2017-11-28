using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForceField.Domain.GameLogic;
using Microsoft.Xna.Framework.Graphics;

namespace ForceField.Interfaces
{
    public interface IUnitsService<T> where T : Unit
    {
        IList<T> Units { get; set; }

        void CreateUnitsFromServiceByResourcePath(string templatePath);

        Unit GetRandom();

        Texture2D GetFirstTexture(T unit);

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);

        void SetSpriteService(ISpriteService spriteService);
    }
}
