using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForceField.GameState.Models.Base;
using ForceField.Core.Managers;
using ForceField.Core.Service;

namespace ForceField.GameState.Interfaces
{
    public interface IGameScreenService
    {
        ScreenManager ScreenManager { get; set; }

        /// <summary>
        /// Activates the screen. Called when the screen is added to the screen manager or if the game resumes
        /// from being paused or tombstoned.
        /// </summary>
        /// <param name="instancePreserved">
        /// True if the game was preserved during deactivation, false if the screen is just being added or if the game was tombstoned.
        /// On Xbox and Windows this will always be false.
        /// </param>
        void Activate(GameScreen screen, bool instancePreserved);


        /// <summary>
        /// Deactivates the screen. Called when the game is being deactivated due to pausing or tombstoning.
        /// </summary>
        void Deactivate(GameScreen screen);


        /// <summary>
        /// Unload content for the screen. Called when the screen is removed from the screen manager.
        /// </summary>
        void Unload(GameScreen screen);


        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike HandleInput, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        void Update(GameScreen screen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen);


        /// <summary>
        /// Helper for updating the screen transition position.
        /// </summary>
        bool UpdateTransition(GameScreen screen, GameTime gameTime, TimeSpan time, int direction);


        /// <summary>
        /// Allows the screen to handle user input. Unlike Update, this method
        /// is only called when the screen is active, and not when some other
        /// screen has taken the focus.
        /// </summary>
        void HandleInput(GameScreen screen, GameTime gameTime, InputService input);


        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        void Draw(GameScreen screen, GameTime gameTime);


        /// <summary>
        /// Tells the screen to go away. Unlike ScreenManager.RemoveScreen, which
        /// instantly kills the screen, this method respects the transition timings
        /// and will give the screen a chance to gradually transition off.
        /// </summary>
        void ExitScreen(GameScreen screen);
    }
}
