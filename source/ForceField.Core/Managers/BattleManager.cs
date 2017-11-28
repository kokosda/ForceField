using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ForceField.Core.Managers.Services;
using ForceField.Interfaces;

namespace ForceField.Core.Managers
{
    public class BattleManager : IGameComponent
    {
        public BattleManager(Game game)
        {
            this.game = game;

            battleService = new BattleService(game);
            game.Services.AddService(typeof(IBattleService), battleService);
            game.Components.Add(this);
        }

        public void Initialize()
        {
        }

        #region private

        private Game game;
        private BattleService battleService;

        #endregion
    }
}
