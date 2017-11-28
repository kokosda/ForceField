using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public struct SpellLibrarySlot
    {
        public HeroSpell Spell { get; set; }            // заклинание, вставленное в слот

        public int MaxUse { get; set; }             // максимально использований за бой

        public int CastedNumber { get; set; }       // сколько было потрачено

        public int InHandNumber { get; set; }       // сколько имеется в руке
    }
}
