using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using ForceField.GameState.Interfaces;
using ForceField.GameState.Services.Base;
using Microsoft.Xna.Framework.Graphics;

namespace ForceField.GameState.Models.Base
{
    public abstract class GameScreen
    {
        public GameScreen(GameScreenService gameScreenServiceImpl)
        {
            this.gameScreenService = gameScreenServiceImpl;
        }

        #region Properties

        public virtual GameScreenService GameScreenServiceImpl
        {
            get
            {
                return gameScreenService;
            }
        }

        public bool IsPopup
        {
            get { return isPopup; }
            protected set { isPopup = value; }
        }
        
        /// <summary>
        /// Indicates how long the screen takes to
        /// transition on when it is activated.
        /// </summary>
        public TimeSpan TransitionOnTime
        {
            get { return transitionOnTime; }
            set { transitionOnTime = value; }
        }
        
        /// <summary>
        /// Indicates how long the screen takes to
        /// transition off when it is deactivated.
        /// </summary>
        public TimeSpan TransitionOffTime
        {
            get { return transitionOffTime; }
            set { transitionOffTime = value; }
        }
        
        /// <summary>
        /// Gets the current position of the screen transition, ranging
        /// from zero (fully active, no transition) to one (transitioned
        /// fully off to nothing).
        /// </summary>
        public float TransitionPosition
        {
            get { return transitionPosition; }
            set { transitionPosition = value; }
        }
        
        /// <summary>
        /// Gets the current alpha of the screen transition, ranging
        /// from 1 (fully active, no transition) to 0 (transitioned
        /// fully off to nothing).
        /// </summary>
        public float TransitionAlpha
        {
            get { return 1f - TransitionPosition; }
        }
        
        /// <summary>
        /// Gets the current screen transition state.
        /// </summary>
        public ScreenState ScreenState
        {
            get { return screenState; }
            set { screenState = value; }
        }
        
        /// <summary>
        /// There are two possible reasons why a screen might be transitioning
        /// off. It could be temporarily going away to make room for another
        /// screen that is on top of it, or it could be going away for good.
        /// This property indicates whether the screen is exiting for real:
        /// if set, the screen will automatically remove itself as soon as the
        /// transition finishes.
        /// </summary>
        public bool IsExiting
        {
            get { return isExiting; }
            set { isExiting = value; }
        }
        
        /// <summary>
        /// Checks whether this screen is active and can respond to user input.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return !OtherScreenHasFocus &&
                       (screenState == ScreenState.TransitionOn ||
                        screenState == ScreenState.Active);
            }
        }
        
        /// <summary>
        /// Gets the index of the player who is currently controlling this screen,
        /// or null if it is accepting input from any player. This is used to lock
        /// the game to a specific player profile. The main menu responds to input
        /// from any connected gamepad, but whichever player makes a selection from
        /// this menu is given control over all subsequent screens, so other gamepads
        /// are inactive until the controlling player returns to the main menu.
        /// </summary>
        public PlayerIndex? ControllingPlayer
        {
            get { return controllingPlayer; }
            set { controllingPlayer = value; }
        }
        
        /// <summary>
        /// Gets the gestures the screen is interested in. Screens should be as specific
        /// as possible with gestures to increase the accuracy of the gesture engine.
        /// For example, most menus only need Tap or perhaps Tap and VerticalDrag to operate.
        /// These gestures are handled by the ScreenManager when screens change and
        /// all gestures are placed in the InputState passed to the HandleInput method.
        /// </summary>
        public GestureType EnabledGestures
        {
            get { return enabledGestures; }
            protected set
            {
                enabledGestures = value;

                // the screen manager handles this during screen changes, but
                // if this screen is active and the gesture types are changing,
                // we have to update the TouchPanel ourself.
                if (ScreenState == ScreenState.Active)
                {
                    TouchPanel.EnabledGestures = value;
                }
            }
        }

        /// <summary>
        /// Gets whether or not this screen is serializable. If this is true,
        /// the screen will be recorded into the screen manager's state and
        /// its Serialize and Deserialize methods will be called as appropriate.
        /// If this is false, the screen will be ignored during serialization.
        /// By default, all screens are assumed to be serializable.
        /// </summary>
        public bool IsSerializable
        {
            get { return isSerializable; }
            protected set { isSerializable = value; }
        }

        #endregion

        #region private

        bool isPopup = false;
        TimeSpan transitionOnTime = TimeSpan.Zero;
        TimeSpan transitionOffTime = TimeSpan.Zero;
        float transitionPosition = 1;
        ScreenState screenState = ScreenState.TransitionOn;
        bool isExiting = false;
        public bool OtherScreenHasFocus;
        PlayerIndex? controllingPlayer;
        GestureType enabledGestures = GestureType.None;
        bool isSerializable = true;
        GameScreenService gameScreenService;

        #endregion
    }
}
