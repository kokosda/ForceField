using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForceField.Interfaces;
using ForceField.Core.Service;

namespace ForceField.Core.Managers
{
    public class InputManager : IGameComponent
    {
        public InputService inputService;

        public InputManager(Game game)
        {
            game.Components.Add(this);
            inputService = new InputService();
            game.Services.AddService(typeof(IUserInputService), inputService);
        }

        public void Initialize()
        {

        }
    }
}
