using System;
using Microsoft.Xna.Framework;
using ForceField.GameState.Interfaces;
using ForceField.GameState.Models.Base;
using ForceField.Core.Service;

namespace ForceField.GameState.Services.Base
{
    public class GameScreenService : IGameScreenService
    {
        public GameScreenService(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
        }

        #region Properties

        /// <summary>
        /// Gets the manager that this screen belongs to.
        /// </summary>
        public ScreenManager ScreenManager
        {
            get
            {
                return screenManager;
            }
            set
            {
                screenManager = value;
            }
        }

        #endregion

        #region Initialize

        public virtual void InitializeScreen(GameScreen menuscreen) { }

        #endregion

        /// <summary>
        /// Activates the screen. Called when the screen is added to the screen manager or if the game resumes
        /// from being paused or tombstoned.
        /// </summary>
        /// <param name="instancePreserved">
        /// True if the game was preserved during deactivation, false if the screen is just being added or if the game was tombstoned.
        /// On Xbox and Windows this will always be false.
        /// </param>
        public virtual void Activate(GameScreen screen, bool instancePreserved) { }
        
        /// <summary>
        /// Deactivates the screen. Called when the game is being deactivated due to pausing or tombstoning.
        /// </summary>
        public virtual void Deactivate(GameScreen screen) { }
        
        /// <summary>
        /// Unload content for the screen. Called when the screen is removed from the screen manager.
        /// </summary>
        public virtual void Unload(GameScreen screen) { }
        
        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike HandleInput, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        public virtual void Update(GameScreen screen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            screen.OtherScreenHasFocus = otherScreenHasFocus;

            if (screen.IsExiting)
            {
                // If the screen is going away to die, it should transition off.
                screen.ScreenState = ScreenState.TransitionOff;

                if (!UpdateTransition(screen, gameTime, screen.TransitionOffTime, 1))
                {
                    // When the transition finishes, remove the screen.
                    ScreenManager.RemoveScreen(screen);
                }
            }
            else if (coveredByOtherScreen)
            {
                // If the screen is covered by another, it should transition off.
                if (UpdateTransition(screen, gameTime, screen.TransitionOffTime, 1))
                {
                    // Still busy transitioning.
                    screen.ScreenState = ScreenState.TransitionOff;
                }
                else
                {
                    // Transition finished!
                    screen.ScreenState = ScreenState.Hidden;
                }
            }
            else
            {
                // Otherwise the screen should transition on and become active.
                if (UpdateTransition(screen, gameTime, screen.TransitionOnTime, -1))
                {
                    // Still busy transitioning.
                    screen.ScreenState = ScreenState.TransitionOn;
                }
                else
                {
                    // Transition finished!
                    screen.ScreenState = ScreenState.Active;
                }
            }
        }
        
        /// <summary>
        /// Helper for updating the screen transition position.
        /// </summary>
        public bool UpdateTransition(GameScreen screen, GameTime gameTime, TimeSpan time, int direction)
        {
            // How much should we move by?
            float transitionDelta;

            if (time == TimeSpan.Zero)
            {
                transitionDelta = 1;
            }
            else
            {
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);
            }

            // Update the transition position.
            screen.TransitionPosition += transitionDelta * direction;

            // Did we reach the end of the transition?
            if (((direction < 0) && (screen.TransitionPosition <= 0)) ||
                ((direction > 0) && (screen.TransitionPosition >= 1)))
            {
                screen.TransitionPosition = MathHelper.Clamp(screen.TransitionPosition, 0, 1);
                return false;
            }

            // Otherwise we are still busy transitioning.
            return true;
        }
        
        /// <summary>
        /// Allows the screen to handle user input. Unlike Update, this method
        /// is only called when the screen is active, and not when some other
        /// screen has taken the focus.
        /// </summary>
        public virtual void HandleInput(GameScreen screen, GameTime gameTime, InputService input) { }
        
        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        public virtual void Draw(GameScreen screen, GameTime gameTime) { }
        
        /// <summary>
        /// Tells the screen to go away. Unlike ScreenManager.RemoveScreen, which
        /// instantly kills the screen, this method respects the transition timings
        /// and will give the screen a chance to gradually transition off.
        /// </summary>
        public void ExitScreen(GameScreen screen)
        {
            if (screen.TransitionOffTime == TimeSpan.Zero)
            {
                // If the screen has a zero transition time, remove it immediately.
                ScreenManager.RemoveScreen(screen);
            }
            else
            {
                // Otherwise flag that it should transition off and then exit.
                screen.IsExiting = true;
            }
        }

        #region protected

        protected ScreenManager screenManager;

        #endregion
    }
}
