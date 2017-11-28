using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public class BattleUnitDescription
    {
        public BattleUnitDescription()
        {
            DefaultKeys = new List<string>();
        }

        #region Properties

        public int BaseMaxHp { get; set; }

        public int BaseStrength { get; set; }

        public int BaseActionPoints { get; set; }

        public BattleUnitSize Size { get; set; }

        public int BaseMaxMana { get; set; }

        public int BaseManaRegen { get; set; }

        public ColorDescription Color { get; set; }

        public List<string> DefaultKeys { get; set; }

        // туду: список способностей

        // туду: список заклинаний

        // туду: список триггеров (пасивных способностей)

        #endregion
    }
}
