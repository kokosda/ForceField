using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForceField.Core.Services;
using ForceField.Interfaces;

namespace ForceField.Core.Managers
{
    public class CursorManager : IGameComponent
    {
        public CursorManager(Game game)
        {
            this.game = game;
            cursorService = new CursorService(game);
            game.Services.AddService(typeof(ICursorService), cursorService);
            game.Components.Add(this);
        }

        public void Initialize()
        {
        }

        private Game game;
        public CursorService cursorService;

    }
}
