using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.GameState.Models.Base;

namespace ForceField.Interfaces
{
    public interface IScreenFactory
    {
        GameScreen CreateScreen(Type screenType);
    }
}
