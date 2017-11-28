using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using ForceField.Interfaces;
using ForceField.Core.Services;
using ForceField.Domain.GameLogic;

namespace ForceField.Core.Managers
{
    public class LevelManager : IGameComponent
    {
        LevelService levelService;
        Game game;
        public LevelManager(Game game)
        {
            levelService = new LevelService(new List<Level>());
            game.Components.Add(this);

            game.Services.AddService(typeof(ILevelService), levelService);
            this.game = game;
        }

        public void Initialize()
        {

        }
    }
}
