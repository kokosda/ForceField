using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ForceField.GameState.Models.Base;
using ForceField.GameState.Services;
using ForceField.Domain.GameLogic;

namespace ForceField.GameState.Models
{
    public class BackgroundScreen : GameScreen
    {
        public BackgroundScreen(BackgroundScreenServiceImpl service) :
            base(service)
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public Background background;
    }
}
