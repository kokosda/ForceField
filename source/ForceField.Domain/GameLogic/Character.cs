using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public class Character : Unit
    {
        #region Properties

        protected int Hp { get; set; }

        protected int MaxHp { get; set; }

        protected int Mana { get; set; }

        protected int MaxMana { get; set; }

        protected int Damage { get; set; }

        protected int DamageRange { get; set; }

        protected int Armor { get; set; }

        protected int Speed { get; set; }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
