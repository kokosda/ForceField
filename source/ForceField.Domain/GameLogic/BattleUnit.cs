using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public class BattleUnit : Unit
    {
        public BattleUnit()
        {
        }

        #region Properties

        public BattleUnitDescription Description { get; set; }

        public string BattleId { get; set; }

        public int Hp { get; set; }

        public int MaxHpModifier { get; set; }

        public int ActionPoints { get; set; }

        public int MaxActionPointsModifier { get; set; }

        public int StrengthModifier { get; set; }

        public int AttackModifier { get; set; }

        public int MaxHp
        {
            get
            {
                return Description.BaseMaxHp + MaxHpModifier;
            }
        }

        public int MaxActionPoints
        {
            get
            {
                return Description.BaseActionPoints + MaxActionPointsModifier;
            }
        }

        public int Strength 
        {
            get
            {
                return Description.BaseStrength + StrengthModifier;
            }
        }

        public int AttackValue
        {
            get
            {
                return Strength + AttackModifier;
            }
        }

        public List<string> Keys { get; set; }

        // туду: текущий список способностей

        // туду: текущий список триггеров

        // туду: список действующих эффектов

        #endregion
    }
}
