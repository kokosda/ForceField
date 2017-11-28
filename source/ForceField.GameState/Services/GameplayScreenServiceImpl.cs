using System;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ForceField.GameState.Services.Base;
using ForceField.Core.Managers;
using ForceField.GameState.Models.Base;
using ForceField.GameState.Models;
using ForceField.Core.Service;
using ForceField.Interfaces;

namespace ForceField.GameState.Services
{
    public class GameplayScreenServiceImpl : GameScreenService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreenServiceImpl(ScreenManager screenManager) :
            base(screenManager)
        {
            camera = screenManager.Game.Services.GetService(typeof(ICamera2DService)) as ICamera2DService;
            input = screenManager.Game.Services.GetService(typeof(IUserInputService)) as IUserInputService;
            spriteService = screenManager.Game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            animationService = screenManager.Game.Services.GetService(typeof(IAnimationService)) as IAnimationService;
            spriteBatchService = screenManager.Game.Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
            uMan = ScreenManager.Game.Components.First(p => p.GetType() == typeof(UnitsManager)) as UnitsManager;
        }
        
        #region Properties
        
        public ContentManager Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void Activate(GameScreen screen, bool instancePreserved)
        {
            var gameplayScreen = screen as GameplayScreen;

            if (!instancePreserved)
            {
                if (Content == null)
                    Content = new ContentManager(ScreenManager.Game.Services, "Content");

                var resMan = ScreenManager.Game.Components.First(p => p.GetType() == typeof(ResourceManager)) as ResourceManager;

                //gameplayScreen.GameFont = resMan.Fonts.GetByName("unitbar").Data;

                // A real game would probably have more content than this sample, so
                // it would take longer to load. We simulate that by delaying for a
                // while, giving you a chance to admire the beautiful loading screen.
                Thread.Sleep(1000);

                // once the load has finished, we use ResetElapsedTime to tell the game's
                // timing mechanism that we have just finished a very long frame, and that
                // it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }
        }
        
        public override void Deactivate(GameScreen screen)
        {
            var gameplayScreen = screen as GameplayScreen;

            base.Deactivate(gameplayScreen);

            //TODO: провести работу по деактивации экрана
        }
        
        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload(GameScreen screen)
        {
            var gameplayScreen = screen as GameplayScreen;

            //TODO: провести работу по выгрузке ресурсов
        }
        
        #endregion

        #region Update and Draw
        
        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameScreen screen, GameTime gameTime, 
                                        bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            var gameplayScreen = screen as GameplayScreen;

            base.Update(gameplayScreen, gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
            {
                gameplayScreen.PauseAlpha = Math.Min(gameplayScreen.PauseAlpha + 1f / 32, 1);
            }
            else
            {
                gameplayScreen.PauseAlpha = Math.Max(gameplayScreen.PauseAlpha - 1f / 32, 0);
            }

            if (gameplayScreen.IsActive)
            {
                uMan.IsGameplay = true;
                //uMan.tileService.Update(gameTime);
            }
            else
            {
                uMan.IsGameplay = false;
            }
        }
        
        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameScreen screen, GameTime gameTime, InputService input)
        {
            var gameplayScreen = screen as GameplayScreen;

            if (input == null)
                throw new ArgumentNullException("input");
        }

        public override void Draw(GameScreen screen, GameTime gameTime)
        {
            var gameplayScreen = screen as GameplayScreen;

            // If the game is transitioning on or off, fade it out to black.
            if (gameplayScreen.TransitionPosition > 0 || gameplayScreen.PauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - gameplayScreen.TransitionAlpha, 1f, gameplayScreen.PauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
            
        }


        #endregion

        #region protected

        protected ICamera2DService camera;
        protected IUserInputService input;
        protected IAnimationService animationService;
        protected ISpriteService spriteService;
        protected ISpriteBatchService spriteBatchService;
        protected UnitsManager uMan;

        #endregion 

        #region private

        private ContentManager content;

        #endregion
    }
}
