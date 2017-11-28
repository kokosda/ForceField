using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ForceField.GameState.Models.Base;
using ForceField.GameState.Services;

namespace ForceField.GameState.Models
{
    public class GameplayScreen : GameScreen
    {
        public GameplayScreen(GameplayScreenServiceImpl service) :
            base(service)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                null,
                true);
        }

        #region Properties

        public InputAction PauseAction
        {
            get
            {
                return pauseAction;
            }
        }

        public SpriteFont GameFont
        {
            get
            {
                return gameFont;
            }
            set
            {
                gameFont = value;
            }
        }

        public Random Random
        {
            get
            {
                return random;
            }
        }

        public float PauseAlpha
        {
            get
            {
                return pauseAlpha;
            }
            set
            {
                pauseAlpha = value;
            }
        }

        #endregion

        #region Fields

        public Vector2 EnemyPosition = new Vector2(100, 100);

        public Vector2 PlayerPosition = new Vector2(100, 100);

        #endregion

        #region private

        SpriteFont gameFont;

        Random random = new Random();

        float pauseAlpha;

        InputAction pauseAction;

        #endregion
    }
}
