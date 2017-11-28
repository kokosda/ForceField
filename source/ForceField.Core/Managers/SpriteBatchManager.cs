using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Core.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ForceField.Interfaces;

namespace ForceField.Core.Managers
{
    public class SpriteBatchManager : IGameComponent
    {
        public SpriteBatchManager(Game game)
        {
            this.game = game;
            game.Components.Add(this);
            SpriteBatchService = new SpriteBatchService();
            game.Services.AddService(typeof(ISpriteBatchService), SpriteBatchService);
        }

        public void Initialize()
        {
            SpriteBatchService.Initialize(game);
        }

        #region private

        private SpriteBatchService SpriteBatchService;
        private Game game;

        #endregion
    }
}