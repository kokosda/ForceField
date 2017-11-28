using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ForceField.GameState.Services.Base;
using ForceField.GameState.Models;
using ForceField.GameState.Models.Base;
using ForceField.Domain.GameLogic;
using ForceField.Interfaces;
using ForceField.Core.Services;

namespace ForceField.GameState.Services
{
    public class BackgroundScreenServiceImpl : GameScreenService
    {
        public BackgroundScreenServiceImpl(ScreenManager screenManager) :
            base(screenManager)
        {
            spriteBatchService = ScreenManager.Game.Services.GetService(typeof(ISpriteBatchService)) as SpriteBatchService;
        }

        #region Initialize

        public override void Activate(GameScreen screen, bool instancePreserved)
        {
            var backgroundScreen = screen as BackgroundScreen;

            if (!instancePreserved)
            {
                backgroundsService = ScreenManager.Game.Services.GetService(typeof(IUnitsService<Background>)) as IUnitsService<Background>;
                //backgroundScreen.background = backgroundsService.GetRandom() as Background;
            }
        }

        public override void Unload(GameScreen screen)
        {

        }

        #endregion

        #region Update and Draw

        public override void Update(GameScreen screen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            var backgroundScreen = screen as BackgroundScreen;
            base.Update(backgroundScreen, gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameScreen screen, GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);
            var backgroundScreen = screen as BackgroundScreen;
        }

        #endregion

        #region private

        private IUnitsService<Background> backgroundsService;
        private SpriteBatchService spriteBatchService;

        #endregion
    }
}
