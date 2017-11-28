using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ForceField.Interfaces
{
    public interface IScript
    {
        void SetArgs(params object[] args);

        void Activate(Game game);

        void Update(GameTime gameTime);                  
    }
}
