using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ForceField.Interfaces;
using ForceField.GameState.Models;
using ForceField.GameState.Models.Base;
using ForceField.Core.Managers;
using ForceField.Core.Services;
using ForceField.Domain.Renderer;
using ForceField.Core.Service;
using ForceField.Domain.Input;

namespace ForceField.GameState.Services.Base
{
    public abstract class MenuScreenService : GameScreenService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MenuScreenService(ScreenManager screenManager) :
            base(screenManager)
        {
            particleSystemManager = ScreenManager.Game.Components.First(p => p.GetType() == typeof(ParticleSystemManager)) as ParticleSystemManager;
            menuParticleService = ScreenManager.Game.Services.GetService(typeof(IParticleEmitterService<MenuEmitter>)) as MenuEmitterService;
        }

        #region Handle Input
        
        /// <summary>
        /// Responds to user input, changing the selected entry and accepting
        /// or cancelling the menu.
        /// </summary>
        public override void HandleInput(GameScreen screen, GameTime gameTime, InputService input)
        {
            // For input tests we pass in our ControllingPlayer, which may
            // either be null (to accept input from any player) or a specific index.
            // If we pass a null controlling player, the InputState helper returns to
            // us which player actually provided the input. We pass that through to
            // OnSelectEntry and OnCancel, so they can tell which player triggered them.
            PlayerIndex playerIndex;
            var menuScreen = screen as MenuScreen;

            // Move to the previous menu entry?
            if (menuScreen.MenuUp.Evaluate(input, menuScreen.ControllingPlayer, out playerIndex))
            {
                menuScreen.SelectedEntry--;

                if (menuScreen.SelectedEntry < 0)
                    menuScreen.SelectedEntry = menuScreen.MenuEntries.Count - 1;
            }

            // Move to the next menu entry?
            if (menuScreen.MenuDown.Evaluate(input, menuScreen.ControllingPlayer, out playerIndex))
            {
                menuScreen.SelectedEntry++;

                if (menuScreen.SelectedEntry >= menuScreen.MenuEntries.Count)
                    menuScreen.SelectedEntry = 0;
            }

            Rectangle SelectRegion = new Rectangle();
            for (int i = 0; i < menuScreen.MenuEntries.Count; ++i)
            {
                MenuEntry entry = menuScreen.MenuEntries[i];
                Vector2 stringParam = ScreenManager.Font.MeasureString(entry.Text);
                SelectRegion = new Rectangle((int)(entry.Position.X), (int)(entry.Position.Y), (int)(stringParam.X), (int)(stringParam.Y));
                if (SelectRegion.Contains(input.IsMousePosition()) == true)
                {
                    menuScreen.SelectedEntry = i;
                    if (input.IsMouseButtonPress(MouseButton.LeftButton) == true)
                    {
                        OnSelectEntry(menuScreen, menuScreen.SelectedEntry, playerIndex);
                    }
                }
            }

            if (menuScreen.MenuSelect.Evaluate(input, menuScreen.ControllingPlayer, out playerIndex))
            {
                OnSelectEntry(menuScreen, menuScreen.SelectedEntry, playerIndex);
            }

            if (menuScreen.MenuCancel.Evaluate(input, menuScreen.ControllingPlayer, out playerIndex))
            {
                OnCancel(menuScreen, playerIndex);
            }
        }
        
        /// <summary>
        /// Handler for when the user has chosen a menu entry.
        /// </summary>
        protected virtual void OnSelectEntry(MenuScreen menuScreen, int entryIndex, PlayerIndex playerIndex)
        {
            menuScreen.MenuEntries[entryIndex].OnSelectEntry(playerIndex);
        }
        
        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected virtual void OnCancel(MenuScreen menuScreen, PlayerIndex playerIndex)
        {
            ExitScreen(menuScreen);
        }
        
        /// <summary>
        /// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        /// </summary>
        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            var menuEntry = sender as MenuEntry;
            OnCancel(menuEntry.MenuScreen, e.PlayerIndex);
        }
        
        #endregion

        #region Update and Draw
        
        /// <summary>
        /// Allows the screen the chance to position the menu entries. By default
        /// all menu entries are lined up in a vertical list, centered on the screen.
        /// </summary>
        protected virtual void UpdateMenuEntryLocations(MenuScreen menuScreen)
        {
            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(menuScreen.TransitionPosition, 2);

            // start at Y = 175; each X value is generated per entry
            Vector2 position = new Vector2(0f, 175f);

            // update each menu entry's location in turn
            for (int i = 0; i < menuScreen.MenuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuScreen.MenuEntries[i];
                
                // each entry is to be centered horizontally
                position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - menuEntry.GetWidth(menuScreen) / 2;

                if (menuScreen.ScreenState == ScreenState.TransitionOn)
                    position.X -= transitionOffset * 256;
                else
                    position.X += transitionOffset * 512;

                // set the entry's position
                menuEntry.Position = position;

                // move down for the next entry the size of this entry
                position.Y += menuEntry.GetHeight(menuScreen);
            }
        }
        
        /// <summary>
        /// Updates the menu.
        /// </summary>
        public override void Update(GameScreen screen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            var menuScreen = screen as MenuScreen;

            base.Update(menuScreen, gameTime, otherScreenHasFocus, coveredByOtherScreen);
            Point mouseLoc = ScreenManager.input.IsMousePosition();

            //MenuEmitter MenuEmitter = (MenuEmitter)particleSystemManager.GetEmitter<MenuEmitter>("CursorEmitter");
            //MenuEmitter MenuSmokeEmitter = (MenuEmitter)particleSystemManager.GetEmitter<MenuEmitter>("CursorSmokeEmitter");
            //if (LastMouseLoc.X == mouseLoc.X && LastMouseLoc.Y == mouseLoc.Y)
            //{
            //    MenuEmitter.Spawn = false;
            //    MenuSmokeEmitter.Spawn = false;
            //}
            //else
            //{
            //    MenuEmitter.Spawn = true;
            //    MenuSmokeEmitter.Spawn = true;
            //}
            //MenuEmitter.EmitterInfo.Location = new Vector2(mouseLoc.X + 15, mouseLoc.Y + 15);
            //MenuSmokeEmitter.EmitterInfo.Location = new Vector2(mouseLoc.X + 15, mouseLoc.Y + 15);
            // Update each nested MenuEntry object.
            //for (int i = 0; i < menuScreen.MenuEntries.Count; i++)
            //{
            //    bool isSelected = menuScreen.IsActive && (i == menuScreen.SelectedEntry);

            //    menuScreen.MenuEntries[i].Update(menuScreen, isSelected, gameTime);
            //}
            //menuParticleService.Update(gameTime);

            //LastMouseLoc = mouseLoc;
        }
        
        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameScreen screen, GameTime gameTime)
        {
            var menuScreen = screen as MenuScreen;
            // make sure our entries are in the right place before we draw them
            UpdateMenuEntryLocations(menuScreen);

            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            spriteBatch.Begin();

            // Draw each menu entry in turn.
            for (int i = 0; i < menuScreen.MenuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuScreen.MenuEntries[i];

                bool isSelected = menuScreen.IsActive && (i == menuScreen.SelectedEntry);

                menuEntry.Draw(menuScreen, isSelected, gameTime);
            }

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(menuScreen.TransitionPosition, 2);

            // Draw the menu title centered on the screen
            Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            Vector2 titleOrigin = font.MeasureString(menuScreen.MenuTitle) / 2;
            Color titleColor = new Color(192, 192, 192) * menuScreen.TransitionAlpha;
            float titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, menuScreen.MenuTitle, titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);
            spriteBatch.End();

            //menuParticleService.Draw(gameTime);
            
        }

        #endregion
        private MenuEmitterService menuParticleService;
        private ParticleSystemManager particleSystemManager;

    }
}
