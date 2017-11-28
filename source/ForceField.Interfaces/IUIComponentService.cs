using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ForceField.Interfaces
{
    public interface IUIComponentService
    {
        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);

        //IList<UIComponent> Elements { get; protected set; }
    }
}
