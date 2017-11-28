using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Domain.GameLogic;

namespace ForceField.Interfaces
{
    public interface IBattleService
    {
        void StartBattle(Hero offensiveHero, Hero defendingHero);

        void StartBattle(Battle battle);

        Battle CreateTestBattle();

        void Update();

        Battle CurrentBattle 
        { 
            get; 
        }
    }
}
