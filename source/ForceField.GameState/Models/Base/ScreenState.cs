using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.GameState.Models.Base
{
    /// <summary>
    /// Enum describes the screen transition state.
    /// </summary>
    public enum ScreenState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }
}
