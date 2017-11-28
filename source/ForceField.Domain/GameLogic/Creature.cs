using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public class Creature : BattleUnit
    {
        public Creature(string name)
        {
            Name = name;
        }

        #region Properties

        public int Morale { get; set; }

        public int Rage { get; set; }

        public int Fear { get; set; }

        public HeroSpell SummonSpell { get; set; }

        // todo: список триггеров

        // todo: список ключей

        // todo: список текущих эффектов

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
