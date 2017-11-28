using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ForceField.GameState.Services.Base;
using ForceField.GameState.Models.Base;
using ForceField.GameState.Models;
using ForceField.Core.Managers;
using ForceField.Core.Service;

namespace ForceField.GameState.Services
{
    public class MessageBoxScreenServiceImpl : GameScreenService
    {
        public MessageBoxScreenServiceImpl(ScreenManager screenManager) :
            base(screenManager)
        {
        }

        #region Initialization

        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded Data.
        /// </summary>
        public override void Activate(GameScreen screen, bool instancePreserved)
        {
            if (!instancePreserved)
            {
                var messageBoxScreen = screen as MessageBoxScreen;
                var resMan = ScreenManager.Game.Components.First(p => p.GetType() == typeof(ResourceManager)) as ResourceManager;
                messageBoxScreen.GradientTexture = resMan.Textures.GetByName("gradient").Data;
            }
        }

        #endregion

        #region Handle Input

        /// <summary>
        /// Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput(GameScreen screen, GameTime gameTime, InputService input)
        {
            PlayerIndex playerIndex;
            var messageBoxScreen = screen as MessageBoxScreen;

            // We pass in our ControllingPlayer, which may either be null (to
            // accept input from any player) or a specific index. If we pass a null
            // controlling player, the InputState helper returns to us which player
            // actually provided the input. We pass that through to our Accepted and
            // Cancelled events, so they can tell which player triggered them.
            if (messageBoxScreen.MenuSelect.Evaluate(input, messageBoxScreen.ControllingPlayer, out playerIndex))
            {
                // Raise the accepted event, then exit the message box.
                messageBoxScreen.OnAccepted(messageBoxScreen, this, new PlayerIndexEventArgs(playerIndex));                

                ExitScreen(messageBoxScreen);
            }
            else if (messageBoxScreen.MenuCancel.Evaluate(input, messageBoxScreen.ControllingPlayer, out playerIndex))
            {
                // Raise the cancelled event, then exit the message box.
                messageBoxScreen.OnCancelled(messageBoxScreen, this, new PlayerIndexEventArgs(playerIndex));

                ExitScreen(messageBoxScreen);
            }
        }

        #endregion

        #region Draw
        
        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameScreen screen, GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;
            var messageBoxScreen = screen as MessageBoxScreen;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(messageBoxScreen.TransitionAlpha * 2 / 3);

            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(messageBoxScreen.Message);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            // The background includes a border somewhat larger than the text itself.
            const int hPad = 32;
            const int vPad = 16;

            Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y + vPad * 2);

            // Fade the popup alpha during transitions.
            Color color = Color.White * messageBoxScreen.TransitionAlpha;

            spriteBatch.Begin();

            // Draw the background rectangle.
            spriteBatch.Draw(messageBoxScreen.GradientTexture, backgroundRectangle, color);

            // Draw the message box text.
            spriteBatch.DrawString(font, messageBoxScreen.Message, textPosition, color);

            spriteBatch.End();
        }
        
        #endregion
    }
}
