using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public enum BattlePhase
    {
        Initialize,
        PrimaryDrawCard,
        ManaCheck,
        Support,
        DrawCard,
        Wait,
        PreAction,
        Action,
        ClearPhase
    }
}
