﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ForceField.GameState.Services.Base;
using ForceField.GameState.Models;
using ForceField.GameState.Models.Base;

namespace ForceField.GameState.Services
{
    public class LoadingScreenServiceImpl : GameScreenService
    {
        /// <summary>
        /// The constructor is private: loading screens should
        /// be activated via the static Load method instead.
        /// </summary>
        public LoadingScreenServiceImpl(ScreenManager screenManager) :
            base(screenManager)
        {
        }

        #region Initialization
        
        /// <summary>
        /// Activates the NEWLY created loading screen.
        /// </summary>
        public static void Load(GameScreenService service, LoadingScreen loadingScreen, PlayerIndex? controllingPlayer)
        {
            // Tell all the current screens to transition off.
            foreach (var screen in service.ScreenManager.GetScreens())
            {
                service.ExitScreen(screen);
            }

            foreach (var screen in loadingScreen.ScreensToLoad)
            {
                screen.ControllingPlayer = controllingPlayer;
            }

            service.ScreenManager.AddScreen(loadingScreen, controllingPlayer);
        }
        
        #endregion

        #region Update and Draw
        
        /// <summary>
        /// Updates the loading screen.
        /// </summary>
        public override void Update(GameScreen screen, GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            var loadingScreen = screen as LoadingScreen;
            base.Update(loadingScreen, gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // If all the previous screens have finished transitioning
            // off, it is time to actually perform the load.
            if (loadingScreen.OtherScreensAreGone)
            {
                ScreenManager.RemoveScreen(loadingScreen);

                foreach (GameScreen screeny in loadingScreen.ScreensToLoad)
                {
                    if (screeny != null)
                    {
                        ScreenManager.AddScreen(screeny, screeny.ControllingPlayer);
                    }
                }

                // Once the load has finished, we use ResetElapsedTime to tell
                // the  game timing mechanism that we have just finished a very
                // long frame, and that it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }
        }
        
        /// <summary>
        /// Draws the loading screen.
        /// </summary>
        public override void Draw(GameScreen screen, GameTime gameTime)
        {
            var loadingScreen = screen as LoadingScreen;

            // If we are the only active screen, that means all the previous screens
            // must have finished transitioning off. We check for this in the Draw
            // method, rather than in Update, because it isn't enough just for the
            // screens to be gone: in order for the transition to look good we must
            // have actually drawn a frame without them before we perform the load.
            if ((loadingScreen.ScreenState == ScreenState.Active) && (ScreenManager.GetScreens().Length == 1))
            {
                loadingScreen.OtherScreensAreGone = true;
            }

            // The gameplay screen takes a while to load, so we display a loading
            // message while that is going on, but the menus load very quickly, and
            // it would look silly if we flashed this up for just a fraction of a
            // second while returning from the game to the menus. This parameter
            // tells us how long the loading is going to take, so we know whether
            // to bother drawing the message.
            if (loadingScreen.LoadinIsSlow)
            {
                SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                SpriteFont font = ScreenManager.Font;

                const string message = "Loading...";

                // Center the text in the viewport.
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
                Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
                Vector2 textSize = font.MeasureString(message);
                Vector2 textPosition = (viewportSize - textSize) / 2;

                Color color = Color.White * loadingScreen.TransitionAlpha;

                // Draw the text.
                spriteBatch.Begin();
                spriteBatch.DrawString(font, message, textPosition, color);
                spriteBatch.End();
            }
        }
        
        #endregion
    }
}
