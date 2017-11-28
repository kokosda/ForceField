using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.GameState.Services;
using ForceField.GameState.Models.Base;

namespace ForceField.GameState.Models
{
    public class GlobalMapScreen : GameplayScreen
    {
        public GlobalMapScreen(GlobalMapScreenServiceImpl impl)
            : base(impl)
        {

        }
    }
}
